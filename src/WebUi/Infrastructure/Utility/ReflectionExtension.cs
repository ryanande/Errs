namespace Errs.WebUi.Infrastructure.Utility
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class ReflectionExtensions
    {
        public static PropertyInfo AsPropertyInfo<T>(this Expression<Func<T, object>> property)
        {
            return ReflectionHelper.GetProperty(property);
        }

        public static bool Has<TAttribute>(this Type type) where TAttribute : Attribute
        {
            return type.GetCustomAttributes<TAttribute>(false).Any();
        }

        public static bool Has<TAttribute>(this PropertyInfo propertyInfo) where TAttribute : Attribute
        {
            return propertyInfo.GetCustomAttributes<TAttribute>().Any();
        }

        public static bool IsLocalAssembly(this Assembly assembly)
        {
            return assembly.FullName.Contains("Errs");
        }
    }
}