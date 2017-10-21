namespace RsaCalculatorTests
{
    using System;
    using System.Numerics;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RsaCalculator;

    [TestClass]
    public class BigIntegerExtensionTests
    {
        [TestMethod]
        public void ExtendedEuclidTest_OneOne()
        {
            var u = new BigInteger(1);
            var v = new BigInteger(1);

            var result = BigIntegerExtension.ExtendedEuclid(u, v);

            Assert.AreEqual(0, result.Item1);
            Assert.AreEqual(1, result.Item2);
            Assert.AreEqual(1, result.Item3);
        }

        [TestMethod]
        public void ExtendedEuclidTest_TwoTwo()
        {
            var u = new BigInteger(2);
            var v = new BigInteger(2);

            var result = BigIntegerExtension.ExtendedEuclid(u, v);

            Assert.AreEqual(0, result.Item1);
            Assert.AreEqual(1, result.Item2);
            Assert.AreEqual(2, result.Item3);
        }

        [TestMethod]
        public void ExtendedEuclidTest_TwoPrimes()
        {
            var u = new BigInteger(7823);
            var v = new BigInteger(7919);

            var result = BigIntegerExtension.ExtendedEuclid(u, v);

            Assert.AreEqual(3877, result.Item1);
            Assert.AreEqual(-3830, result.Item2);
            Assert.AreEqual(1, result.Item3);
        }

        [TestMethod]
        public void ExtendedEuclidTest_TwoProductsOfPrimes()
        {
            // Let 7703, 7607 and 7541 be three prime numbers.
            var u = new BigInteger(58088323); // 7703 * 7541
            var v = new BigInteger(57364387); // 7607 * 7541

            var result = BigIntegerExtension.ExtendedEuclid(u, v);

            Assert.AreEqual(1981, result.Item1);
            Assert.AreEqual(-2006, result.Item2);
            Assert.AreEqual(7541, result.Item3);
        }

        [TestMethod]
        public void ExtendedEuclidTest_DontComputeU2()
        {
            // Let 7703, 7607 and 7541 be three prime numbers.
            var u = new BigInteger(58088323); // 7703 * 7541
            var v = new BigInteger(57364387); // 7607 * 7541

            var result = BigIntegerExtension.ExtendedEuclid(u, v, false);

            Assert.AreEqual(1981, result.Item1);
            Assert.AreEqual(0, result.Item2);
            Assert.AreEqual(7541, result.Item3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ExtendedEuclidTest_UNotPositive()
        {
            var u = new BigInteger(0);
            var v = new BigInteger(1);

            BigIntegerExtension.ExtendedEuclid(u, v);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ExtendedEuclidTest_VNotPositive()
        {
            var u = new BigInteger(1);
            var v = new BigInteger(0);

            BigIntegerExtension.ExtendedEuclid(u, v);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FromBigEndianByteArrayTest_ThrowsArgumentNullException()
        {
            BigIntegerExtension.FromBigEndianByteArray(null);
        }

        [TestMethod]
        public void FromBigEndianByteArrayTest_Empty()
        {
            var array = new byte[] { };
            var value = BigIntegerExtension.FromBigEndianByteArray(array);
            Assert.AreEqual(0, value);
        }

        [TestMethod]
        public void FromBigEndianByteArrayTest_Zero()
        {
            var array = new byte[] { 0x00 };
            var value = BigIntegerExtension.FromBigEndianByteArray(array);
            Assert.AreEqual(0, value);
        }

        [TestMethod]
        public void FromBigEndianByteArrayTest_One()
        {
            var array = new byte[] { 0x01 };
            var value = BigIntegerExtension.FromBigEndianByteArray(array);
            Assert.AreEqual(1, value);
        }

        [TestMethod]
        public void FromBigEndianByteArrayTest_BigValue1()
        {
            var array = new byte[] { 0x3f, 0x88, 0xc9, 0x8c, 0x42, 0xe4, 0x79, 0xde };
            var value = BigIntegerExtension.FromBigEndianByteArray(array);
            Assert.AreEqual(4578130625476983262, value);
        }

        [TestMethod]
        public void FromBigEndianByteArrayTest_BigValue2()
        {
            var array = new byte[] { 0xba, 0xad, 0x87, 0xcb, 0x64, 0x27, 0x29, 0xf0 };
            var value = BigIntegerExtension.FromBigEndianByteArray(array);
            Assert.AreEqual(-4995187104055612944, value);
        }

        [TestMethod]
        public void ToBigEndianByteArrayTest_Zero()
        {
            var value = new BigInteger(0);
            var array = BigIntegerExtension.ToBigEndianByteArray(value);

            Assert.IsTrue(array.Length == 1);
            Assert.IsTrue(array[0] == 0);
        }

        [TestMethod]
        public void ToBigEndianByteArrayTest_One()
        {
            var value = new BigInteger(1);
            var array = BigIntegerExtension.ToBigEndianByteArray(value);

            Assert.IsTrue(array.Length == 1);
            Assert.IsTrue(array[0] == 1);
        }

        [TestMethod]
        public void ToBigEndianByteArrayTest_BigValue1()
        {
            var value = new BigInteger(-8726769014922317791);
            var array = BigIntegerExtension.ToBigEndianByteArray(value);

            Assert.IsTrue(array.Length == 8);
            Assert.IsTrue(array[0] == 0x86);
            Assert.IsTrue(array[1] == 0xE4);
            Assert.IsTrue(array[2] == 0x49);
            Assert.IsTrue(array[3] == 0xD1);
            Assert.IsTrue(array[4] == 0x3C);
            Assert.IsTrue(array[5] == 0xAB);
            Assert.IsTrue(array[6] == 0x70);
            Assert.IsTrue(array[7] == 0x21);
        }

        [TestMethod]
        public void ToBigEndianByteArrayTest_BigValue2()
        {
            var value = new BigInteger(904637443760610831);
            var array = BigIntegerExtension.ToBigEndianByteArray(value);

            Assert.IsTrue(array.Length == 8);
            Assert.IsTrue(array[0] == 0x0C);
            Assert.IsTrue(array[1] == 0x8D);
            Assert.IsTrue(array[2] == 0xEA);
            Assert.IsTrue(array[3] == 0xF6);
            Assert.IsTrue(array[4] == 0x4D);
            Assert.IsTrue(array[5] == 0xDE);
            Assert.IsTrue(array[6] == 0x5E);
            Assert.IsTrue(array[7] == 0x0F);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ModularInverseTest_ZeroZero()
        {
            var value = new BigInteger(0);
            var modulus = new BigInteger(0);
            BigIntegerExtension.ModularInverse(value, modulus);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ModularInverseTest_ZeroOne()
        {
            var value = new BigInteger(0);
            var modulus = new BigInteger(1);
            BigIntegerExtension.ModularInverse(value, modulus);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ModularInverseTest_OneZero()
        {
            var value = new BigInteger(1);
            var modulus = new BigInteger(0);
            BigIntegerExtension.ModularInverse(value, modulus);
        }

        [TestMethod]
        public void ModularInverseTest_OneOne()
        {
            var value = new BigInteger(1);
            var modulus = new BigInteger(1);
            var result = BigIntegerExtension.ModularInverse(value, modulus);

            Assert.AreEqual(new BigInteger(0), result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void ModularInverseTest_NotRelativelyPrime()
        {
            var value = new BigInteger(18);
            var modulus = new BigInteger(60);
            BigIntegerExtension.ModularInverse(value, modulus);
        }

        [TestMethod]
        public void ModularInverseTest_RelativelyPrime()
        {
            var value = new BigInteger(12);
            var modulus = new BigInteger(25);
            var result = BigIntegerExtension.ModularInverse(value, modulus);

            Assert.AreEqual(new BigInteger(23), result);
        }


        [TestMethod]
        public void ModularInverseTest_Prime()
        {
            var value = new BigInteger(881);
            var modulus = new BigInteger(991);
            var result = BigIntegerExtension.ModularInverse(value, modulus);

            Assert.AreEqual(new BigInteger(9), result);
        }
    }
}