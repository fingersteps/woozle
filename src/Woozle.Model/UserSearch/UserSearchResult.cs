using System;

namespace Woozle.Model.UserSearch
{
    public class UserSearchResult
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime? LastLogin { get; set; }
        public Language Language { get; set; }
        public int FlagActiveStatusId { get; set; }
        public string Email { get; set; }
    }
}
