namespace Long.Kernel.Service
{
    public static class RandomService
    {
        /// <summary>
        ///     Returns the next random number from the generator.
        /// </summary>
        /// <param name="maxValue">One greater than the greatest legal return value.</param>
        public static Task<int> NextAsync(int maxValue)
        {
            return NextAsync(0, maxValue);
        }

        public static Task<double> NextRateAsync(double range)
        {
            return Services.Randomness.NextRateAsync(range);
        }

        /// <summary>Writes random numbers from the generator to a buffer.</summary>
        /// <param name="buffer">Buffer to write bytes to.</param>
        public static Task NextBytesAsync(byte[] buffer)
        {
            return Services.Randomness.NextBytesAsync(buffer);
        }

        /// <summary>
        ///     Returns the next random number from the generator.
        /// </summary>
        /// <param name="minValue">The least legal value for the Random number.</param>
        /// <param name="maxValue">One greater than the greatest legal return value.</param>
        public static Task<int> NextAsync(int minValue, int maxValue)
        {
            return Services.Randomness.NextIntegerAsync(minValue, maxValue);
        }

        public static async Task<bool> ChanceCalcAsync(int chance, int outOf)
        {
            return await NextAsync(outOf) < chance;
        }

        /// <summary>
        ///     Calculates the chance of success based in a rate.
        /// </summary>
        /// <param name="chance">Rate in percent.</param>
        /// <returns>True if the rate is successful.</returns>
        public static async Task<bool> ChanceCalcAsync(double chance)
        {
            const int divisor = 10_000_000;
            const int maxValue = 100 * divisor;
            return await NextAsync(0, maxValue) <= chance * divisor;
        }

        public static bool ChanceCalc(int chance, int outOf)
        {
            return Services.Randomness.NextInteger(0, outOf) < chance;
        }

        public static bool ChanceCalc(double chance)
        {
            const int divisor = 10_000_000;
            const int maxValue = 100 * divisor;
            return Services.Randomness.NextInteger(0, maxValue) <= chance * divisor;
        }
    }
}
