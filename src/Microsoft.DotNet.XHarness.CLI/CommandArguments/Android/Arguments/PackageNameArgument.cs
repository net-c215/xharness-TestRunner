﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.DotNet.XHarness.Common.CLI.CommandArguments;

namespace Microsoft.DotNet.XHarness.CLI.CommandArguments.Android
{
    internal class PackageNameArgument : RequiredStringArgument
    {
        public PackageNameArgument()
            : base("package-name=|p=", "Package name contained within the supplied APK")
        {
        }
    }
}
