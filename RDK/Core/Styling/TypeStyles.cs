using Pastel;

namespace RDK.Core.Styling
{
    public class TypeStyles<T>
    {
        // TODO: Dictionary to save custom colors and assign them into the respective Method by the name.

        private static T instance;

        private static T Instance
        {
            get { return instance; }
            set { instance = value; }
        }

        /// <summary>
        /// Return a colored Type.FullName string using Pastel NuGet pkg for the colors
        /// </summary>
        /// <param name="input">Is the Type of the generic</param>
        /// <returns></returns>
        public static string T_FullName(T input)
        {
            instance = input;
            return $"{Instance.GetType().FullName}".Pastel("#8a5afa");
        }

        /// <summary>
        /// Return a colored Type.Name string using Pastel NuGet pkg for the colors
        /// </summary>
        /// <param name="input">Is the Type of the generic</param>
        /// <returns></returns>
        public static string T_Name(T input)
        {
            instance = input;
            return $"{Instance.GetType().Name}".Pastel("#8a5afa");
        }
    }
}
