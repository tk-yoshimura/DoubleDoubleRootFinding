using DoubleDouble;
using DoubleDoubleRootFinding;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoubleDoubleRootFindingTest {
    [TestClass]
    public class SecantFinderTest {
        [TestMethod]
        public void CbrtTest() {
            static ddouble f(ddouble x) {
                return x * x * x - 2;
            }

            ddouble y = SecantFinder.RootFind(f, x0: 1);

            Assert.IsTrue(y - ddouble.Cbrt(2) < 1e-30);
        }
    }
}