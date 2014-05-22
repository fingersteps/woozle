using System.Runtime.Serialization;
using ServiceStack.ServiceInterface.ServiceModel;

namespace Woozle.Services.Registration
{
    [DataContract]
    public class RegistrationResponse
    {
        public RegistrationResponse()
        {
            this.ResponseStatus = new ResponseStatus();
        }

        [DataMember(Order = 1)]
        public string UserId { get; set; }
        [DataMember(Order = 2)]
        public string SessionId { get; set; }
        [DataMember(Order = 3)]
        public string UserName { get; set; }
        [DataMember(Order = 4)]
        public string ReferrerUrl { get; set; }
        [DataMember(Order = 5)]
        public ResponseStatus ResponseStatus { get; set; }
    }
}
