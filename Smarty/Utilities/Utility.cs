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
    public class NegateBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }
    }

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
    }
}
