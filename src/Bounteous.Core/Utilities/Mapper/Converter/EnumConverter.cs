﻿using System;

namespace Bounteous.Core.Utilities.Mapper.Converter;

public class EnumConverter<T> : AbstractValueConverter<T>
{
    protected override T InternalConvert(string input)
    {
        try
        {
            return (T)Enum.Parse(typeof(T), input);
        }
        catch
        {
            return default;
        }
    }
}