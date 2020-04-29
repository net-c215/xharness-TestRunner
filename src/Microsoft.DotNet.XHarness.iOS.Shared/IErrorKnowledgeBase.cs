using Microsoft.DotNet.XHarness.iOS.Shared.Logging;

namespace Microsoft.DotNet.XHarness.iOS.Shared
{
    /// <summary>
    /// Interface to be implemented to determine if error in the installation, build and execution of the tests
    /// are known. This class will help users understand better why an error ocurred.
    /// </summary>
    public interface IErrorKnowledgeBase
    {
        /// <summary>
        /// Identifies via the logs if the installation failure is due to a known issue that the user can act upon.
        /// </summary>
        /// <param name="installLog">The installation log.</param>
        /// <param name="knownFailureMessage">A string message for the user to understand the reason for the failure.</param>
        /// <returns>True if the failure is due to a known reason, false otherwise.</returns>
        bool IsKnownInstallIssue(ILog installLog, out string knownFailureMessage);

        /// <summary>
        /// Identifies via the logs if the build failure is due to a known issue that the user can act upon.
        /// </summary>
        /// <param name="buildLog">The build log.</param>
        /// <param name="knownFailureMessage">A string message for the user to understand the reason for the failure.</param>
        /// <returns>True if the failure is due to a known reason, false otherwise.</returns>
        bool IsKnownBuildIssue(ILog buildLog, out string knownFailureMessage);

        /// <summary>
        /// Identifies via the logs if the run failure is due to a known issue that the user can act upon.
        /// </summary>
        /// <param name="runLog">The run log.</param>
        /// <param name="knownFailureMessage">A string message for the user to understand the reason for the failure.</param>
        /// <returns>True if the failure is due to a known reason, false otherwise.</returns>
        bool IsKnownTestIssue(ILog runLog, out string knownFailureMessage);
    }
}
