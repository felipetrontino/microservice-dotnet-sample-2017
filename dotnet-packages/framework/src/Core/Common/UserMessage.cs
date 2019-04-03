namespace Framework.Core.Common
{
    public class UserMessage
    {
        public UserMessage(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public int Code { get; }
        public string Message { get; }

        public UserMessage Format(params object[] args)
        {
            return new UserMessage(Code, string.Format(Message, args));
        }
    }
}