using System;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentValidation.Extensions.AutoMapper
{
    /// <summary>
    /// FluentValidationPropertyNameResolver to map the DTO-PropertyName to (View)Model-PropertyName.
    /// </summary>
    public interface IFluentValidationPropertyNameResolver
    {
        /// <summary>
        /// Resolves the property name for the specified type, memberInfo and lambdaExpression.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="memberInfo">The member information.</param>
        /// <param name="lambdaExpression">The lambda expression.</param>
        /// <returns>New Property Name if found, else construct the Property Name from the MemberInfo or else null.</returns>
        string Resolve(Type type, MemberInfo memberInfo, LambdaExpression lambdaExpression);
    }
}