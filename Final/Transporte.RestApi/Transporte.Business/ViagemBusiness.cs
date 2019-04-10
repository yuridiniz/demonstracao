using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Transporte.Model;
using Transporte.Business.Result;
using Transporte.Business.Base.KeyGuid;
using Transporte.Repository.Interface;
using Transporte.Repository.Interface.KeyGeneric;

namespace Transporte.Business
{
    public class ViagemBusiness : BasicBusiness<Viagem>
    {
        public static readonly ErrorDetail VIAGEM_NAO_ENCONTRADA = new ErrorDetail("id", "viagem_nao_encontrado", "O registro informado não foi localizado");
        public static readonly ErrorDetail VIAGEM_JA_ESTORNADA = new ErrorDetail("status", "viagem_ja_estornada", "Já foi feito o estorno para a viagem do dia {0} do colaborador {1}, não é possível realizar a finalização");
        public static readonly ErrorDetail VIAGEM_JA_FINALIZADA = new ErrorDetail("status", "viagem_ja_finalizada", "Já foi feito a finalização da viagem do dia {0} do colaborador {1}, não é possível realizar o estorno da mesma");
        public static readonly ErrorDetail FORA_PRAZO_EXCLUSAO = new ErrorDetail("data", "fora_prazo_exclusao", "Não é mais possível excluir a viagem do dia {0} do colaborador {1}");

        private readonly IViagemDAL viagemDal;

        public async Task<BusinessResult<Guid>> Finalizar(Guid id)
        {
            var result = new BusinessResult<Guid>();

            var registroViagem = await viagemDal.Obter(id);

            // Verifica se foi informado uma Guid de uma viagem inexistente
            if (registroViagem == null)
                result.AddErrorDetail(VIAGEM_NAO_ENCONTRADA);

            // Verifica se algumas das validações anteriores deram problemas
            // Caso tenha dado, não faz sentido continuar com as demais validações
            if (result.Validate())
                return result;

            // Demonstração para o método de formatação
            if (registroViagem.StatusViagemId == StatusViagem.Estornado)
                result.AddErrorDetail(VIAGEM_JA_ESTORNADA.Format(registroViagem.DataRegistro, registroViagem.Matricula));

            // Caso essa viagem já tenha sido estornada, não é possível finalizar
            // Dessa forma o sistema acusa um erro para a operação informando que o grupo "INVALID_INPUT"
            // Mesmo que exista uma operação de administrador para realizar isso, não devemos retornar "FORBIDDEN"
            // pois o usuário não precisa saber que existe usuários que conseguem realizar essa operação
            // No contexto dele, é um erro
            if (result.Validate())
                return result;

            // Não iremos considerar essa situação como um erro, pois pode ter sido proveniente de um click duplo de mouse
            // Apensar dele ter tentado finalizar um registro que já está finalizado, para não confundi o usuário
            // nesse cenário não adicionaremos nenhum detalhe de erro e finalizaremos a função resultado como sucesso
            if (registroViagem.StatusViagemId == StatusViagem.Finalizado)
                return result;

            registroViagem.StatusViagemId = StatusViagem.Finalizado;
            await viagemDal.Salvar(registroViagem);

            // Apesar de não ter muito sentido retornar o Id já que quem chamou essa função já possui essa informação
            // estamos retornando para demonstrar como uma função de negócio pode retornar seu resultado
            result.Result = id;
            return result;
        }

        public async Task<BusinessResult<Guid>> Estornar(Guid id)
        {
            var result = new BusinessResult<Guid>();

            var registroViagem = await viagemDal.Obter(id);

            if (registroViagem == null)
                result.AddErrorDetail(VIAGEM_NAO_ENCONTRADA);

            if (result.Validate())
                return result;

            if (registroViagem.StatusViagemId == StatusViagem.Finalizado)
                result.AddErrorDetail(VIAGEM_JA_FINALIZADA.Format(registroViagem.DataRegistro, registroViagem.Matricula));

            if (result.Validate())
                return result;

            if (registroViagem.StatusViagemId == StatusViagem.Estornado)
                return result;

            registroViagem.StatusViagemId = StatusViagem.Finalizado;
            await viagemDal.Salvar(registroViagem);

            result.Result = id;

            return result;
        }

        public override async Task<BusinessResult> Excluir(Guid id)
        {
            var result = new BusinessResult();

            var registroViagem = await viagemDal.Obter(id);

            if (registroViagem == null)
                result.AddErrorDetail(VIAGEM_NAO_ENCONTRADA);

            if (result.Validate())
                return result;

            if (registroViagem.DataRegistro.AddMinutes(10) < DateTime.Now)
                result.AddErrorDetail(FORA_PRAZO_EXCLUSAO.Format(registroViagem.DataRegistro, registroViagem.Matricula));

            if (result.Validate())
                return result;

            await viagemDal.Excluir(id);

            return result;
        }

        public async Task<BusinessResult<List<Viagem>>> Listar(DateTime? data)
        {
            var result = new BusinessResult<List<Viagem>>();

            if (!data.HasValue)
                result.Result = await viagemDal.Listar();
            else
                result.Result = await viagemDal.ListarPorData(data.Value);

            return result;
        }

        public async Task<BusinessResult<Guid>> Registrar(Viagem viagem)
        {
            var result = new BusinessResult<Guid>();

            viagem.Id = Guid.Empty;
            viagem.StatusViagemId = StatusViagem.EmAberto;
            viagem.DataRegistro = DateTime.Now;

            await viagemDal.Salvar(viagem);

            result.Result = viagem.Id;

            return result;
        }

        public ViagemBusiness(IViagemDAL viagemDal) : base(viagemDal)
        {
            this.viagemDal = viagemDal;
        }
    }
}