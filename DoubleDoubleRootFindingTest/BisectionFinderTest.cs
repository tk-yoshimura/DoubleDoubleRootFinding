using DoubleDouble;
using DoubleDoubleRootFinding;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoubleDoubleRootFindingTest {
    [TestClass]
    public class BisectionFinderTest {
        [TestMethod]
        public void CbrtTest() {
            static ddouble f(ddouble x) {
                return x * x * x - 2;
            }

            ddouble y = BisectionFinder.RootFind(f, 1, 1.5);

            Assert.IsTrue(y - ddouble.Cbrt(2) < 1e-30);
        }
    }
}