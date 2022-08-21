using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;
using AutoMapper.Internal;
using Stef.Validation;

namespace FluentValidation.Extensions.AutoMapper;

/// <summary>
/// Implementation from <seealso cref="IFluentValidationPropertyNameResolver" />
/// </summary>
public class FluentValidationPropertyNameResolver : IFluentValidationPropertyNameResolver
{
    private readonly IDictionary<(Type dtoType, string dtoProperty), string> _mappings = new Dictionary<(Type dtoType, string dtoProperty), string>();

    /// <summary>
    /// Initializes a new instance of the <see cref="FluentValidationPropertyNameResolver"/> class.
    /// </summary>
    /// <param name="mapper">The mapper.</param>
    public FluentValidationPropertyNameResolver(IMapper mapper)
    {
        Init(Guard.NotNull(mapper));
    }

    private void Init(IMapper mapper)
    {
#if AUTOMAPPER11
        var configuration = mapper.ConfigurationProvider;
        var configurationType = configuration.GetType();
        var configuredMapsFieldInfo = configurationType.GetField("_configuredMaps", BindingFlags.Instance | BindingFlags.NonPublic)!;
        var configuredMaps = (Dictionary<TypePair, TypeMap>)configuredMapsFieldInfo.GetValue(configuration)!;
        var allTypeMaps = configuredMaps.Values;
#else
        var allTypeMaps = mapper.ConfigurationProvider.GetAllTypeMaps();
#endif
        foreach (TypeMap map in allTypeMaps)
        {
#if AUTOMAPPER7
            var propertyMaps = map.GetPropertyMaps();
#else
            var propertyMaps = map.PropertyMaps;
#endif
            foreach (PropertyMap propertyMap in propertyMaps)
            {
#if AUTOMAPPER7
                string destinationName = propertyMap.DestinationProperty.Name;
#else
                string destinationName = propertyMap.DestinationName;
#endif
                string modelMemberName = propertyMap.SourceMember?.Name!;

                (Type, string) key = (map.DestinationType, destinationName);
                _mappings.Add(key, modelMemberName);
            }
        }
    }

    /// <inheritdoc cref="IFluentValidationPropertyNameResolver.Resolve(Type, MemberInfo, LambdaExpression)"/>
    public string Resolve(Type type, MemberInfo memberInfo, LambdaExpression lambdaExpression)
    {
        string propertyName = memberInfo.Name;

        (Type, string) key = (type, propertyName);

        // Not found in dictionary, just return memberInfo.Name or null.
        return _mappings.ContainsKey(key) ? _mappings[key] : propertyName;
    }
}