﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Microsoft.DotNet.XHarness.CLI.CommandArguments.Wasm;

internal class JavaScriptEngineArguments : RepeatableArgument
{
    public JavaScriptEngineArguments()
        : base("engine-arg=", "Argument to pass to the JavaScript engine")
    {
    }
}
