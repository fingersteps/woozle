using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woozle.Model;

namespace Woozle.Domain.Authentication
{
    public interface IRegistrationSettings
    {
        Language DefaultLanguage { get; set; }
        Status DefaultFlagActiveStatus { get; set; }
    }
}
