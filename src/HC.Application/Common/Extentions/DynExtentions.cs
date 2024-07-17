using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;

namespace HC.Application.Common.Extentions;

public static class DynExtentions
{
    public static dynamic ToDynamic<T>(this T obj)
    {
        IDictionary<string, object> expando = new ExpandoObject();

        foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
        {
            object currentValue = propertyInfo.GetValue(obj);
            expando.Add(char.ToLowerInvariant(propertyInfo.Name[0]) + propertyInfo.Name.Substring(1), currentValue);
        }

        return expando as ExpandoObject;
    }
}