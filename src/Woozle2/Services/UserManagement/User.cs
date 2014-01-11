
using System;
using System.Collections.ObjectModel;
using ServiceStack;
using ServiceStack.ServiceHost;
using Woozle.Model;
using Woozle.Services.Location;
using Language = Woozle.Services.Location.Language;

namespace Woozle.Services.UserManagement
{
    [Serializable]
    [Route("/users", "POST,PUT,DELETE")]
    [Route("/users/{Id}")]
    public partial class User : WoozleDto, IReturn<UserResponse>, IReturn<SaveResultDto<User>>, IReturnVoid
    {
        public User()
        {
            this.UserMandatorRoles = new ObservableCollection<UserMandatorRole>();
        }
    
        public string Username { get; set; }
        public string Password { get; set; }
        public bool FlagActive { get; set; }
        public Nullable<System.DateTime> LastLogin { get; set; }
        public Nullable<System.DateTime> LastPasswordChange { get; set; }
        public int LanguageId { get; set; }
        public int FlagActiveStatusId { get; set; }
        public byte[] ChangeCounter { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    
        public Language Language { get; set; }
        public Status Status { get; set; }
        public ObservableCollection<UserMandatorRole> UserMandatorRoles { get; set; }
    
    }

    public class UserResponse
    {
        public User User { get; set; }
    }
    
}
