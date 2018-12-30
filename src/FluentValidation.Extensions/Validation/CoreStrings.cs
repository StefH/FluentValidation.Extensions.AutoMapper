// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

// copied from https://github.com/aspnet/EntityFramework/blob/dev/src/Microsoft.EntityFrameworkCore/Properties/CoreStrings.resx
namespace FluentValidation.Extensions.AutoMapper.Validation
{
    internal static class CoreStrings
    {
        /// <summary>
        /// The string argument '{argumentName}' cannot be empty.
        /// </summary>
        public static string ArgumentIsEmpty(string argumentName)
        {
            return $"The string argument '{argumentName}' cannot be empty.";
        }
    }
}