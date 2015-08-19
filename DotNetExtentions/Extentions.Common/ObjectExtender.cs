using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Extentions.Common
{
    public static class ObjectExtender
    {
        /// <summary>
        /// Copy the equal property values from original object to receiver.
        /// Copy only value type properties (and string) with same name and type.
        /// You can use different object types.
        /// </summary>
        /// <param name="original">Source</param>
        /// <param name="receiver">Destination</param>
        public static void MapProperties(this object original, object receiver)
        {
            List<PropertyInfo> originalProps = original.GetType().GetProperties().ToList();
            List<PropertyInfo> receiverProps = receiver.GetType().GetProperties().ToList();

            foreach (PropertyInfo prop in originalProps)
            {
                if (receiverProps.Exists(p => p.Name == prop.Name
                    && p.PropertyType.IsAssignableFrom(prop.PropertyType))
                    && (prop.PropertyType.IsValueType || prop.PropertyType == typeof(string)))
                {
                    string name = prop.Name;
                    object value = original.GetType().GetProperty(name).GetValue(original, null);
                    receiver.GetType().GetProperty(name).SetValue(receiver, value, null);
                }
            }
        }

        public static void CopyPropertiesTo<T>(this T source, T destination)
        {
            PropertyInfo[] destinationProperties = destination.GetType().GetProperties();
            foreach (PropertyInfo destinationProperty in destinationProperties)
            {
                if (destinationProperty.CanWrite)
                {
                    PropertyInfo sourceProperty = source.GetType().GetProperty(destinationProperty.Name);
                    if (sourceProperty.PropertyType.IsValueType || sourceProperty.PropertyType == typeof(string))
                    {
                        destinationProperty.SetValue(destination, sourceProperty.GetValue(source, null), null);
                    }
                }
            }
        }

        public static string ToStringOrNull(this object o)
        {
            if (o == null)
            {
                return null;
            }
            else
            {
                return o.ToString();
            }
        }

        public static int? ToIntOrNull(this object o)
        {
            if (o == null)
            {
                return null;
            }
            else
            {
                int value;
                if (int.TryParse(o.ToString(), out value))
                {
                    return value;
                }
                else
                {
                    return null;
                }
            }
        }

        public static decimal? ToDecimalOrNull(this object o)
        {
            if (o == null)
            {
                return null;
            }
            else
            {
                decimal value;
                if (decimal.TryParse(o.ToString(), out value))
                {
                    return value;
                }
                else
                {
                    return null;
                }
            }
        }

        public static DateTime? ToDateOrNull(this object o)
        {
            if (o == null)
            {
                return null;
            }
            else
            {
                DateTime value;
                if (DateTime.TryParse(o.ToString(), out value))
                {
                    return value;
                }
                else
                {
                    return null;
                }
            }
        }
        
        // TODO: Move to another file
        public static string UpFirstLetter(this string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException("UpFirstLettr");
            }
            if (string.IsNullOrEmpty(str.Trim()))
            {
                throw new ArgumentException("UpFirstLettr: argument is null or empty");
            }

            return str.First().ToString().ToUpper() + string.Join("", str.Skip(1));
        }
    }
}
