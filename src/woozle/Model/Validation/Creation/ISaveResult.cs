﻿using System.Collections.Generic;

namespace Woozle.Core.Model.Validation.Creation
{
    public interface ISaveResult<TO>
    {
        TO TargetObject { get; set; }

        bool HasErrors { get; }

        bool HasSystemErrors { get; set; }

        List<Error> Errors { get; set; }
    }
}