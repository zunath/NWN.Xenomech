using System.Linq.Expressions;
using System.Reflection;
using System;
using System.Collections.Generic;

namespace XM.UI.Builder
{
    public abstract class NuiBindable<TViewModel>
        where TViewModel: IViewModel
    {
        protected NuiEventCollection RegisteredEvents { get;  }

        protected bool RaisesNuiEvents { get; set; }

        protected NuiBindable(NuiEventCollection registeredEvents)
        {
            RegisteredEvents = registeredEvents;
        }

        protected string GetBindName<T>(Expression<Func<TViewModel, T>> expression)
        {
            return GetMemberName(expression);
        }

        protected NuiEventDetail GetMethodInfo<TMethod>(Expression<Func<TViewModel, TMethod>> expression)
        {
            var body = (MethodCallExpression)expression.Body;
            var method = body.Method;
            var values = new List<KeyValuePair<Type, object>>();

            foreach (var argument in body.Arguments)
            {
                var value = Expression.Lambda(argument).Compile().DynamicInvoke();
                values.Add(new KeyValuePair<Type, object>(value.GetType(), value));
            }

            return new NuiEventDetail(method, values);
        }

        /// <summary>
        /// Retrieves the name of the property targeted in an expression.
        /// </summary>
        /// <typeparam name="TProperty">The type of property being targeted.</typeparam>
        /// <typeparam name="TInterface">The type of interface being targeted.</typeparam>
        /// <param name="expression">Expression to target the property.</param>
        /// <returns>The name of the property.</returns>
        private static string GetMemberName<TInterface, TProperty>(Expression<Func<TInterface, TProperty>> expression)
        {
            // Case 1: If the expression refers to a property or field
            if (expression.Body is MemberExpression member)
            {
                if (member.Member is PropertyInfo propInfo)
                {
                    return propInfo.Name; // Property name
                }

                if (member.Member is FieldInfo fieldInfo)
                {
                    return fieldInfo.Name; // Field name
                }
            }

            // Case 2: If the expression refers to a method (Action or Func)
            if (expression.Body is MethodCallExpression methodCall)
            {
                return methodCall.Method.Name; // Method name
            }

            // Case 3: If the expression is a delegate (e.g., Action or Func)
            if (expression.Body is UnaryExpression unaryExpression)
            {
                // Compile the expression into a delegate
                var compiledDelegate = Expression.Lambda(unaryExpression.Operand).Compile();

                if (compiledDelegate is Delegate del)
                {
                    return del.Method.Name; // Method name from delegate
                }
            }

            throw new ArgumentException($"Expression '{expression}' is neither a property, field, nor method.");
        }

    }
}
