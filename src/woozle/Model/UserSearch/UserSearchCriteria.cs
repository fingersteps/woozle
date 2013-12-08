namespace Woozle.Model.UserSearch
{
    public class UserSearchCriteria
    {
        public UserSearchCriteria()
        {
            this.Username = string.Empty;
            this.Firstname = string.Empty;
            this.Lastname = string.Empty;
        }

        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
