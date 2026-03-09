using System;

namespace PayrollEngine;

/// <summary>Attribute extensions for <see cref="string"/></summary>
public static class StringAttributeExtensions
{
    /// <param name="attribute">The attribute</param>
    extension(string attribute)
    {
        /// <summary>Test for text attribute field name</summary>
        /// <returns>True for a text attribute field</returns>
        public bool IsTextAttributeField() =>
            attribute.StartsWith(SystemSpecification.TextAttributePrefix);

        /// <summary>To text attribute field name</summary>
        /// <returns>Text attribute name</returns>
        public string ToTextAttributeField() =>
            attribute.ToAttributeField(SystemSpecification.TextAttributePrefix);

        /// <summary>Test for text attribute field name</summary>
        /// <returns>True for a date attribute field</returns>
        public bool IsDateAttributeField() =>
            attribute.StartsWith(SystemSpecification.DateAttributePrefix);

        /// <summary>To date attribute field name</summary>
        /// <returns>Date attribute name</returns>
        public string ToDateAttributeField() =>
            attribute.ToAttributeField(SystemSpecification.DateAttributePrefix);

        /// <summary>Test for numeric attribute field name</summary>
        /// <returns>True for a numeric attribute field</returns>
        public bool IsNumericAttributeField() =>
            attribute.StartsWith(SystemSpecification.NumericAttributePrefix);

        /// <summary>To numeric attribute field name</summary>
        /// <returns>String starting uppercase</returns>
        public string ToNumericAttributeField() =>
            attribute.ToAttributeField(SystemSpecification.NumericAttributePrefix);

        /// <summary>Remove the attribute field name prefix</summary>
        /// <returns>Attribute name without prefix</returns>
        public string RemoveAttributePrefix()
        {
            if (attribute.IsTextAttributeField())
            {
                return attribute.RemoveFromStart(SystemSpecification.TextAttributePrefix);
            }
            if (attribute.IsDateAttributeField())
            {
                return attribute.RemoveFromStart(SystemSpecification.DateAttributePrefix);
            }
            if (attribute.IsNumericAttributeField())
            {
                return attribute.RemoveFromStart(SystemSpecification.NumericAttributePrefix);
            }
            return attribute;
        }

        private string ToAttributeField(string prefix)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(attribute);
            return attribute.EnsureStart(prefix);
        }
    }
}