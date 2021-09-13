using System.Reflection;

namespace RDK.Core.Reflection
{
    public static class Generator
    {
        public static T InstanciateConstructorWithParams<T>(Type[] parameterTypeList, params object[] valuesOnParameter)
        {
            object generatedParams = new();
            Type generatedClass = typeof(T);

            ConstructorInfo newConstructor = generatedClass.GetConstructor(parameterTypeList);

            if (newConstructor == null)
            {
                Console.WriteLine($"No constructors found for: {generatedClass}");
                return default;
            }

            foreach (Type parameterType in parameterTypeList)
            {
                // Verify if the new constructor doesn't need a parameter so can create an instances without a parameter.
                if (newConstructor != null)
                {
                    ParameterInfo[] newParameters = newConstructor.GetParameters();

                    foreach (ParameterInfo currentParameter in newParameters)
                    {
                        generatedParams = Activator.CreateInstance(generatedClass, valuesOnParameter);
                    }
                }
                else
                {
                    generatedParams = Activator.CreateInstance(generatedClass);
                }
            }

            return (T) generatedParams;
        }
    }
}
