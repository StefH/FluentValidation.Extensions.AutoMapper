using System;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

// ReSharper disable once CheckNamespace
namespace AutoMapper
{
    public static class AutoMapperExtensions
    {
        [PublicAPI]
        public static void Unflatten<TSource, TDestination, TMember>(this IMemberConfigurationExpression<TSource, TDestination, TMember> config)
        {
            config.MapFrom((source, destination, member, resolutionContext) =>
            {
                string prefix = typeof(TMember).Name;

                TMember resolvedObject = (TMember) Activator.CreateInstance(typeof(TMember));

                PropertyInfo[] targetProperties = resolvedObject.GetType().GetProperties();

                foreach (var sourceMember in source.GetType().GetProperties())
                {
                    // find the matching target property and populate it
                    PropertyInfo matchedProperty = targetProperties.FirstOrDefault(p => sourceMember.Name == prefix + p.Name);

                    matchedProperty?.SetValue(resolvedObject, sourceMember.GetValue(source));
                }

                return resolvedObject;
            });
        }
    }
}
