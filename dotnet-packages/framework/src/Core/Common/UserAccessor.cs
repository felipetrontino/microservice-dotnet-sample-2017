namespace Framework.Core.Common
{
    public class UserAccessor : IUserAccessor
    {
        public UserAccessor(string userName = "system")
        {
            UserName = userName;
        }

        public string UserName { get; }
    }
}
