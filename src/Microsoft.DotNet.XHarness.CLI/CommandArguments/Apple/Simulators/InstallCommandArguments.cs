﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace Microsoft.DotNet.XHarness.CLI.CommandArguments.Apple.Simulators
{
    internal class InstallCommandArguments : SimulatorsCommandArguments
    {
        public ForceInstallationArgument Force { get; } = new();

        public HideProgressArgument HideProgress { get; } = new();

        protected override IEnumerable<Argument> GetAdditionalArguments() => new Argument[]
        {
            Force,
            HideProgress,
        };
    }
}
