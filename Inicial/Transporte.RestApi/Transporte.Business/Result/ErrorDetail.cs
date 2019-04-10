using System;

namespace Transporte.Business.Result
{
    public class ErrorDetail
    {
        public string Target { get; }

        public string Code { get; }

        public string Message { get; }

        public ErrorDetail(string target, string code, string message)
        {
            if(code == null) throw new ArgumentException("Deve ser informado um código de erro válido", "code");
            if(message == null) throw new ArgumentException("Deve ser informada uma mensagem de erro clara", "message");

            Target = target;
            Code = code;
            Message = message;
        }


        public ErrorDetail() { }

        public ErrorDetail Format(params object[] valor)
        {
            return new ErrorDetail(Target, Code, string.Format(Message, valor));
        }
    }
}