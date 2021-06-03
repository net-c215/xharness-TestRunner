﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Threading;
using System.Threading.Tasks;
using Microsoft.DotNet.XHarness.Apple;
using Microsoft.DotNet.XHarness.CLI.CommandArguments.Apple;
using Microsoft.DotNet.XHarness.Common.CLI;
using Microsoft.DotNet.XHarness.iOS.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.DotNet.XHarness.CLI.Commands.Apple
{
    internal class AppleJustTestCommand : AppleAppCommand<AppleJustTestCommandArguments>
    {
        private const string CommandHelp = "Runs an already installed iOS/tvOS/watchOS/MacCatalyst test application containing a TestRunner in a target device/simulator.";

        protected override string CommandUsage { get; } = "apple just-test --app=... --output-directory=... --target=... [OPTIONS] [-- [RUNTIME ARGUMENTS]]";
        protected override string CommandDescription { get; } = CommandHelp;
        protected override AppleJustTestCommandArguments AppleAppArguments { get; } = new();

        public AppleJustTestCommand(IServiceCollection services) : base("just-test", false, services, CommandHelp)
        {
        }

        protected override Task<ExitCode> InvokeInternal(CancellationToken cancellationToken) =>
            Services
                .BuildServiceProvider()
                .GetRequiredService<IJustTestOrchestrator>()
                .OrchestrateTest(
                    AppBundleInformation.FromBundleId(AppleAppArguments.BundleIdentifier),
                    AppleAppArguments.Target,
                    AppleAppArguments.DeviceName,
                    AppleAppArguments.Timeout,
                    AppleAppArguments.LaunchTimeout,
                    AppleAppArguments.CommunicationChannel,
                    AppleAppArguments.XmlResultJargon,
                    AppleAppArguments.SingleMethodFilters,
                    AppleAppArguments.ClassMethodFilters,
                    AppleAppArguments.ResetSimulator,
                    AppleAppArguments.EnableLldb,
                    AppleAppArguments.EnvironmentalVariables,
                    PassThroughArguments,
                    cancellationToken);
    }
}