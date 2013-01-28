using System;
using System.Collections.Generic;

namespace DRCOG.Common.Domain
{
    public interface IValidatable
    {
        IDictionary<String, String> GetValidationResults();
        Boolean IsValid();
        void ResetValidation();
    }
}
