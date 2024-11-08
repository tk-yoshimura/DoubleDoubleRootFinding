using DoubleDouble;

namespace DoubleDoubleRootFinding {
    public static class BrentFinder {
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

            ddouble y1 = f(x1), y2 = f(x2), xc = 0d, yc = 0d;

            if (y1 == 0d) {
                return x1;
            }
            if (y2 == 0d) {
                return x2;
            }

            if (ddouble.Sign(y1) == ddouble.Sign(y2)) {
                throw new ArithmeticException($"invalid interval: sgn(f(x1)) == sgn(f(x2))");
            }

            ddouble dx_prev = 0d, dx = 0d;

            while (iters != 0) {
                if (y1 != 0d && y2 != 0d && (ddouble.Sign(y1) != ddouble.Sign(y2))) {
                    (xc, yc) = (x1, y1);
                    dx_prev = dx = x2 - x1;
                }

                if (ddouble.Abs(yc) < ddouble.Abs(y2)) {
                    (x2, x1, xc) = (xc, x2, x2);
                    (y2, y1, yc) = (yc, y2, y2);
                }

                ddouble delta = ddouble.Max(ddouble.Epsilon, eps * ddouble.Abs(x2));
                ddouble dx_bisect = ddouble.Ldexp(xc - x2, -1), dx_interp;

                if (y2 == 0d || !(ddouble.Abs(dx_bisect) >= delta)) {
                    return x2;
                }

                if (ddouble.Abs(dx_prev) > delta && ddouble.Abs(y2) < ddouble.Abs(y1)) {
                    if (x1 != xc) {
                        ddouble rab = (y1 - y2) / (x1 - x2);
                        ddouble rcb = (yc - y2) / (xc - x2);
                        dx_interp = -y2 * (yc * rcb - y1 * rab) / (rcb * rab * (yc - y1));
                    }
                    else {
                        dx_interp = -y2 * (x2 - x1) / (y2 - y1);
                    }

                    if (ddouble.Ldexp(ddouble.Abs(dx_interp), 1) < ddouble.Min(ddouble.Abs(dx_prev), 3d * ddouble.Abs(dx_bisect) - delta)) {
                        dx_prev = dx;
                        dx = dx_interp;
                    }
                    else {
                        dx_prev = dx = dx_bisect;
                    }
                }
                else {
                    dx_prev = dx = dx_bisect;
                }

                (x1, y1) = (x2, y2);

                if (ddouble.Abs(dx) > delta) {
                    x2 += dx;
                }
                else {
                    if (!ddouble.IsFinite(dx_bisect)) {
                        return x2;
                    }

                    x2 += dx_bisect > 0d ? delta : -delta;
                }

                y2 = f(x2);

                iters = int.Max(-1, iters - 1);
            }

            return x2;
        }
    }
}
