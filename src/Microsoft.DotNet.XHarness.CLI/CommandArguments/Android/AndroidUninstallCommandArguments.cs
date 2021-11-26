﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace Microsoft.DotNet.XHarness.CLI.CommandArguments.Android;

internal class AndroidUninstallCommandArguments : XHarnessCommandArguments
{
    public PackageNameArgument PackageName { get; } = new();
    public DeviceIdArgument DeviceId { get; } = new();

    protected override IEnumerable<Argument> GetArguments() => new Argument[]
    {
            PackageName,
            DeviceId,
    };
}
