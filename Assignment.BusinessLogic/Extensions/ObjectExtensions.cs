using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Assignment.BusinessLogic.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Serialize Object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string SerializeObject<T>(this T model)
        {
            var serializer = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };

            return JsonConvert.SerializeObject(model, serializer);
        }   
    }
}
