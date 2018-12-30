using AutoMapper;
using FluentValidationExample.Common.Validation;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentValidationExample.Web.Validation
{
    /// <summary>
    /// Implementation from IFluentValidationPropertyNameResolver
    /// </summary>
    /// <seealso cref="IFluentValidationPropertyNameResolver" />
    internal class FluentValidationPropertyNameResolver : IFluentValidationPropertyNameResolver
    {
        private readonly IMapper _mapper;

        private readonly IDictionary<(Type dtoType, string dtoProperty), string> _mappings = new Dictionary<(Type dtoType, string dtoProperty), string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentValidationPropertyNameResolver"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        public FluentValidationPropertyNameResolver([NotNull] IMapper mapper)
        {
            Guard.NotNull(mapper, nameof(mapper));

            _mapper = mapper;

            Init();
        }

        private void Init()
        {
            TypeMap[] allTypeMaps = _mapper.ConfigurationProvider.GetAllTypeMaps();
            foreach (TypeMap map in allTypeMaps)
            {
                foreach (PropertyMap propertyMap in map.PropertyMaps)
                {
                    string modelMemberName = propertyMap.SourceMember?.Name;
                    _mappings.Add((map.DestinationType, propertyMap.DestinationName), modelMemberName);
                }
            }
        }

        /// <inheritdoc cref="IFluentValidationPropertyNameResolver.Resolve(Type, MemberInfo, LambdaExpression)"/>
        public string Resolve(Type dtoType, MemberInfo dtoMemberInfo, LambdaExpression lambdaExpression)
        {
            string propertyName = dtoMemberInfo?.Name;

            (Type, string) key = (dtoType, propertyName);

            // Not found in dictionary, just return propertyName.
            return _mappings.ContainsKey(key) ? _mappings[key] : propertyName;
        }
    }
}