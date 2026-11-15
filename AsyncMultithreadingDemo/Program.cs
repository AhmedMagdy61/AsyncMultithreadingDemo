
using AsyncMultithreadingDemo;

Console.WriteLine("======================================");
Console.WriteLine(" Async Programming & Multithreading");
Console.WriteLine(" Codveda Internship - Level 3");
Console.WriteLine("======================================");

while (true)
{
    Console.WriteLine("\nSelect an example:");
    Console.WriteLine("1. Async Download with Task.WhenAll");
    Console.WriteLine("2. CancellationToken");
    Console.WriteLine("3. Async Exception Handling");
    Console.WriteLine("4. Create and Manage Thread");
    Console.WriteLine("5. Background Task with Task.Run");
    Console.WriteLine("6. Parallel Processing");
    Console.WriteLine("7. Compare Execution Time");
    Console.WriteLine("8. ThreadPool");
    Console.WriteLine("9. BackgroundWorker");
    Console.WriteLine("10. Race Condition and Lock");
    Console.WriteLine("11. Interlocked");
    Console.WriteLine("12. Deadlock Prevention");
    Console.WriteLine("13. Exit");

    Console.Write("\nEnter your choice: ");
    string? choice = Console.ReadLine();

    Console.WriteLine();

    try
    {
        switch (choice)
        {
            case "1":
                Task task1 = AsyncExamples.DownloadFileAsync("File 1");
                Task task2 = AsyncExamples.DownloadFileAsync("File 2");
                Task task3 = AsyncExamples.DownloadFileAsync("File 3");
                await Task.WhenAll(task1, task2, task3);
                break;

            case "2":
                using (var cts = new CancellationTokenSource())
                {
                    Task task = AsyncExamples.ProcessWithCancellationAsync(cts.Token);
                    await Task.Delay(2000);
                    Console.WriteLine("Canceling operation...");
                    cts.Cancel();

                    try
                    {
                        await task;
                    }
                    catch (OperationCanceledException)
                    {
                        Console.WriteLine("Operation was canceled!");
                    }
                }
                break;

            case "3":
                Task[] tasks =
                {
                    AsyncExamples.ProcessDataAsync("Task 1", false),
                    AsyncExamples.ProcessDataAsync("Task 2", true),
                    AsyncExamples.ProcessDataAsync("Task 3", false)
                };

                try
                {
                    await Task.WhenAll(tasks);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                break;

            case "4":
                AsyncExamples.RunWorkerThread();
                break;

            case "5":
                await AsyncExamples.RunBackgroundTaskAsync();
                break;

            case "6":
                AsyncExamples.RunParallelTasks();
                break;

            case "7":
                AsyncExamples.CompareExecutionTime();
                break;

            case "8":
                AsyncExamples.RunThreadPoolExample();
                break;

            case "9":
                AsyncExamples.RunBackgroundWorkerExample();
                break;

            case "10":
                AsyncExamples.RunRaceConditionExample();
                AsyncExamples.RunRaceConditionPreventionWithLock();
                break;

            case "11":
                AsyncExamples.RunInterlockedExample();
                break;

            case "12":
                AsyncExamples.RunDeadlockPreventionExample();
                break;

            case "13":
                Console.WriteLine("Goodbye!");
                return;

            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Unexpected error: {ex.Message}");
    }

    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
    Console.Clear();
}
