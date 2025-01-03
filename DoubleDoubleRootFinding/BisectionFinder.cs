﻿using DoubleDouble;

namespace DoubleDoubleRootFinding {
    public static class BisectionFinder {
        public static ddouble RootFind(
            Func<ddouble, ddouble> f,
            ddouble x1, ddouble x2,
            int iters = -1, double eps = 1e-30) {

            if (!(eps >= 1e-30)) {
                throw new ArgumentOutOfRangeException(nameof(eps), $"{nameof(eps)} >= 1e-30");
            }

            if (!ddouble.IsFinite(x1) || !ddouble.IsFinite(x2)) {
                return ddouble.NaN;
            }

            bool convergenced = false;

            ddouble x = ddouble.Ldexp(x1 + x2, -1), y1 = f(x1), y2 = f(x2);
            
            if (ddouble.Sign(y1) == ddouble.Sign(y2)) {
                throw new ArithmeticException($"invalid interval: sgn(f(x1)) == sgn(f(x2))");
            }

            while (iters != 0) {
                (x1, y1, x2, y2, bool success) = Iteration(f, x1, y1, x2, y2);

                x = ddouble.Ldexp(x1 + x2, -1);
                ddouble dx = x1 - x2;

                if (ddouble.IsZero(dx) || (double.Abs((double)dx / (double)x) < eps)) {
                    if (convergenced) {
                        break;
                    }

                    convergenced = true;
                }
                else {
                    convergenced = false;
                }

                if (!success) {
                    break;
                }

                iters = int.Max(-1, iters - 1);
            }

            return x;
        }

        public static (ddouble x1, ddouble y1, ddouble x2, ddouble y2, bool success) Iteration(Func<ddouble, ddouble> f, ddouble x1, ddouble y1, ddouble x2, ddouble y2) {
            if (ddouble.Sign(y1) == ddouble.Sign(y2)) {
                return (x1, y1, x2, y2, success: false);
            }

            ddouble xc = ddouble.Ldexp(x1 + x2, -1);
            ddouble yc = f(xc);

            if (xc == x1 || xc == x2) {
                return (xc, yc, xc, yc, success: false);
            }

            if (ddouble.Sign(y1) == ddouble.Sign(yc)) {
                return (xc, yc, x2, y2, success: true);
            }
            else {
                return (x1, y1, xc, yc, success: true);
            }
        }
    }
}
