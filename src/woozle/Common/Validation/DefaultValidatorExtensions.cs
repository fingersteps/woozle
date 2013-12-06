using System;
using System.Linq.Expressions;

using ServiceStack.FluentValidation;

namespace Woozle.Core.Common.FluentValidation
{
    public static class DefaultValidatorExtensions
    {
        public static IRuleBuilderOptions<T, DateTime?> GreaterThanNullableDate<T>(this IRuleBuilderInitial<T, DateTime?> ruleBuilder, Expression<Func<T, DateTime?>> expression) //Expression<PropertySelector<T, DateTime?>> expression)//Expression<Func<T, DateTime?>> expression)
        {
            return ruleBuilder.SetValidator(new GreaterThanNullableDate<T>(expression));
        }
    }
}
