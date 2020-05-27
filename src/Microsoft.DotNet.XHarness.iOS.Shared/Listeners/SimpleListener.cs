﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.DotNet.XHarness.Common.Logging;

namespace Microsoft.DotNet.XHarness.iOS.Shared.Listeners
{
    public interface ISimpleListener : IDisposable
    {
        Task CompletionTask { get; }
        Task ConnectedTask { get; }
        IFileBackedLog TestLog { get; }

        void Cancel();
        int InitializeAndGetPort();
        void StartAsync();
    }

    public abstract class SimpleListener : ISimpleListener
    {
        private readonly TaskCompletionSource<bool> _stopped = new TaskCompletionSource<bool>();
        private readonly TaskCompletionSource<bool> _connected = new TaskCompletionSource<bool>();

        public IFileBackedLog TestLog { get; private set; }

        protected readonly IPAddress Address = IPAddress.Any;
        protected ILog Log { get; }
        protected abstract void Start();
        protected abstract void Stop();

        public Task ConnectedTask => _connected.Task;
        public abstract int InitializeAndGetPort();

        protected SimpleListener(ILog log, IFileBackedLog testLog)
        {
            Log = log ?? throw new ArgumentNullException(nameof(log));
            TestLog = testLog ?? throw new ArgumentNullException(nameof(testLog));
        }

        protected void Connected(string remote)
        {
            Log.WriteLine("Connection from {0} saving logs to {1}", remote, TestLog.FullPath);
            _connected.TrySetResult(true);
        }

        protected void Finished(bool early_termination = false)
        {
            if (_stopped.TrySetResult(early_termination))
            {
                if (early_termination)
                {
                    Log.WriteLine("Tests were terminated before completion");
                }
                else
                {
                    Log.WriteLine("Tests have finished executing");
                }
            }
        }

        public void StartAsync()
        {
            var t = new Thread(() =>
            {
                try
                {
                    Start();
                }
                catch (Exception e)
                {
                    Log.WriteLine($"{GetType().Name}: an exception occurred in processing thread: {e}");
                }
            })
            {
                IsBackground = true,
            };
            t.Start();
        }

        public bool WaitForCompletion(TimeSpan ts)
        {
            return _stopped.Task.Wait(ts);
        }

        public Task CompletionTask => _stopped.Task;

        public void Cancel()
        {
            _connected.TrySetCanceled();
            try
            {
                // wait a second just in case more data arrives.
                if (!_stopped.Task.Wait(TimeSpan.FromSeconds(1)))
                    Stop();
            }
            catch
            {
                // We might have stopped already, so just ignore any exceptions.
            }
        }

        public virtual void Dispose()
        {
            TestLog.Dispose();
        }
    }
}

