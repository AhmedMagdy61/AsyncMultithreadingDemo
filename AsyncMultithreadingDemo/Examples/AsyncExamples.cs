using System.ComponentModel;
using System.Diagnostics;

namespace AsyncMultithreadingDemo;

public static class AsyncExamples
{
    public static async Task DownloadFileAsync(string fileName)
    {
        Console.WriteLine($"{fileName} Download Started");

        await Task.Delay(3000);

        Console.WriteLine($"{fileName} Download Completed");
    }
    public static async Task ProcessWithCancellationAsync(
    CancellationToken cancellationToken)
    {
        for (int i = 1; i <= 10; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Console.WriteLine($"Processing: {i}");

            await Task.Delay(1000, cancellationToken);
        }

        Console.WriteLine("Processing Completed");
    }
    public static async Task ProcessDataAsync(string taskName, bool shouldFail)
    {
        Console.WriteLine($"{taskName} Started");

        await Task.Delay(1000);

        if (shouldFail)
        {
            throw new Exception($"{taskName} Failed!");
        }

        Console.WriteLine($"{taskName} Completed");
    }
    public static void RunWorkerThread()
    {
        Console.WriteLine("Main Thread Started");

        Thread workerThread = new Thread(() =>
        {
            Console.WriteLine("Worker Thread Started");

            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine($"Working: {i}");
                Thread.Sleep(1000);
            }

            Console.WriteLine("Worker Thread Finished");
        });

        workerThread.Start();

        Console.WriteLine("Main Thread is still running");

        workerThread.Join();

        Console.WriteLine("Main Thread Finished");
    }
    public static async Task RunBackgroundTaskAsync()
    {
        Console.WriteLine(
            $"Main Thread: {Thread.CurrentThread.ManagedThreadId}");

        Task task = Task.Run(() =>
        {
            Console.WriteLine(
                $"Background Thread: {Thread.CurrentThread.ManagedThreadId}");

            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine($"Processing: {i}");
                Thread.Sleep(1000);
            }
        });

        Console.WriteLine("Main Thread is still working...");

        await task;

        Console.WriteLine("Background Task Completed");
    }
    public static void RunParallelTasks()
    {
        Console.WriteLine("Parallel Processing Started");

        Parallel.For(1, 6, i =>
        {
            Console.WriteLine(
                $"Processing {i} on Thread {Thread.CurrentThread.ManagedThreadId}");

            Thread.Sleep(1000);
        });

        Console.WriteLine("Parallel Processing Completed");
    }
    public static void CompareExecutionTime()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();

        Parallel.For(1, 6, i =>
        {
            Thread.Sleep(1000);
        });

        stopwatch.Stop();

        Console.WriteLine(
            $"Parallel Execution Time: {stopwatch.Elapsed.TotalSeconds:F2} seconds");
    }
    public static void RunThreadPoolExample()
    {
        using ManualResetEventSlim completed = new(false);

        Console.WriteLine("Main Thread Started");

        ThreadPool.QueueUserWorkItem(state =>
        {
            try
            {
                Console.WriteLine(
                    $"ThreadPool Thread: {Thread.CurrentThread.ManagedThreadId}");

                Console.WriteLine("Background Work Started");

                Thread.Sleep(2000);

                Console.WriteLine("Background Work Completed");
            }
            finally
            {
                completed.Set();
            }
        });

        completed.Wait();

        Console.WriteLine("Main Thread Finished");
    }
    public static void RunBackgroundWorkerExample()
    {
        using BackgroundWorker worker = new BackgroundWorker();

        using ManualResetEventSlim completed = new(false);

        worker.WorkerReportsProgress = true;

        worker.DoWork += (sender, e) =>
        {
            for (int i = 1; i <= 5; i++)
            {
                Thread.Sleep(1000);

                worker.ReportProgress(i * 20);
            }
        };

        worker.ProgressChanged += (sender, e) =>
        {
            Console.WriteLine($"Progress: {e.ProgressPercentage}%");
        };

        worker.RunWorkerCompleted += (sender, e) =>
        {
            if (e.Error != null)
            {
                Console.WriteLine($"Error: {e.Error.Message}");
            }
            else
            {
                Console.WriteLine("Background Work Completed");
            }

            completed.Set();
        };

        Console.WriteLine("Background Worker Started");

        worker.RunWorkerAsync();

        completed.Wait();
    }

    public static void RunRaceConditionExample()
    {
        int counter = 0;
        int iterations = 1_000_000;

        Parallel.For(0, iterations, i =>
        {
            counter++;
        });

        Console.WriteLine("Race Condition Example:");
        Console.WriteLine($"Expected Counter Value: {iterations}");
        Console.WriteLine($"Actual Counter Value: {counter}");
    }

    public static void RunRaceConditionPreventionWithLock()
    {
        int counter = 0;
        int iterations = 1_000_000;
        object lockObject = new object();

        Parallel.For(0, iterations, i =>
        {
            lock (lockObject)
            {
                counter++;
            }
        });

        Console.WriteLine("\nRace Condition Prevention Using Lock:");
        Console.WriteLine($"Expected Counter Value: {iterations}");
        Console.WriteLine($"Actual Counter Value: {counter}");
    }

    public static void RunInterlockedExample()
    {
        int counter = 0;

        Parallel.For(0, 1_000_000, i =>
        {
            Interlocked.Increment(ref counter);
        });

        Console.WriteLine("Interlocked Example:");
        Console.WriteLine("Expected Counter Value: 1000000");
        Console.WriteLine($"Actual Counter Value: {counter}");
    }
    public static void RunDeadlockPreventionExample()
    {
        object lockA = new object();
        object lockB = new object();

        Task task1 = Task.Run(() =>
        {
            if (Monitor.TryEnter(lockA, TimeSpan.FromSeconds(2)))
            {
                try
                {
                    Console.WriteLine("Task 1 locked A");

                    Thread.Sleep(500);

                    if (Monitor.TryEnter(lockB, TimeSpan.FromSeconds(2)))
                    {
                        try
                        {
                            Console.WriteLine("Task 1 locked B");
                        }
                        finally
                        {
                            Monitor.Exit(lockB);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Task 1 could not lock B");
                    }
                }
                finally
                {
                    Monitor.Exit(lockA);
                }
            }
        });

        Task task2 = Task.Run(() =>
        {
            if (Monitor.TryEnter(lockB, TimeSpan.FromSeconds(2)))
            {
                try
                {
                    Console.WriteLine("Task 2 locked B");

                    Thread.Sleep(500);

                    if (Monitor.TryEnter(lockA, TimeSpan.FromSeconds(2)))
                    {
                        try
                        {
                            Console.WriteLine("Task 2 locked A");
                        }
                        finally
                        {
                            Monitor.Exit(lockA);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Task 2 could not lock A");
                    }
                }
                finally
                {
                    Monitor.Exit(lockB);
                }
            }
        });

        Task.WaitAll(task1, task2);

        Console.WriteLine("Deadlock Prevention Completed");
    }
}