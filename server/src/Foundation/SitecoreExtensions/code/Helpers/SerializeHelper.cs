using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace MockProject.Foundation.SitecoreExtensions.Helpers
{
    public static class SerializeHelper
    {
        public static string SerializeToJson<T>(T pObj)
        {
            var stream = new MemoryStream();
            var ser = new DataContractJsonSerializer(typeof(T));
            ser.WriteObject(stream, pObj);
            stream.Position = 0;
            var sr = new StreamReader(stream);
            return sr.ReadToEnd();
        }

        public static T DeserializeFromJson<T>(string pSerializedObj)
        {
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(pSerializedObj)))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(ms);
            }
        }
    }
}