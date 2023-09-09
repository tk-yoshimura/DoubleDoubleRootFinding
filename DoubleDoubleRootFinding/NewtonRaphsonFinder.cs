using DoubleDouble;

namespace DoubleDoubleRootFinding {
    public static class NewtonRaphsonFinder {

        public static ddouble RootFind(
            Func<ddouble, (ddouble v, ddouble d1)> f,
            ddouble x0,
            int iters = -1, double eps = 1e-30, bool overshoot_decay = true) {

            if (!(eps >= 1e-30)) {
                throw new ArgumentOutOfRangeException(nameof(eps), $"{nameof(eps)} >= 1e-30");
            }

            ddouble x = x0, dx;
            ddouble? dx_prev = null;

            bool convergenced = false;

            while (iters != 0) {
                dx = Delta(f, x);

                if (overshoot_decay && dx_prev is not null) {
                    if (ddouble.Sign(dx) != ddouble.Sign(dx_prev.Value)) {
                        dx = ddouble.Ldexp(dx, -1);
                    }
                }

                x += dx;
                dx_prev = dx;

                if (ddouble.IsZero(dx) || (Math.Abs((double)dx / (double)x) < eps)) {
                    if (convergenced) {
                        break;
                    }

                    convergenced = true;
                }
                else {
                    convergenced = false;
                }

                if (!ddouble.IsFinite(x)) {
                    break;
                }

                iters = Math.Max(-1, iters - 1);
            }

            return x;
        }

        public static ddouble RootFind(
            Func<ddouble, (ddouble v, ddouble d1)> f,
            ddouble x0, (ddouble min, ddouble max) xrange,
            int iters = -1, double eps = 1e-30, bool overshoot_decay = true) {

            if (!(eps >= 1e-30)) {
                throw new ArgumentOutOfRangeException(nameof(eps), $"{nameof(eps)} >= 1e-30");
            }
            if (!(xrange.min < xrange.max)) {
                throw new ArgumentException("Invalid range.", nameof(xrange));
            }

            ddouble x = x0, dx;
            ddouble? dx_prev = null;

            bool convergenced = false;

            while (iters != 0) {
                dx = Delta(f, x);

                if (overshoot_decay && dx_prev is not null) {
                    if (ddouble.Sign(dx) != ddouble.Sign(dx_prev.Value)) {
                        dx = ddouble.Ldexp(dx, -1);
                    }
                }

                x += dx;
                dx_prev = dx;

                if ((x == xrange.min && ddouble.Sign(dx) == -1) || (x == xrange.max && ddouble.Sign(dx) == +1)) {
                    return ddouble.NaN;
                }

                if (x < xrange.min) {
                    x = xrange.min;
                }
                else if (x > xrange.max) {
                    x = xrange.max;
                }

                if (ddouble.IsZero(dx) || (Math.Abs((double)dx / (double)x) < eps)) {
                    if (convergenced) {
                        break;
                    }

                    convergenced = true;
                }
                else {
                    convergenced = false;
                }

                if (!ddouble.IsFinite(x)) {
                    break;
                }

                iters = Math.Max(-1, iters - 1);
            }

            return x;
        }

        public static ddouble Delta(
            Func<ddouble, (ddouble v, ddouble d1)> f,
            ddouble x) {

            (ddouble v, ddouble d1) = f(x);

            ddouble dx = -v / d1;

            return dx;
        }
    }
}