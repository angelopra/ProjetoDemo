using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Business.Base
{
    public class BaseBusiness<TApp>
    {
        protected BaseBusiness(TApp context)
        {
            this._context = context;
        }

        public TApp _context { get; }

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
    }
}
