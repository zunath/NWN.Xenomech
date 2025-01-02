using System.Linq.Expressions;
using System.Reflection;
using System;
using Action = System.Action;

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

        /// <summary>
        /// Retrieves the name of the property targeted in an expression.
        /// </summary>
        /// <typeparam name="TProperty">The type of property being targeted.</typeparam>
        /// <param name="expression">Expression to target the property.</param>
        /// <returns>The name of the property.</returns>
        private static string GetMemberName<TProperty>(Expression<Func<TViewModel, TProperty>> expression)
        {
            // Case 1: If the expression refers to a property or field
            var member = expression.Body as MemberExpression;
            if (member != null)
            {
                var propInfo = member.Member as PropertyInfo;
                if (propInfo != null)
                {
                    return propInfo.Name; // Property name
                }

                var fieldInfo = member.Member as FieldInfo;
                if (fieldInfo != null)
                {
                    return fieldInfo.Name; // Field name
                }
            }

            // Case 2: If the expression refers to a delegate (e.g., Action or Func)
            var unaryExpression = expression.Body as UnaryExpression;
            if (unaryExpression != null)
            {
                // Compile the expression into a delegate
                var compiledDelegate = Expression.Lambda(unaryExpression.Operand).Compile();

                // Extract MethodInfo from the delegate
                if (compiledDelegate is Delegate del)
                {
                    // Check if it's an Action (it will not return a value)
                    if (del is Action)
                    {
                        return del.Method.Name; // Method name from Action delegate
                    }
                    // You can handle Func here similarly if needed
                    else if (del is Func<object> || del is Delegate)
                    {
                        return del.Method.Name; // Method name from other delegates
                    }
                }
            }

            throw new ArgumentException($"Expression '{expression}' is neither a property, field, nor method.");
        }
    }
}
