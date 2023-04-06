using DoubleDouble;
using DoubleDoubleRootFinding;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoubleDoubleRootFindingTest {
    [TestClass]
    public class NewtonRaphsonFinderTest {
        [TestMethod]
        public void CbrtTest() {
            static (ddouble v, ddouble d) f(ddouble x) {
                return (x * x * x - 2, 3 * x * x);
            }

            ddouble y = NewtonRaphsonFinder.RootFind(f, x0: 2);

            Assert.IsTrue(y - ddouble.Cbrt(2) < 1e-30);
        }
    }
}