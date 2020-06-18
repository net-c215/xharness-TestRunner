﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using Microsoft.DotNet.XHarness.Common.Logging;
using Microsoft.DotNet.XHarness.iOS.Shared;
using Microsoft.DotNet.XHarness.iOS.Shared.Execution.Mlaunch;
using Microsoft.DotNet.XHarness.iOS.Shared.Logging;

namespace Microsoft.DotNet.XHarness.iOS
{
    public interface ICrashSnapshotReporterFactory
    {
        ICrashSnapshotReporter Create(ILog log, ILogs logs, bool isDevice, string deviceName);
    }

    public class CrashSnapshotReporterFactory : ICrashSnapshotReporterFactory
    {
        private readonly IMLaunchProcessManager _processManager;

        public CrashSnapshotReporterFactory(IMLaunchProcessManager processManager)
        {
            _processManager = processManager ?? throw new ArgumentNullException(nameof(processManager));
        }

        public ICrashSnapshotReporter Create(ILog log, ILogs logs, bool isDevice, string deviceName) =>
            new CrashSnapshotReporter(_processManager, log, logs, isDevice, deviceName);
    }
}
