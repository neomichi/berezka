using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Berezka.Data
{
    /// <summary>
    /// entity framework
    /// </summary>
    public static class EFHelper
    {
        public static IOrderedQueryable<T> Contains<T>(this IQueryable<T> queryable, string propertyName, string propertyValue)
        {
            var parameterExp = Expression.Parameter(typeof(T), "type");
            var propertyExp = Expression.Property(parameterExp, propertyName);
            var method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var someValue = Expression.Constant(propertyValue, typeof(string));
            var containsMethodExp = Expression.Call(propertyExp, method, someValue);

            var expression = Expression.Lambda<Func<T, bool>>(containsMethodExp, parameterExp);

            var expcall = Expression.Call(typeof(Queryable), "Where", new[] { typeof(T) }, queryable.Expression, Expression.Quote(expression));

            return (IOrderedQueryable<T>)queryable.Provider.CreateQuery<T>(expcall);
        }

        public static IQueryable<T> WhereEquals<T>(this IQueryable<T> source, string memberPath, object value)
        {
            var parameter = Expression.Parameter(typeof(T), "e");
            var member = memberPath.Split('.').Aggregate((Expression)parameter, Expression.PropertyOrField);
            var predicate = Expression.Lambda<Func<T, bool>>(
                Expression.Equal(member, Expression.Constant(value)),
                parameter);
            return source.Where(predicate);
        }

        public static System.Linq.Expressions.Expression<Func<string, string, bool>>
    Like_Lambda = (item, search) => item.ToLower().Contains(search.ToLower());


        public static Func<string, string, bool> Like = Like_Lambda.Compile();


        public static System.Linq.Expressions.Expression<Func<Object, Object, bool>>
            EQL_Lambda = (obj1, obj2) => obj1.ObjIsString() == obj2.ObjIsString();

        public static Func<string, string, bool> EQL = EQL_Lambda.Compile();

        public static Expression<Func<T, bool>> CreateLike<T>(PropertyInfo prop, string value)
        {
            var parameter = Expression.Parameter(typeof(T), "f");
            var propertyAccess = Expression.MakeMemberAccess(parameter, prop);

            var indexOf = Expression.Call(propertyAccess, "IndexOf", null, Expression.Constant(value, typeof(string)), Expression.Constant(StringComparison.OrdinalIgnoreCase));
            var like = Expression.GreaterThanOrEqual(indexOf, Expression.Constant(0));
            return Expression.Lambda<Func<T, bool>>(like, parameter);
        }
    }
}
