using System;

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
    }
}
