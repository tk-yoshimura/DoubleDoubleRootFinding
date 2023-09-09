using DoubleDouble;

namespace DoubleDoubleRootFinding {
    public static class SecantFinder {
        public static ddouble RootFind(
            Func<ddouble, ddouble> f,
            ddouble x0,
            int iters = -1, double eps = 1e-30, bool overshoot_decay = true, bool divergence_decay = true) {

            if (!(eps >= 1e-30)) {
                throw new ArgumentOutOfRangeException(nameof(eps), $"{nameof(eps)} >= 1e-30");
            }
            bool convergenced = false;

            ddouble x = x0;
            ddouble h = ddouble.Max(
                ddouble.Ldexp(1, -100),
                ddouble.Ldexp(ddouble.Abs(x), -4)
            );

            ddouble x_prev = x + h, y_prev = f(x_prev);

            ddouble? dx_prev = null;

            while (iters != 0) {
                ddouble y = f(x);
                ddouble dx = Delta(x, y, x_prev, y_prev);

                if (ddouble.IsNaN(dx)) {
                    break;
                }

                if (dx_prev is not null) {
                    if (divergence_decay && ddouble.Abs(dx) > ddouble.Abs(dx_prev.Value)) {
                        dx = ddouble.Sign(dx) * ddouble.Abs(dx_prev.Value) / 2;
                    }
                    if (overshoot_decay && ddouble.Sign(dx) != ddouble.Sign(dx_prev.Value)) {
                        dx = ddouble.Ldexp(dx, -1);
                    }
                }

                (x_prev, y_prev) = (x, y);

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

        public static ddouble Delta(ddouble x, ddouble y, ddouble x_prev, ddouble y_prev) {
            ddouble dx = y * (x_prev - x) / (y - y_prev);

            return dx;
        }
    }
}
