using System;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentValidationExample.Web.Validation
{
    /// <summary>
    /// FluentValidationPropertyNameResolver to map the DTO-PropertyName to Model-PropertyName.
    /// </summary>
    public interface IFluentValidationPropertyNameResolver
    {
        /// <summary>
        /// Resolves the property name for the specified type, memberInfo and lambdaExpression.
        /// </summary>
        /// <param name="dtoType">The type.</param>
        /// <param name="dtoMemberInfo">The member information.</param>
        /// <param name="lambdaExpression">The lambda expression.</param>
        /// <returns>Property Name</returns>
        string Resolve(Type dtoType, MemberInfo dtoMemberInfo, LambdaExpression lambdaExpression);
    }
}