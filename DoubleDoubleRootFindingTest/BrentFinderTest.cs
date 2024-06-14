using DoubleDouble;
using DoubleDoubleRootFinding;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoubleDoubleRootFindingTest {
    [TestClass]
    public class BrentFinderTest {
        [TestMethod]
        public void CubicTest() {
            static ddouble f(ddouble x) {
                return (x + 3) * ddouble.Square(x - 1);
            }

            ddouble y = BrentFinder.RootFind(f, x1: -4, x2: 4d / 3);

            Assert.IsTrue(y - (-3) < 1e-30);
        }

        [TestMethod]
        public void CbrtTest() {
            static ddouble f(ddouble x) {
                return x * x * x - 2;
            }

            ddouble y = BrentFinder.RootFind(f, x1: 1, x2: 2);

            Assert.IsTrue(y - ddouble.Cbrt(2) < 1e-30);
        }

        [TestMethod]
        public void InvGammaTest() {
            static ddouble f(ddouble x) {
                return ddouble.Gamma(x) - 63;
            }

            ddouble y = BrentFinder.RootFind(f, x1: 4, x2: 7);

            Assert.IsTrue(y - ddouble.InverseGamma(63) < 1e-30);
        }

        [TestMethod]
        public void InvErfTest() {
            static ddouble f(ddouble x) {
                return ddouble.Erf(x) - 0.75;
            }

            ddouble y = BrentFinder.RootFind(f, x1: 0, x2: 4);

            Assert.IsTrue(y - ddouble.InverseErf(0.75) < 1e-30);
        }
    }
}