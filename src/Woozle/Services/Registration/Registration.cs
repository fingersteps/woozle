using System.Runtime.Serialization;
using ServiceStack.ServiceHost;

namespace Woozle.Services.Registration
{
    [DataContract]
    public class Registration : IReturn<RegistrationResponse>
    {
        [DataMember(Order = 1)]
        public string UserName { get; set; }
        [DataMember(Order = 2)]
        public string FirstName { get; set; }
        [DataMember(Order = 3)]
        public string LastName { get; set; }
        [DataMember(Order = 4)]
        public string DisplayName { get; set; }
        [DataMember(Order = 5)]
        public string Email { get; set; }
        [DataMember(Order = 6)]
        public string Password { get; set; }
        [DataMember(Order = 7)]
        public bool? AutoLogin { get; set; }
        [DataMember(Order = 8)]
        public string Continue { get; set; }
        [DataMember(Order = 9)]
        public string LanguageCode { get; set; }
    }
}
