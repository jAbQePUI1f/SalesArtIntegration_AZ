using System.Reflection;

namespace SalesArtIntegration_AZ.Manager.Helper
{
    public static class TransferHelper
    {

        public static string[] ConvertToStringArray<T>(T obj)
        {
            List<string> stringArray = new List<string>();

            if (obj != null)
            {
                PropertyInfo[] properties = typeof(T).GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(obj, null);
                    string stringValue = (value != null) ? value.ToString() : "null";

                    stringArray.Add($"{property.Name}: {stringValue}");
                }
            }

            return stringArray.ToArray();
        }
        public static string GetMessages(this List<string> list)
        {
            string msg = "";

            foreach (var item in list)
            {
                if (list.Count > 1)
                {
                    msg += item + "\n";
                }
                else
                {
                    msg += item;
                }
            }

            return msg;
        }

    }
}
