using System.Collections;
using System.Linq;
using System.Reflection;
using System.Text;
using Task11.FileHandler;

namespace Task11
{
    internal class TxtSerializer
    {

        #region Props
        #endregion

        #region Ctors
        public TxtSerializer()
        { }
        #endregion

        #region Methods
        public TXTSerializedParameters Serialize<T>(in T obj)
            where T : class
        {
            TXTSerializedParameters txtSerializedParameters = new TXTSerializedParameters();
            txtSerializedParameters.Add("Type", obj.GetType().Name);
            foreach (PropertyInfo property in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (property.PropertyType.GetInterfaces().Contains(typeof(ICollection)))
                {
                    StringBuilder value = new();
                    foreach (object item in (IEnumerable)property.GetValue(obj, null))
                    {
                        value.Append(item);
                    }
                    txtSerializedParameters.Add(property.Name, $"{{{value}}}");
                }
                else
                {
                    txtSerializedParameters.Add(property.Name, property.GetValue(obj).ToString());
                }
            }
            return txtSerializedParameters;
        }
        #endregion

        #region ObjectOverrides
        #endregion
    }
}
