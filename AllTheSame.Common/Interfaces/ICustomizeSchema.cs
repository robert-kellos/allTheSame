using System;
using System.Collections.Generic;
using System.Linq;

namespace AllTheSame.Common.Interfaces
{
    /// <summary>
    ///     Provides customizations for producing documentation for this type
    /// </summary>
    public interface ICustomizeSchema
    {
        /// <summary>
        ///     List of property names to exclude from schema generation
        /// </summary>
        /// <returns></returns>
        List<string> GetExcludeProperties();
    }

    /// <summary>
    /// </summary>
    public static class CustomizeSchemaExtentions
    {
        /// <summary>
        ///     Excludes the type of the properties of.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="excludeType">Type of the exclude.</param>
        /// <param name="excludeProps">The exclude props.</param>
        public static void ExcludePropertiesOfType(this ICustomizeSchema instance, Type excludeType,
            List<string> excludeProps)
        {
            var addrType = instance.GetType();
            excludeProps.AddRange(from propInfo in addrType.GetProperties()
                where (propInfo.PropertyType.IsGenericType &&
                       propInfo.PropertyType.GetGenericTypeDefinition() == excludeType) ||
                      propInfo.PropertyType == excludeType
                select propInfo.Name);
        }
    }
}