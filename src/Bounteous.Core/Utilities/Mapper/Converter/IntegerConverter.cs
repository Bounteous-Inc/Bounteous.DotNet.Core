﻿namespace Bounteous.Core.Utilities.Mapper.Converter;

public class IntegerConverter : AbstractValueConverter<int?>
{
    protected override int? InternalConvert(string input)
        => string.IsNullOrEmpty(input) ? null : int.Parse(input);
}