using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Woozle.Core.Model.Validation.Delete
{
    public class DeleteResult<TO> : IDeleteResult<TO>
    {
        public DeleteResult()
        {
            this.Errors = new List<Error>();
        }

        public TO TargetObject { get; set; }
        public List<Error> Errors { get; private set; }
    }
}
