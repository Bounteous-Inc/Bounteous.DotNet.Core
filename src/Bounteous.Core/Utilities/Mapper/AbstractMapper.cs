﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Bounteous.Core.Extensions;
using Bounteous.Core.Utilities.Mapper.Converter;
using Bounteous.Core.Validations;

namespace Bounteous.Core.Utilities.Mapper;

public abstract class AbstractMapper<TFrom, TO> : IMapper<TFrom, TO>
    where TFrom : class
    where TO : class
{
    private static readonly Dictionary<Type, IValueConverter> Converters =
        new()
        {
            { typeof(DateTime?), new NullableDateTimeConverter() },
            { typeof(DateTime), new DateTimeConverter() },
            { typeof(string), new StringConverter() },
            { typeof(decimal?), new DecimalConverter() },
            { typeof(double?), new DoubleConverter() },
            { typeof(int?), new IntegerConverter() }
        };

    protected AbstractMapper() => InternalInitialize();

    public List<IPropertyMapper> PropertyMappers { get; } = [];

    public List<IClassMapper<TFrom, TO>> ClassMappers { get; } = [];

    public TO Build(TFrom input) => BuildInternal(input, Create());

    protected abstract void Initialize();
    protected abstract TO Create();

    private void InternalInitialize() => Initialize();

    protected void Map<T>(Expression<Func<TFrom, object>> source, Expression<Func<TO, T>> target)
    {
        var sourceProperty = source.GetProperty();
        var targetProperty = target.GetProperty();
        Map(source, target, GetValueConverter<T>(sourceProperty, targetProperty));
    }

    protected static IValueConverter<T> GetValueConverter<T>(PropertyInfo source, PropertyInfo target)
        => source.PropertyType == target.PropertyType
            ? new DirectConverter<T>()
            : GetConverterFromMap<T>();

    private static IValueConverter<T> GetConverterFromMap<T>()
    {
        var targetType = typeof(T);
        var hasConverter = Converters.TryGetValue(targetType, out var converter);
        Validate.Begin().IsTrue(hasConverter,
                $"There is no converter for type {targetType}.  Must provide a custom converter")
            .Check();
        return (IValueConverter<T>)converter;
    }

    protected void Map<T>(Expression<Func<TFrom, object>> sourceProperty, Expression<Func<TO, T>> targetProperty,
        IValueConverter<T> converter)
        => Map(sourceProperty.GetProperty(), targetProperty.GetProperty(), converter);
    
    protected void Map<T>(Expression<Func<TO, T>> targetProperty, T value)
    {
        var target = targetProperty.GetProperty();
        PropertyMappers.Add(new ValueMapper<T>(target, value));
    }

    protected void Map(IClassMapper<TFrom, TO> action)
        => ClassMappers.Add(action);

    private void Map<T>(PropertyInfo source, PropertyInfo target, IValueConverter<T> converter)
        => PropertyMappers.Add(new PropertyMapper<T>(source, target, converter));

    private TO BuildInternal(TFrom input, TO to)
    {
        foreach (var applier in PropertyMappers) applier.Apply(input, to);
        foreach (var action in ClassMappers) action.Apply(input, to);
        return to;
    }
}