using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Mono.Linq.Expressions;
using static System.Linq.Expressions.Expression;

namespace MultiMcLibrary;

internal delegate void Setter<in T>(T instance, object? value);
    
internal static class ReflectionUtils
{
    public static Setter<T> SetMemberInstance<T>(PropertyInfo prop)
    {
        var instance = ParameterInstance<T>();
        var value = Parameter(typeof(object), "value");

        var convertToType = prop.PropertyType.IsValueType
            ? value.Unbox(prop.PropertyType)
            : value.ConvertChecked(prop.PropertyType);

        return MakeMemberAccess(instance, prop).Assign(convertToType) // instance.name = (T) value
            .Lambda<Setter<T>>(instance, value) // (instance, value) => ...
            .Compile();
    }

    private static ParameterExpression ParameterInstance<T>() => Parameter(typeof(T), "instance");
}
    
public static class InstanceCfgParser
{
    private static readonly IReadOnlyDictionary<string, (PropertyInfo Info, Setter<InstanceCfg> Setter)> InstanceCfgProperties;

    static InstanceCfgParser()
    {
        var dict = new Dictionary<string, (PropertyInfo, Setter<InstanceCfg>)>();
            
        foreach (var prop in typeof(InstanceCfg).GetProperties().Where(e => e.GetCustomAttribute<CfgPropertyAttribute>() != null))
        {
            dict[prop.GetCustomAttribute<CfgPropertyAttribute>().PropertyName] = (prop, ReflectionUtils.SetMemberInstance<InstanceCfg>(prop));
        }

        InstanceCfgProperties = dict;
    }

    public static InstanceCfg Parse(IEnumerable<string> lines)
    {
        var instanceCfg = new InstanceCfg();
            
        foreach (var line in lines)
        {
            if (line.StartsWith("#") || line.StartsWith("//") || !line.Contains("="))
            {
                continue;
            }

            var idx = line.IndexOf('=');
            var key = line.Substring(0, idx);
            var value = line.Substring(idx + 1);

            if (!InstanceCfgProperties.TryGetValue(key, out var property))
            {
                // invalid key
                continue;
            }
                
            object? parsedValue = property.Info.PropertyType switch
            {
                { } t when t == typeof(byte) => byte.Parse(value),
                { } t when t == typeof(sbyte) => sbyte.Parse(value),
                { } t when t == typeof(short) => short.Parse(value),
                { } t when t == typeof(ushort) => ushort.Parse(value),
                { } t when t == typeof(int) => int.Parse(value),
                { } t when t == typeof(uint) => uint.Parse(value),
                { } t when t == typeof(long) => long.Parse(value),
                { } t when t == typeof(ulong) => ulong.Parse(value),
                { } t when t == typeof(float) => float.Parse(value),
                { } t when t == typeof(double) => double.Parse(value),
                { } t when t == typeof(decimal) => decimal.Parse(value),
                { } t when t == typeof(bool) => bool.Parse(value),
                { } t when t == typeof(nint) => (nint) long.Parse(value),
                { } t when t == typeof(nuint) => (nuint) ulong.Parse(value),
                { } t when t == typeof(char) => char.Parse(value),

                { } t when t == typeof(byte?) => (byte?) byte.Parse(value),
                { } t when t == typeof(sbyte?) => (sbyte?) sbyte.Parse(value),
                { } t when t == typeof(short?) => (short?) short.Parse(value),
                { } t when t == typeof(ushort?) => (ushort?) ushort.Parse(value),
                { } t when t == typeof(int?) => (int?) int.Parse(value),
                { } t when t == typeof(uint?) => (uint?) uint.Parse(value),
                { } t when t == typeof(long?) => (long?) long.Parse(value),
                { } t when t == typeof(ulong?) => (ulong?) ulong.Parse(value),
                { } t when t == typeof(float?) => (float?) float.Parse(value),
                { } t when t == typeof(double?) => (double?) double.Parse(value),
                { } t when t == typeof(decimal?) => (decimal?) decimal.Parse(value),
                { } t when t == typeof(bool?) => (bool?) bool.Parse(value),
                { } t when t == typeof(nint?) => (nint?) (nint) long.Parse(value),
                { } t when t == typeof(nuint?) => (nuint?) (nuint) ulong.Parse(value),
                { } t when t == typeof(char?) => (char?) char.Parse(value),

                { } t when t == typeof(string) => value,

                { } t when t == typeof(object) => throw new ArgumentException($"Type 'object' in property {property.Info.Name} is too broad"),

                _ => throw new ArgumentException($"Invalid property type {property.Info.PropertyType} in property {property.Info.Name}")
            };

            property.Setter(instanceCfg, parsedValue);
        }

        return instanceCfg;
    }
}