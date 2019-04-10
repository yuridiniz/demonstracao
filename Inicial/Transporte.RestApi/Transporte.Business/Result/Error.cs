using System.Collections.Generic;

namespace Transporte.Business.Result
{
    public class ErrorGroup {
        public int CodeId { get; }

        public string Code { get; }

        public string Message { get; }

        public ErrorGroup(int codeId, string code, string message)
        {
            CodeId = codeId;
            Code = code;
            Message = message;
        }

        public static readonly ErrorGroup INTERNAL_ERROR = new ErrorGroup(500, "internal_error", "Erro interno");
        public static readonly ErrorGroup INVALID_INPUT = new ErrorGroup(400, "invalid_input", "Valores inválidos foram informados");
        public static readonly ErrorGroup BAD_REQUEST = new ErrorGroup(400, "bad_rquest", "Valores inválidos foram informados");
        public static readonly ErrorGroup FORBIDDEN = new ErrorGroup(403, "forbidden", "Você não tem autorização para realizar essa ação");

    }

    public class Error
    {

        public int CodeId { get; }

        public string Code { get; }

        public string Message { get; }

        public List<ErrorDetail> Details { get; set; }

        public Error(int codeId, string code, string message)
        {
            CodeId = codeId;
            Code = code;
            Message = message;
            Details = new List<ErrorDetail>();
        }
    }
}