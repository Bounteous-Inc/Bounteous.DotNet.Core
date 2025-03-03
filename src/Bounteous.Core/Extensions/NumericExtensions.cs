using System;

namespace Bounteous.Core.Extensions;

public static class NumericExtensions
{
    public static double RoundedTo(this double value, int precision)
        => Math.Round(value, precision, MidpointRounding.AwayFromZero);
}