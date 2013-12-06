using System;
using System.Linq.Expressions;
using ServiceStack.FluentValidation.Validators;

namespace Woozle.Core.Common.FluentValidation
{
    public class GreaterThanNullableDate<T> : PropertyValidator
    {
        private Expression<Func<T, DateTime?>> mTarget;
        public GreaterThanNullableDate(Expression<Func<T, DateTime?>> expression)
            : base("Property {PropertyName} greater than another date!",string.Empty)
        {

            mTarget = expression;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var oFunc = mTarget.Compile();
            //Type oType = mTarget.Parameters[0].Type;
            var oTargetDateTime = oFunc.Invoke((T)context.Instance);

            var oSource = context.PropertyValue as DateTime?;

            if (oSource != null && oTargetDateTime != null)
            {
                if (oSource < oTargetDateTime)
                    return false;
            }

            return true;
        }
    }

 
}
