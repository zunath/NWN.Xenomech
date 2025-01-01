using System.Linq.Expressions;
using System.Reflection;
using System;

namespace XM.UI.Builder
{
    public abstract class NuiBindable<TViewModel>
        where TViewModel: IViewModel
    {
        protected string GetBindName<T>(Expression<Func<TViewModel, T>> expression)
        {
            return GetPropertyName(expression);
        }

        /// <summary>
        /// Retrieves the name of the property targeted in an expression.
        /// </summary>
        /// <typeparam name="TProperty">The type of property being targeted.</typeparam>
        /// <param name="expression">Expression to target the property.</param>
        /// <returns>The name of the property.</returns>
        private static string GetPropertyName<TProperty>(Expression<Func<TViewModel, TProperty>> expression)
        {
            var member = expression.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException($"Expression '{expression}' refers to a method, not a property.");

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException($"Expression '{expression}' refers to a field, not a property.");

            //if (type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType))
            //    throw new ArgumentException($"Expression '{expression}' refers to a property that is not from type {type}.");

            return propInfo.Name;
        }
    }
}
