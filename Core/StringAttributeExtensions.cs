using System;

namespace PayrollEngine;

/// <summary>Attribute extensions for <see cref="string"/></summary>
public static class StringAttributeExtensions
{
    /// <summary>Test for text attribute field name</summary>
    /// <param name="attribute">The attribute</param>
    /// <returns>True for a text attribute field</returns>
    public static bool IsTextAttributeField(this string attribute) =>
        attribute.StartsWith(SystemSpecification.TextAttributePrefix);

    /// <summary>To text attribute field name</summary>
    /// <param name="attribute">The attribute</param>
    /// <returns>Text attribute name</returns>
    public static string ToTextAttributeField(this string attribute) =>
        ToAttributeField(attribute, SystemSpecification.TextAttributePrefix);

    /// <summary>Test for text attribute field name</summary>
    /// <param name="attribute">The attribute</param>
    /// <returns>True for a date attribute field</returns>
    public static bool IsDateAttributeField(this string attribute) =>
        attribute.StartsWith(SystemSpecification.DateAttributePrefix);

    /// <summary>To date attribute field name</summary>
    /// <param name="attribute">The attribute</param>
    /// <returns>Date attribute name</returns>
    public static string ToDateAttributeField(this string attribute) =>
        ToAttributeField(attribute, SystemSpecification.DateAttributePrefix);

    /// <summary>Test for numeric attribute field name</summary>
    /// <param name="attribute">The attribute</param>
    /// <returns>True for a numeric attribute field</returns>
    public static bool IsNumericAttributeField(this string attribute) =>
        attribute.StartsWith(SystemSpecification.NumericAttributePrefix);

    /// <summary>To numeric attribute field name</summary>
    /// <param name="attribute">The attribute</param>
    /// <returns>String starting uppercase</returns>
    public static string ToNumericAttributeField(this string attribute) =>
        ToAttributeField(attribute, SystemSpecification.NumericAttributePrefix);

    /// <summary>Remove the attribute field name prefix</summary>
    /// <param name="attribute">The attribute</param>
    /// <returns>Attribute name without prefix</returns>
    public static string RemoveAttributePrefix(this string attribute)
    {
        if (IsTextAttributeField(attribute))
        {
            return attribute.RemoveFromStart(SystemSpecification.TextAttributePrefix);
        }
        if (IsDateAttributeField(attribute))
        {
            return attribute.RemoveFromStart(SystemSpecification.DateAttributePrefix);
        }
        if (IsNumericAttributeField(attribute))
        {
            return attribute.RemoveFromStart(SystemSpecification.NumericAttributePrefix);
        }
        return attribute;
    }

    private static string ToAttributeField(this string attribute, string prefix)
    {
        if (string.IsNullOrWhiteSpace(attribute))
        {
            throw new ArgumentException(null, nameof(attribute));
        }
        return attribute.EnsureStart(prefix);
    }
}