using System.Collections.Generic;

namespace Woozle.Model.Validation.Delete
{
    public interface IDeleteResult<TO>
    {
        TO TargetObject { get; set; }
        List<Error> Errors { get; }
    }
}
