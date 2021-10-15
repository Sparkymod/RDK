using RDK.Core.Extensions;
using System;
using System.Reflection;

namespace RDK.Core.Reflection
{
    public abstract class Singleton<T> where T : class
    {
        /// <summary>
        ///   Returns the singleton instance.
        /// </summary>
        public static T Instance
        {
            get { return SingletonAllocator.instance; }
            protected set { SingletonAllocator.instance = value; }
        }

        #region Nested type: SingletonAllocator

        internal static class SingletonAllocator
        {
            internal static T instance;

            static SingletonAllocator()
            {
                CreateInstance(typeof(T));
            }

            // Create the instance of T
            public static T CreateInstance(Type type)
            {
                ConstructorInfo[] constructorInfos = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public);

                if (constructorInfos.Length > 0)
                {
                    return instance = (T)Activator.CreateInstance(type);
                }

                ConstructorInfo ctorNonPublic = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, Type.EmptyTypes, Array.Empty<ParameterModifier>());

                if (ctorNonPublic == null)
                {
                    throw new Exception(type.FullName + " doesn't have a private/protected constructor so the property cannot be enforced.");
                }

                try
                {
                    return instance = (T)ctorNonPublic.Invoke(Array.Empty<object>());
                }
                catch (Exception e)
                {
                    throw new Exception("The Singleton couldnt be constructed, check if " + type.FullName + " has a default constructor", e);
                }
            }
        }

        #endregion

        public override string ToString() => $"{Instance.GetType().FullName()}";
    }
}
