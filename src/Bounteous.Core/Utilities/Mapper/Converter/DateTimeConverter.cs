﻿using System;
using System.Globalization;

namespace Bounteous.Core.Utilities.Mapper.Converter;

public class DateTimeConverter : AbstractValueConverter<DateTime>
{
    public const string DateFormat = "yyyyMMdd";
    public const string DateTimeFormat = "yyyyMMdd-HH:mm:ss";
    private readonly string format;

    public DateTimeConverter(string format)
        => this.format = format;

    public DateTimeConverter() : this(DateFormat)
    {
    }

    public override DateTime Convert(object value)
        => value switch
        {
            null => DateTime.MinValue,
            DateTime time => time,
            _ => base.Convert(value)
        };

    protected override DateTime InternalConvert(string input)
        => string.IsNullOrEmpty(input)
            ? DateTime.MinValue
            : DateTime.ParseExact(input, format, CultureInfo.CurrentCulture);
}