namespace RsaCalculator
{
    using System;
    using System.Numerics;

    /// <summary>
    /// This class implements some BigInteger modular arithmetic routines.
    /// </summary>
    public static class BigIntegerExtension
    {
        /// <summary>
        /// Determines a vector (u1,u2,u3) such that u * u1 + v * u2 = u3 = gcd(u, v).
        /// </summary>
        /// <param name="u">A positive integer.</param>
        /// <param name="v">A positive integer.</param>
        /// <param name="computeU2">false, if u2 shouldn't be computed, in which case it is set to 0.</param>
        /// <returns>A tuple containing (u1,u2,u3).</returns>
        /// <remarks>
        /// This implements "Extended Euclid's algorithm".
        /// For more information refer to Algorithm X,
        /// page 342,
        /// section 4.5.2 "The greatest common divisor",
        /// volume 2 "Seminumerical Algorithms Third Edition",
        /// of "The Art of Computer Programming",
        /// by Donald Ervin Knuth
        /// (c) 1998 Addison-Wesley.
        /// </remarks>
        /// <exception cref="System.ArgumentOutOfRangeException">At least one of u or v is not a positive integer.</exception>
        public static Tuple<BigInteger, BigInteger, BigInteger> ExtendedEuclid(BigInteger u, BigInteger v, bool computeU2 = true)
        {
            if (u.Sign != 1)
            {
                throw new ArgumentOutOfRangeException("u", u, "Value must be positive");
            }

            if (v.Sign != 1)
            {
                throw new ArgumentOutOfRangeException("v", v, "Value must be positive");
            }

            var u1 = BigInteger.One;
            var u3 = u;

            var v1 = BigInteger.Zero;
            var v3 = v;

            while (!v3.IsZero)
            {
                var q = u3 / v3;

                var t1 = u1 - (v1 * q);
                u1 = v1;
                v1 = t1;

                var t3 = u3 - (v3 * q);
                u3 = v3;
                v3 = t3;
            }

            BigInteger u2;

            if (computeU2)
            {
                // We compute u2 here because it's much easier to get it once you got u1 and u3.
                u2 = (u3 - (u1 * u)) / v;
            }
            else
            {
                u2 = BigInteger.Zero;
            }

            return Tuple.Create(u1, u2, u3);
        }

        /// <summary>
        /// Computes the modular inverse of an integer with respect to the specified modulus.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="modulus">The modulus. A positive integer.</param>
        /// <returns>The modular inverse of value with respect to modulus.</returns>
        /// <remarks>The modular inverse exists only if value and modulus are mutually prime. i.e. their greatest common divisor is 1.</remarks>
        /// <exception cref="System.ArithmeticException">The modular inverse does not exist, value and modulus are not mutually prime.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The modulus is not positive.</exception>
        public static BigInteger ModularInverse(BigInteger value, BigInteger modulus)
        {
            if (modulus.Sign != 1)
            {
                throw new ArgumentOutOfRangeException("modulus", modulus, "Modulus must be positive");
            }

            var result = ExtendedEuclid(value, modulus, false);

            var u1 = result.Item1;
            var u3 = result.Item3; // Greatest common divisor

            if (!u3.IsOne)
            {
                throw new ArithmeticException("Numbers are not relatively prime.");
            }

            if (u1.Sign == -1)
            {
                u1 += modulus;
            }

            return u1;
        }

        /// <summary>
        /// Creates a BigInteger from a big-endian byte array.
        /// </summary>
        /// <param name="value">The byte array.</param>
        /// <returns>The BigInteger.</returns>
        /// <remarks>
        /// The source byte array is left untouched.
        /// Some other BigInteger constructors use big-endian byte arrays, whereas .Net's uses little-endian byte arrays.
        /// </remarks>
        public static BigInteger FromBigEndianByteArray(byte[] value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value", "Value cannot be null.");
            }

            var temp = new byte[value.Length];
            Array.Copy(value, temp, temp.Length);
            Array.Reverse(temp);

            return new BigInteger(temp);
        }

        public static byte[] ToBigEndianByteArray(BigInteger value)
        {
            var temp = value.ToByteArray();
            Array.Reverse(temp);

            return temp;
        }
    }
}
