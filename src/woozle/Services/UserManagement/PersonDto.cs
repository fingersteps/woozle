
using System;
using Woozle.Model;

namespace Woozle.Services.UserManagement
{
    [Serializable]
    public partial class PersonDto : WoozleDto
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string EMail { get; set; }
        public byte[] Picture { get; set; }
        public string Street { get; set; }
        public Nullable<int> CityId { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public Nullable<System.DateTime> Birthdate { get; set; }
        public string EnterpriseName { get; set; }
        public Nullable<int> SalutationStatusId { get; set; }
        public byte[] ChangeCounter { get; set; }
    
        public City City { get; set; }
        public Status SalutationStatus { get; set; }
        public Mandator.MandatorDto MandatorDto { get; set; }
    }
    
}
