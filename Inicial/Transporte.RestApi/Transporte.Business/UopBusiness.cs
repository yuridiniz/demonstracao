using Transporte.Business.Base;
using Transporte.Business.Result;
using Transporte.Repository.Interface;
using Transporte.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transporte.Business.Base.KeyInt;

namespace Transporte.Business
{
    public class UopBusiness : BasicBusiness<Uop>
    {
        // Todos os erros dessa classe de negócio devem ser especificados, dessa forma, é possível que outras classes do sistema possam
        // Saber quais foram os erros que ocorreram na execução de um método de negócio
        public static readonly ErrorDetail NAME_EXIST = new ErrorDetail("Nome", "uop_name_exist", "O nome '{0}' já foi cadastrado.");
        public static readonly ErrorDetail CODE_EXIST = new ErrorDetail("CdUop", "uop_code_exist", "O código '{0}' já foi cadastrado.");
        public static readonly ErrorDetail INVALID_YEAR = new ErrorDetail("Year", "invalid_year", "Não é possível adicionar unidades para ser ativadas antes do ano atual.");
        public static readonly ErrorDetail INVALID_HRS = new ErrorDetail("", "invalid_hrs", "O sistema não aceita registros de UOP antes das {0} horas.");

        private readonly IRegionalDAL regionalDAL;
        private readonly IUopDAL uopDAL;

        /// <summary>
        /// Esse método exemplifica uma validação de negócio, aqui podemos ver dois cenários de grupos de erros distintos
        /// O primeiro simula uma validação referente a permissão, onde verifica se o usuário possui permissão para
        /// cadastro de UOPs com datas de ativação para o ano anterior.
        /// Caso essa condição seja negativa, ele não precisa verificar o resto das validações, já não fará mais sentido
        /// então ignora o resto do método e retorna um erro nomeada de "FORBIDEN"
        /// Caso o usuário tenha permissão, ou então não não seja uma ativação para o ano anterior, a aplicação continua
        /// as validações, novamente testando se está valido quando finalizar um grupo de validações
        /// </summary>
        public async Task<BusinessResult<int>> Adicionar(Uop uop)
        {
            var result = new BusinessResult<int>();

            if (!result.Validate(ErrorGroup.FORBIDDEN))
                return result;

            if (uop.DataAtivacao.Year < DateTime.Now.Year)
                result.AddErrorDetail(INVALID_YEAR);

            if (!result.Validate(ErrorGroup.FORBIDDEN)) // Reflete o cenário no qual não faz mais sentido continuar caso haja um erro
                return result;

            var uopMesmoNome = uopDAL.ObterPorNome(uop.Nome);
            var uopMesmoCodigo = uopDAL.Obter(uop.Id);

            if (uopMesmoNome != null)
                result.AddErrorDetail(NAME_EXIST.Format(uop.Nome)); // Exemplo com formatação dinâmica de uma mensagem de erro pré-defininda

            if (uopMesmoCodigo != null)
                result.AddErrorDetail(CODE_EXIST.Format(uop.Id));

            /* ... */
            /* Mais validações referente aos campo informados junto a regras de negócios */
            /* ... */

            if (!result.Validate(ErrorGroup.INVALID_INPUT))
                return result;

            await uopDAL.Salvar(uop);
            result.Result = uop.Id;

            return result;
        }

        public async Task<BusinessResult<List<int>>> AdicionarLista(List<Uop> uops)
        {
            var result = new BusinessResult<List<int>>();

            using(var transaction = await uopDAL.BeginTransaction()) {
                foreach (var item in uops)
                {
                    var resultadoAdicao = await Adicionar(item);
                    if(resultadoAdicao.IsSuccess) {
                        result.Result.Add(resultadoAdicao.Result);
                    } else {
                        result.AddErrorDetail(resultadoAdicao.Error.Details);
                    }
                }

                if(result.Validate(ErrorGroup.INVALID_INPUT)) {
                    transaction.Commit();
                }
            }

            return result;
        }

        /// <summary>
        /// Será injetado dois repositórios, e passaremos apenas uma para a classe base
        /// O repositório passado será o repositório "principal" da classe de negócio
        /// No qual proverá os métodos de CRUD básicos para essa classe de negócio
        /// </summary>
        public UopBusiness(IRegionalDAL regionalDAL, IUopDAL uopDAL)
        : base(uopDAL)
        {
            this.regionalDAL = regionalDAL;
            this.uopDAL = uopDAL;
        }
    }
}
