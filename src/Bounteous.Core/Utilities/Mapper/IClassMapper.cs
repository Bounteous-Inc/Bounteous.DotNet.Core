﻿namespace Bounteous.Core.Utilities.Mapper;

public interface IClassMapper<in TFrom, T>
{
    void Apply(TFrom from, T to);
}