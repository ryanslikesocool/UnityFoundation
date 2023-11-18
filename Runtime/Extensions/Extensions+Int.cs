
namespace Foundation {
    public static partial class Extensions {
        public static int Wrap(this int input, int min, int max, bool exclusive = true) {
            if (min >= max) {
                throw new System.ArgumentOutOfRangeException("'min' is equal to or greater than 'max'");
            }

            int delta = max - min;

            while (input < min) {
                input += delta;
            }
            while (input > max) {
                input -= delta;
            }

            return input;
        }
    }
}