using System.Reflection;

namespace PayrollEngine
{
    /// <summary>
    /// Property with value
    /// </summary>
    public class PropertyValue
    {
        /// <summary>
        /// The property
        /// </summary>
        public PropertyInfo Property { get; set; }

        /// <summary>
        /// The property value
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// The dictionary key
        /// </summary>
        public string DictionaryKey { get; set; }
    }
}
