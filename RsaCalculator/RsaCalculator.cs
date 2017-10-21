namespace RsaCalculator
{
    using System;
    using System.Numerics;

    /// <summary>
    /// This class implements a simple RSA calculator.
    /// </summary>
    public class RsaCalculator
    {
        /// <summary>
        /// First prime number.
        /// </summary>
        /// <remarks>Leaking P and Q is like leaking the private exponent D.</remarks>
        public BigInteger P { get; private set; }

        /// <summary>
        /// Second prime number.
        /// </summary>
        /// <remarks>Leaking P and Q is like leaking the private exponent D.</remarks>
        public BigInteger Q { get; private set; }

        /// <summary>
        /// Product of P and Q.
        /// </summary>
        /// <remarks>N is part of the public key and can be safely shared.</remarks>
        public BigInteger N { get; private set; }

        /// <summary>
        /// φ(n) = (p - 1) * (q - 1).
        /// </summary>
        /// <remarks>Leaking PhiN is like leaking the private exponent D.</remarks>
        public BigInteger PhiN { get; private set; }

        /// <summary>
        /// Public exponent.
        /// </summary>
        /// <remarks>E is part of the public key and can be safely shared.</remarks>
        public BigInteger E { get; private set; }

        /// <summary>
        /// Private exponent.
        /// </summary>
        /// <remarks>Leaking the private exponent D is leaking the private key.</remarks>
        public BigInteger D { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RsaCalculator"/> class. 
        /// </summary>
        /// <remarks>
        /// As of today, this methods is not supported.
        /// </remarks>
        public RsaCalculator()
        {
            throw new NotSupportedException("Full computation of keys not supported. You should manually choose P, Q, and E.");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RsaCalculator"/> class. 
        /// </summary>
        /// <param name="p">
        /// A large prime number for P.
        /// </param>
        /// <param name="q">
        /// A large prime number for Q.
        /// </param>
        /// <param name="e">
        /// A large number such that that 1 &lt; e &lt; φ(n), and such that and e and n are coprime.
        /// </param>
        /// <exception cref="System.ArithmeticException">
        /// Values of e and n are not coprime.
        /// </exception>
        public RsaCalculator(BigInteger p, BigInteger q, BigInteger e)
        {
            P = p;
            Q = q;
            E = e;

            N = P * Q;
            PhiN = (P - 1) * (Q - 1);
            D = BigIntegerExtension.ModularInverse(E, PhiN);
        }

        /// <summary>
        /// Apply the public key e, n to message m.
        /// </summary>
        /// <param name="m">The message.</param>
        /// <returns>The process message.</returns>
        public BigInteger ApplyPublicKey(BigInteger m)
        {
            return BigInteger.ModPow(m, E, N);
        }

        /// <summary>
        /// Apply the private key d, n to message m.
        /// </summary>
        /// <param name="m">The message.</param>
        /// <returns>The process message.</returns>
        public BigInteger ApplyPrivateKey(BigInteger m)
        {
            return BigInteger.ModPow(m, D, N);
        }

        /// <summary>
        /// Gets a string representing the RSA calculator.
        /// </summary>
        /// <returns>A string representing the RSA calculator.</returns>
        public override string ToString()
        {
            return N.ToString();
        }
    }
}
