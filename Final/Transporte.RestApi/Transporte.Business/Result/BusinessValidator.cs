using System;
using System.Collections.Generic;
using System.Linq;

namespace Transporte.Business.Result
{
    public class BusinessValidation
    {
        private List<ErrorDetail> tempError;

        public Error Error { get; private set; }

        public bool IsSuccess { get { return Error == null; } }

        public BusinessValidation()
        {
            tempError = new List<ErrorDetail>();
        }

        public void AddErrorDetail(ErrorDetail errorDetail)
        {
            tempError.Add(errorDetail);
        }

        public void AddErrorDetail(List<ErrorDetail> errorDetails)
        {
            tempError.AddRange(errorDetails);
        }

        public bool ContainsError(ErrorDetail errorDetail)
        {
            var hasError = tempError.FirstOrDefault(p => p.Code == errorDetail.Code);
            if (hasError != null)
                return true;

            if (Error != null)
                return Error.Details.FirstOrDefault(p => p.Code == errorDetail.Code) != null;

            return false;
        }

        /// <summary>
        /// Verifica se possui erro e retorna o resultado
        /// </summary>
        /// <param name="errorGroup"></param>
        /// <returns>Retorna true caso possua erro</returns>
        public bool Validate(ErrorGroup errorGroup = null)
        {
            if (errorGroup == null)
                errorGroup = ErrorGroup.INVALID_INPUT;

            if (Error != null && errorGroup.Code != Error.Code)
                throw new InvalidOperationException(
                        string.Format("Já foi realizado a validação do tipo '{0}', não é possível ter mais de uma operação de alto nível para realizar validações de tipos de erro diferente na mesma transação, procure manter sua api com métodos atómicos, caso seja realmente necessário, valide um erro mais genérico",
                        Error.Code));

            if (tempError.Count == 0 && Error == null)
                return false;

            if (Error == null)
                Error = new Error(errorGroup.CodeId, errorGroup.Code, errorGroup.Message);

            Error.Details.AddRange(tempError);

            tempError.Clear();

            return true;
        }
    }
}