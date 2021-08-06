using Pastel;
using System;
using System.Reflection;

namespace RDK.Core.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsSubclassOfGeneric(this Type type, Type genericType)
        {
            Type baseType = type.BaseType;

            while (baseType != null && !baseType.IsValueType)
            {
                if (baseType.IsGenericType && baseType.GetGenericTypeDefinition() == genericType)
                {
                    return true;
                }

                baseType = baseType.BaseType;
            }

            return false;
        }

        /// <summary>
        /// Return a colored Type.FullName string using Pastel NuGet pkg for the colors
        /// </summary>
        /// <param name="input">Is the Type of the generic</param>
        /// <returns></returns>
        public static string FullName(this Assembly input) => $"{input.FullName}".Pastel("#8a5afa");

        public static string FullName(this Type input) => $"{input.FullName}".Pastel("#8a5afa");

        /// <summary>
        /// Return a colored Type.Name string using Pastel NuGet pkg for the colors
        /// </summary>
        /// <param name="input">Is the Type of the generic</param>
        /// <returns></returns>
        public static string Name(this Assembly input) => $"{input.GetName()}".Pastel("#8a5afa");

        public static string Name(this Type input) => $"{input.Name}".Pastel("#8a5afa");
    }
}
