using System;
using System.Security.Cryptography;


namespace RDK.Core.Helpers
{
    public static class MathHelper
    {
        ///<summary>
        /// Represents a pseudo-random number generator, a device that produces random data.
        ///</summary>
        public static class CryptoRandom
        {
            private static RandomNumberGenerator RandNumGen { get; set; } = RandomNumberGenerator.Create();

            ///<summary>
            /// Fills the elements of a specified array of bytes with random numbers.
            ///</summary>
            ///<param name=”buffer”>An array of bytes to contain random numbers.</param>
            public static void GetBytes(byte[] buffer) => RandNumGen.GetBytes(buffer);

            ///<summary>
            /// Returns a random number between 0.0 and 1.0.
            ///</summary>
            public static double NextDouble()
            {
                byte[] b = new byte[4];
                RandNumGen.GetBytes(b);
                return (double)BitConverter.ToUInt32(b, 0) / uint.MaxValue;
            }

            ///<summary>
            /// Returns a random number within the specified range.
            ///</summary>
            ///<param name=”minValue”>The inclusive lower bound of the random number returned.</param>
            ///<param name=”maxValue”>The exclusive upper bound of the random number returned. maxValue must be greater than or equal to minValue.</param>
            public static int Next(int minValue, int maxValue) => (int)Math.Round(NextDouble() * (maxValue - minValue - 1)) + minValue;

            ///<summary>
            /// Returns a nonnegative random number.
            ///</summary>
            public static int Next() => Next(0, int.MaxValue);

            ///<summary>
            /// Returns a nonnegative random number less than the specified maximum
            ///</summary>
            ///<param name=”maxValue”>The inclusive upper bound of the random number returned. maxValue must be greater than or equal 0</param>
            public static int Next(int maxValue) => Next(0, maxValue);

            ///<summary>
            /// Fills an array of bytes with a cryptographically strong random sequence of nonzero values.
            ///</summary>
            public static void GetNonZeroBytes(byte[] data) => RandNumGen.GetNonZeroBytes(data);
        }

        public static class GuidGenerator
        {
            public static long Long() => Math.Abs(BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0));
            public static int Int() => Math.Abs(Guid.NewGuid().GetHashCode());
            public static string String() => Guid.NewGuid().ToString();
        }
    }
}
