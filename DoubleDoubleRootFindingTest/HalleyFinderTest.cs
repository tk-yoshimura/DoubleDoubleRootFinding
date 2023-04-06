using DoubleDouble;
using DoubleDoubleRootFinding;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoubleDoubleRootFindingTest {
    [TestClass]
    public class HalleyFinderTest {
        [TestMethod]
        public void CbrtTest() {
            static (ddouble v, ddouble d1, ddouble d2) f(ddouble x) {
                return (x * x * x - 2, 3 * x * x, 6 * x);
            }

            ddouble y = HalleyFinder.RootFind(f, x0: 2);

            Assert.IsTrue(y - ddouble.Cbrt(2) < 1e-30);
        }
    }
}