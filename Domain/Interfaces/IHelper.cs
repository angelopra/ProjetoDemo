using Domain.Validators;
using System;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IHelper
    {
        List<ValidateError> ValidateObj<T>(object request);
        Exception MapperException(Exception exception, List<ValidateError> errors = null);
        T MappingEntity<T>(object obj);
    }
}
