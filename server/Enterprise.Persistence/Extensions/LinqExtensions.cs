using System.Linq.Expressions;
using System.Reflection;

namespace Enterprise.Persistence.Extensions;

public static class LinqExtensions
{
    public static IEnumerable<TResult> SelectSkipNulls<TSource, TResult>(
        this IEnumerable<TSource?> source,
        Func<TSource, TResult?> selector)
        where TSource : class
        where TResult : class
    {
        return source
            .Where(x => x is not null)
            .Select(x => selector(x!))
            .Where(result => result is not null)!;
    }

    public static IQueryable<T> OrderByProperty<T>(
        this IQueryable<T> source,
        string propertyName,
        bool ascending = true)
    {
        if (string.IsNullOrWhiteSpace(propertyName))
        {
            return source;
        }

        var type = typeof(T);
        var property = type.GetProperty(propertyName,
            BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

        if (property == null || !property.CanRead)
        {
            return source;
        }

        try
        {
            var parameter = Expression.Parameter(type, "x");
            var propertyAccess = Expression.Property(parameter, property);
            var lambda = Expression.Lambda(propertyAccess, parameter);

            var methodName = ascending ? "OrderBy" : "OrderByDescending";

            var method = typeof(Queryable)
                .GetMethods()
                .First(m => m.Name == methodName
                            && m.GetParameters().Length == 2)
                .MakeGenericMethod(type, property.PropertyType);

            return (IQueryable<T>)method.Invoke(null, [source, lambda])!;
        }
        catch
        {
            return source;
        }
    }
}
