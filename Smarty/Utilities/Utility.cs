using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Smarty.Utilities
{
    public class Utility
    {
        public async static Task<T> DeserializeObjectFromHttpResponse<T>(HttpResponseMessage response) where T : new()
        {
            T deserializedObject = new T();
            Stream responseStream = await response.Content.ReadAsStreamAsync();
            using (StreamReader reader = new StreamReader(responseStream))
            {
                using (JsonTextReader read = new JsonTextReader(reader))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    deserializedObject = serializer.Deserialize<T>(read);
                }
            }
            return deserializedObject;
        }

        public static byte[] ConvertToByteArray(string hexString)
        {
            string[] hexArray = hexString.Split('-');
            byte[] byteArray = new byte[hexArray.Length];
            for (int i = 0; i < hexArray.Length; i++)
            {
                byteArray[i] = Convert.ToByte(hexArray[i], 16);
            }
            return byteArray;
        }
    }
}
