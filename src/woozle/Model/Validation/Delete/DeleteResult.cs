using System.Collections.Generic;

namespace Woozle.Model.Validation.Delete
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
