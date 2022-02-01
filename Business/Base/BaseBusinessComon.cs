using Domain.Validators;
using System;
using System.Collections.Generic;
using FluentValidation.Results;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace Business.Base
{
    public class BaseBusinessComon
    {
        #region Serializations
        private JsonSerializerSettings JsonSerializerSettings()
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            };

            return jsonSerializerSettings;
        }

        public string SerializerObjectToJson(object obj)
        {
            var convertObjToJson = JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.None, JsonSerializerSettings());

            return convertObjToJson;
        }

        public T DeserializerJsonToObject<T>(string obj)
        {
            var convertJsonToObj = JsonConvert.DeserializeObject<T>(obj, JsonSerializerSettings());
            return convertJsonToObj;
        }

        public T MappingEntity<T>(object obj)
        {
            object objCurrent = null;
            try
            {
                object response = null;
                if (obj != null)
                {
                    var typeObj = typeof(T);
                    var fullName = typeObj.FullName;
                    Assembly assem = typeObj.Assembly;

                    dynamic convertJsonToObj = null;
                    var convertToJson = SerializerObjectToJson(obj);
                    if (!string.IsNullOrEmpty(convertToJson))
                    {
                        objCurrent = assem.CreateInstance(fullName);
                        convertJsonToObj = DeserializerJsonToObject<T>(convertToJson);
                        if (convertJsonToObj != null)
                        {
                            objCurrent = convertJsonToObj;
                        }

                        response = (T)Convert.ChangeType(objCurrent, typeof(T));
                    }
                }

                return (T)response;
            }
            catch (InvalidCastException)
            {
                return default(T);
            }
        }
        #endregion

        #region errorHandling
        public List<ValidateError> ValidateObj<T>(object request)
        {
            List<ValidateError> validateErrors = null;
            Type typeObj = null;
            Type validatorType = null;
            dynamic objCurrent = null;
            dynamic validator = null;

            if (request != null)
            {
                validator = Activator.CreateInstance<T>();
                validatorType = validator.GetType();

                typeObj = request.GetType();
                objCurrent = Convert.ChangeType(request, typeObj);

                MethodInfo methodValidator = validatorType.GetMethods()
                                                .Where(m => m.IsPublic
                                                    && m.IsFinal == true
                                                    && m.Name == "Validate")
                                                .FirstOrDefault();

                if (methodValidator != null)
                {
                    Object[] parameters = new Object[1];
                    parameters[0] = objCurrent;

                    var results = (ValidationResult)methodValidator.Invoke(validator, parameters);
                    if (!results.IsValid)
                    {
                        validateErrors = new List<ValidateError>();
                        foreach (var failure in results.Errors)
                        {
                            var error = new ValidateError();
                            error.PropertyName = failure.PropertyName;
                            error.Error = failure.ErrorMessage;
                            validateErrors.Add(error);
                        }
                    }
                }
            }
            return validateErrors;
        }

        public Exception MapperException(Exception exception, List<ValidateError> errors = null)
        {
            exception.Data.Add("Success", false);
            if (errors != null)
            {
                foreach (var error in errors)
                {
                    var exists = exception.Data.Contains(error.PropertyName);
                    if (!exists)
                    {
                        exception.Data.Add(error.PropertyName, error.Error);
                    }
                    else
                    {
                        exception.Data[error.PropertyName] += $", {error.Error}";
                    }
                }
            }
            else
            {
                exception.Data.Add("Message", exception.Message);
            }

            return exception;
        }
        #endregion

        public string EnumGetValue<T>(T value)
        {
            var enumType = typeof(T);
            var values = System.Enum.GetValues(enumType);

            foreach (var item in values)
            {
                if (item.ToString() == value.ToString())
                {
                    var name = System.Enum.Format(enumType, value, "g");

                    return name;
                }
            }
            return null;
        }
    }
}
