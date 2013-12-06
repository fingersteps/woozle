using System.Runtime.Serialization;

namespace Woozle.Core.Model.Validation
{
    [DataContract]
    public class Error
    {
        public Error()
        {

        }

        public Error(string field, string message)
        {
            this.Field = field;
            this.Message = message;
        }

        [DataMember]
        public string Field { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}
