using System;

namespace Bounteous.Core.Utilities;

public static class ConditionExtensions
{
    public static bool Between<T>(this T item, T lower, T upper) where T : IComparable
        => item.CompareTo(lower) >= 0 && item.CompareTo(upper) <= 0;
}