//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using ServiceStack;
using Woozle.Core.Services.Stack.ServiceModel.LocationManagement;

namespace Woozle.Core.Services.Stack.ServiceModel.Mandator
{
    [Serializable]
    [Route("/mandator", "GET, PUT")]
    public partial class Mandator : WoozleDto, IReturn<Mandator>, IReturn<SaveResult<Mandator>>
    {
        public string Name { get; set; }
        public string Street { get; set; }
        public string Phone { get; set; }
        public Nullable<int> CityId { get; set; }
        public byte[] Picture { get; set; }
        public byte[] ChangeCounter { get; set; }
        public string Email { get; set; }
        public Nullable<int> MandatorGroupId { get; set; }
    
        public City City { get; set; }
        public MandatorGroup MandatorGroup { get; set; }
    
    }
    
}