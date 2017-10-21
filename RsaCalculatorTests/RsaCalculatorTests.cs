namespace RsaCalculatorTests
{
    using System.Numerics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RsaCalculator;

    [TestClass]
    public class RsaCalculatorTests
    {
        [TestMethod]
        public void RsaCalculator_Create()
        {
            var p = new BigInteger(26898370231697);
            var q = new BigInteger(29996224275833);
            var e = new BigInteger(179424673);

            var rsa = new RsaCalculator(p, q, e);
            
            Assert.AreEqual(new BigInteger(26898370231697), rsa.P);
            Assert.AreEqual(new BigInteger(29996224275833), rsa.Q);
            Assert.AreEqual(new BigInteger(179424673), rsa.E);

            var dExpectedValue = new BigInteger(new byte[] {0xe1, 0x52, 0xe1, 0xf1, 0x50, 0x86, 0x2d, 0xc2, 0x67, 0xb0, 0x21, 0x01});
            Assert.AreEqual(dExpectedValue, rsa.D);

            var nExpectedValue = new BigInteger(new byte[] {0x89, 0x0e, 0x5d, 0x53, 0x5f, 0x09, 0xf2, 0x89, 0x09, 0x69, 0x9b, 0x02});
            Assert.AreEqual(nExpectedValue, rsa.N);

            var phiNExpectedValue = new BigInteger(new byte[] { 0x80, 0xab, 0x44, 0x85, 0xa0, 0xd5, 0xf1, 0x89, 0x09, 0x69, 0x9b, 0x02 });
            Assert.AreEqual(phiNExpectedValue, rsa.PhiN);
        }

        [TestMethod]
        public void RsaCalculator_PrivatePublic()
        {
            var p = new BigInteger(29996224275821);
            var q = new BigInteger(29996224275791);
            var e = new BigInteger(523);

            var rsa = new RsaCalculator(p, q, e);

            var randomMessage = new BigInteger(new byte[]
            {
                0x4e, 0x25, 0x06, 0x64, 0x5e, 0x2f, 0x39, 0x04, 0x15, 0x30, 0x3b
            });

            var cipheredMessage = rsa.ApplyPrivateKey(randomMessage);
            var plaintextMessage = rsa.ApplyPublicKey(cipheredMessage);

            Assert.AreEqual(randomMessage, plaintextMessage);
        }

        [TestMethod]
        public void RsaCalculator_PublicPrivate()
        {
            var p = new BigInteger(29996224275821);
            var q = new BigInteger(29996224275791);
            var e = new BigInteger(523);

            var rsa = new RsaCalculator(p, q, e);

            var randomMessage = new BigInteger(new byte[]
            {
                0x4e, 0x25, 0x06, 0x64, 0x5e, 0x2f, 0x39, 0x04, 0x15, 0x30, 0x3b
            });

            var cipheredMessage = rsa.ApplyPublicKey(randomMessage);
            var plaintextMessage = rsa.ApplyPrivateKey(cipheredMessage);

            Assert.AreEqual(randomMessage, plaintextMessage);
        }

        [TestMethod]
        public void RsaCalculator_ToString()
        {
            var p = new BigInteger(29996224275821);
            var q = new BigInteger(29996224275791);
            var e = new BigInteger(523);

            var rsa = new RsaCalculator(p, q, e);

            Assert.IsTrue(rsa.ToString().Contains("899773470804453189156949411"));
        }
    }
}
