using System;
using System.Management.Automation;
using System.Threading;
using System.Threading.Tasks;
using PSOPNSenseAPI.Logging;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Tests threading issues in PowerShell cmdlets.</para>
    /// <para type="description">The Test-OPNSenseThreading cmdlet tests threading issues in PowerShell cmdlets.</para>
    /// </summary>
    [Cmdlet(VerbsDiagnostic.Test, "OPNSenseThreading")]
    [OutputType(typeof(string))]
    public class TestThreadingCmdlet : PSCmdlet
    {
        /// <summary>
        /// <para type="description">The test mode to run.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 0)]
        [ValidateSet("Direct", "ConfigureAwait", "SynchronizationContext", "TaskRun")]
        public string Mode { get; set; } = "Direct";

        /// <summary>
        /// <para type="description">The delay in milliseconds.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public int Delay { get; set; } = 1000;

        /// <summary>
        /// Exception to be processed in ProcessRecord
        /// </summary>
        protected Exception ProcessingException { get; set; }

        /// <summary>
        /// Begins the processing of the cmdlet
        /// </summary>
        protected override void BeginProcessing()
        {
            base.BeginProcessing();
        }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecord()
        {
            try
            {
                WriteVerbose($"Starting test in mode: {Mode}");
                WriteVerbose($"Current thread ID: {Thread.CurrentThread.ManagedThreadId}");
                WriteVerbose($"Current synchronization context: {SynchronizationContext.Current != null}");

                switch (Mode)
                {
                    case "Direct":
                        TestDirect();
                        break;
                    case "ConfigureAwait":
                        TestConfigureAwait();
                        break;
                    case "SynchronizationContext":
                        TestSynchronizationContext();
                        break;
                    case "TaskRun":
                        TestTaskRun();
                        break;
                }

                WriteVerbose("Test completed");
            }
            catch (Exception ex)
            {
                WriteWarning($"Error occurred: {ex.Message}");
                ProcessingException = ex;
            }
            finally
            {
                // Process any exceptions that occurred
                if (ProcessingException != null)
                {
                    WriteWarning($"Processing exception: {ProcessingException.Message}");
                    ProcessingException = null;
                }
            }
        }

        /// <summary>
        /// Ends the processing of the cmdlet
        /// </summary>
        protected override void EndProcessing()
        {
            base.EndProcessing();
        }

        /// <summary>
        /// Executes an async task synchronously and safely
        /// </summary>
        /// <typeparam name="T">The return type of the task</typeparam>
        /// <param name="asyncFunc">The async function to execute</param>
        /// <returns>The result of the async function</returns>
        protected T ExecuteAsyncTask<T>(Func<Task<T>> asyncFunc)
        {
            try
            {
                // Use ConfigureAwait(false) to avoid deadlocks
                return asyncFunc().ConfigureAwait(false).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                // Store the exception to be processed in ProcessRecord
                ProcessingException = ex;
                WriteWarning($"Error occurred: {ex.Message}");
                return default;
            }
        }

        private void TestDirect()
        {
            WriteVerbose("Testing direct method call");
            WriteVerbose($"Before delay - Thread ID: {Thread.CurrentThread.ManagedThreadId}");

            Task.Delay(Delay).Wait();

            WriteVerbose($"After delay - Thread ID: {Thread.CurrentThread.ManagedThreadId}");
            WriteObject("Direct test completed");
        }

        private void TestConfigureAwait()
        {
            WriteVerbose("Testing ConfigureAwait(false)");
            WriteVerbose($"Before async call - Thread ID: {Thread.CurrentThread.ManagedThreadId}");

            var result = ExecuteAsyncTask(() => Task.Delay(Delay).ContinueWith(t =>
            {
                WriteVerbose($"Inside async task - Thread ID: {Thread.CurrentThread.ManagedThreadId}");
                return "ConfigureAwait test completed";
            }));

            WriteVerbose($"After async call - Thread ID: {Thread.CurrentThread.ManagedThreadId}");
            WriteObject(result);
        }

        private void TestSynchronizationContext()
        {
            WriteVerbose("Testing SynchronizationContext");
            WriteVerbose($"Before async call - Thread ID: {Thread.CurrentThread.ManagedThreadId}");
            WriteVerbose($"SynchronizationContext: {SynchronizationContext.Current != null}");

            var syncContext = SynchronizationContext.Current;
            var result = ExecuteAsyncTask(() => Task.Delay(Delay).ContinueWith(t =>
            {
                var taskThreadId = Thread.CurrentThread.ManagedThreadId;
                WriteVerbose($"Inside async task - Thread ID: {taskThreadId}");

                if (syncContext != null)
                {
                    syncContext.Post(_ =>
                    {
                        WriteVerbose($"Inside SynchronizationContext.Post - Thread ID: {Thread.CurrentThread.ManagedThreadId}");
                    }, null);
                }

                return "SynchronizationContext test completed";
            }));

            WriteVerbose($"After async call - Thread ID: {Thread.CurrentThread.ManagedThreadId}");
            WriteObject(result);
        }

        private void TestTaskRun()
        {
            WriteVerbose("Testing Task.Run");
            WriteVerbose($"Before Task.Run - Thread ID: {Thread.CurrentThread.ManagedThreadId}");

            var task = Task.Run(() =>
            {
                WriteVerbose($"Inside Task.Run - Thread ID: {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(Delay);
                WriteVerbose($"After delay in Task.Run - Thread ID: {Thread.CurrentThread.ManagedThreadId}");
                return "TaskRun test completed";
            });

            var result = task.GetAwaiter().GetResult();

            WriteVerbose($"After Task.Run - Thread ID: {Thread.CurrentThread.ManagedThreadId}");
            WriteObject(result);
        }
    }
}
