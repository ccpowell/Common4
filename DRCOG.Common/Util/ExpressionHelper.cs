using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace DRCOG.Common.Util
{
    public sealed class ExpressionHelper
    {
        public static String PropertyName<T, TProperty>(Expression<Func<T, TProperty>> expression)
        {
            String property = expression.Body.ToString();
            Int32 firstPeriod = property.IndexOf(".") + 1;
            if (expression.Body.NodeType == ExpressionType.MemberAccess)
            {
                return property.Substring(firstPeriod, property.Length - firstPeriod);
            }

            if (expression.Body.NodeType == ExpressionType.Convert)
            {
                String subString = property.Substring(firstPeriod, property.Length - (1 + firstPeriod));
                return subString.Contains('.') ? subString.Substring(0, subString.IndexOf('.')) : subString;
            }

            throw new NotSupportedException(expression.Body.NodeType + " is not supported by this method.");
        }

        #region OrderBy

        public static IOrderedQueryable<T> OrderBy<T>(IQueryable<T> source, string property)
        {
            return ApplyOrder(source, property, "OrderBy");
        }
        public static IOrderedQueryable<T> OrderByDescending<T>(IQueryable<T> source, string property)
        {
            return ApplyOrder(source, property, "OrderByDescending");
        }
        public static IOrderedQueryable<T> ThenBy<T>(IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder(source, property, "ThenBy");
        }
        public static IOrderedQueryable<T> ThenByDescending<T>(IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder(source, property, "ThenByDescending");
        }
        private static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
        {
            KeyValuePair<ParameterExpression, KeyValuePair<Type, Expression>> pair =
                GetParameterExpression(typeof(T), property);
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), pair.Value.Key);
            LambdaExpression lambda = Expression.Lambda(delegateType, pair.Value.Value, pair.Key);
            object result = typeof(Queryable).GetMethods().Single(
                method => method.Name == methodName
                    && method.IsGenericMethodDefinition
                    && method.GetGenericArguments().Length == 2
                    && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), pair.Value.Key)
                    .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<T>)result;
        }

        #endregion OrderBy

        #region Support methods

        public static ConstantExpression GetConstantExpression(Type propertyType, Object propertyValue)
        {
            ConstantExpression con;
            if (propertyValue == null)
            {
                con = Expression.Constant(null);
            }
            else
            {
                if (propertyType.Equals(typeof(Int16)) && propertyValue is String)
                {
                    propertyValue = Convert.ToInt16(propertyValue);
                }
                else if (propertyType.Equals(typeof(Int32)) && propertyValue is String)
                {
                    propertyValue = Convert.ToInt32(propertyValue);
                }
                else if (propertyType.Equals(typeof(Int64)) && propertyValue is String)
                {
                    propertyValue = Convert.ToInt64(propertyValue);
                }
                else if (propertyType.Equals(typeof(Boolean)) && propertyValue is String)
                {
                    propertyValue = Convert.ToBoolean(propertyValue);
                }
                else if (propertyType.BaseType.Equals(typeof(Enum)) && propertyValue is String)
                {
                    propertyValue = Enum.Parse(propertyType, propertyValue as String);
                }
                con = Expression.Constant(propertyValue, propertyValue.GetType());
            }

            return con;
        }

        /// <summary>
        /// Gets a key value pair indicating the parameter expression, the property expression of the parameter and the type of that property.
        /// </summary>
        /// <param name="type">The soruce type "T"</param>
        /// <param name="propertyName">The name of the property on "T"</param>
        /// <returns></returns>
        public static KeyValuePair<ParameterExpression, KeyValuePair<Type, Expression>> GetParameterExpression(Type type, String propertyName)
        {
            string[] props = propertyName.Split('.');
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            Type propertyType = type;
            foreach (string prop in props)
            {
                PropertyInfo pi = propertyType.GetProperty(prop);
                expr = Expression.Property(expr, pi);
                propertyType = pi.PropertyType;
            }

            return new KeyValuePair<ParameterExpression, KeyValuePair<Type, Expression>>(
                arg, new KeyValuePair<Type, Expression>(propertyType, expr));
        }

        #endregion Support Methods

        #region Where Expressions

        public static Expression<Func<T, bool>> WhereEqual<T>(String propertyName, Object propertyValue)
        {
            var pair = GetParameterExpression(typeof(T), propertyName);
            ConstantExpression con = GetConstantExpression(pair.Value.Key, propertyValue);
            BinaryExpression be = Expression.Equal(pair.Value.Value, con);
            return Expression.Lambda<Func<T, Boolean>>(be, pair.Key);
        }

        public static Expression<Func<T, bool>> WhereNotEqual<T>(String propertyName, Object propertyValue)
        {
            var pair = GetParameterExpression(typeof(T), propertyName);
            ConstantExpression con = GetConstantExpression(pair.Value.Key, propertyValue);
            BinaryExpression be = Expression.NotEqual(pair.Value.Value, con);
            return Expression.Lambda<Func<T, Boolean>>(be, pair.Key);
        }

        public static Expression<Func<T, bool>> WhereGreaterThan<T>(String propertyName, Object propertyValue)
        {
            var pair = GetParameterExpression(typeof(T), propertyName);
            ConstantExpression con = GetConstantExpression(pair.Value.Key, propertyValue);
            BinaryExpression be = Expression.GreaterThan(pair.Value.Value, con);
            return Expression.Lambda<Func<T, Boolean>>(be, pair.Key);
        }

        public static Expression<Func<T, bool>> WhereGreaterThanOrEqual<T>(String propertyName, Object propertyValue)
        {
            var pair = GetParameterExpression(typeof(T), propertyName);
            ConstantExpression con = GetConstantExpression(pair.Value.Key, propertyValue);
            BinaryExpression be = Expression.GreaterThanOrEqual(pair.Value.Value, con);
            return Expression.Lambda<Func<T, Boolean>>(be, pair.Key);
        }

        public static Expression<Func<T, bool>> WhereLessThan<T>(String propertyName, Object propertyValue)
        {
            var pair = GetParameterExpression(typeof(T), propertyName);
            ConstantExpression con = GetConstantExpression(pair.Value.Key, propertyValue);
            BinaryExpression be = Expression.LessThan(pair.Value.Value, con);
            return Expression.Lambda<Func<T, Boolean>>(be, pair.Key);
        }

        public static Expression<Func<T, bool>> WhereLessThanOrEqual<T>(String propertyName, Object propertyValue)
        {
            var pair = GetParameterExpression(typeof(T), propertyName);
            ConstantExpression con = GetConstantExpression(pair.Value.Key, propertyValue);
            BinaryExpression be = Expression.LessThanOrEqual(pair.Value.Value, con);
            return Expression.Lambda<Func<T, Boolean>>(be, pair.Key);
        }

        enum MatchMode { Contains, StartsWith, EndsWith }
        private static Expression<Func<T, Boolean>> GetLikeExpression<T>(String propertyName, Object propertyValue,
            MatchMode mode, Boolean not)
        {
            var pair = GetParameterExpression(typeof(T), propertyName);
            ConstantExpression con = GetConstantExpression(pair.Value.Key, propertyValue);
            MethodInfo info;
            switch (mode)
            {
                case MatchMode.Contains:
                    info = typeof(String).GetMethod("Contains", new[] { typeof(String) });
                    break;
                case MatchMode.EndsWith:
                    info = typeof(String).GetMethod("StartsWith", new[] { typeof(String) });
                    break;
                case MatchMode.StartsWith:
                    info = typeof(String).GetMethod("EndsWith", new[] { typeof(String) });
                    break;
                default:
                    throw new ArgumentException(string.Format("'{0}' is not supported.", mode), "mode");
            }

            Expression e = Expression.Call(pair.Value.Value, info, con);
            return Expression.Lambda<Func<T, Boolean>>(not ? Expression.Not(e) : e, pair.Key);
        }

        public static Expression<Func<T, bool>> WhereLike<T>(String propertyName, Object propertyValue)
        {
            return GetLikeExpression<T>(propertyName, propertyValue, MatchMode.Contains, false);
        }

        public static Expression<Func<T, bool>> WhereNotLike<T>(String propertyName, Object propertyValue)
        {
            return GetLikeExpression<T>(propertyName, propertyValue, MatchMode.Contains, true);
        }

        public static Expression<Func<T, bool>> WhereStartsWith<T>(String propertyName, Object propertyValue)
        {
            return GetLikeExpression<T>(propertyName, propertyValue, MatchMode.StartsWith, false);
        }

        public static Expression<Func<T, bool>> WhereNotStartsWith<T>(String propertyName, Object propertyValue)
        {
            return GetLikeExpression<T>(propertyName, propertyValue, MatchMode.StartsWith, true);
        }

        public static Expression<Func<T, bool>> WhereEndsWith<T>(String propertyName, Object propertyValue)
        {
            return GetLikeExpression<T>(propertyName, propertyValue, MatchMode.EndsWith, false);
        }

        public static Expression<Func<T, bool>> WhereNotEndsWith<T>(String propertyName, Object propertyValue)
        {
            return GetLikeExpression<T>(propertyName, propertyValue, MatchMode.EndsWith, true);
        }

        [Obsolete("Please use WhereEqual<T>(String, Object)")]
        public static Expression<Func<T, bool>> GetWhereEqualExpression<T, TProperty>(
            Expression<Func<T, TProperty>> property, TProperty propertyValue)
        {
            return WhereEqual<T>(PropertyName(property), propertyValue);
        }

        #endregion Where Expressions
    }
}
