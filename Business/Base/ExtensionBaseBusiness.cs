using Domain.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Business.Base
{
    static class ExtensionBaseBusiness
    {

        private static JsonSerializerSettings JsonSerializerSettings()
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            };

            return jsonSerializerSettings;
        }

        public static string SerializerObjectToJson(object obj)
        {
            var convertObjToJson = JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.None, JsonSerializerSettings());

            return convertObjToJson;
        }

        public static T DeserializerJsonToObject<T>(string obj)
        {
            var convertJsonToObj = JsonConvert.DeserializeObject<T>(obj, JsonSerializerSettings());
            return convertJsonToObj;
        }
        public static T Map<T>(this object obj)
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
        public static T MappingEntityLinq<T>(this IQueryable<object> obj)
        {
            return Map<T>(obj);
        }

        public static PaginatedList<T> Paginate<T>(this List<T> obj, int? pageNumber, int? itemsPerPage)
        {
            var list = PaginatedList<T>.Create(obj, pageNumber ?? 1, itemsPerPage ?? 10);
            return list;
        }
        public static PaginatedList<T> PaginateLinq<T>(this IQueryable<T> obj, int? pageNumber, int? itemsPerPage)
        {
            var list = PaginatedList<T>.Create(obj.ToList(), pageNumber ?? 1, itemsPerPage ?? 10);
            return list;
        }
    }
}
