

\# Async Programming and Multithreading in C#



\## Codveda Technologies Internship – Level 3



\### Project Overview



This project demonstrates asynchronous programming and multithreading concepts using C# and .NET 9.



It provides practical examples of concurrent execution, task management, thread synchronization, and safe access to shared resources.



\## Technologies Used



\- C#

\- .NET 9

\- Task Parallel Library (TPL)

\- Async/Await

\- Multithreading

\- Visual Studio



\## Implemented Concepts



1\. Async/Await and Task.WhenAll

2\. CancellationToken

3\. Asynchronous Exception Handling

4\. Thread Creation and Management

5\. Background Tasks using Task.Run

6\. Parallel Processing using Parallel.For

7\. Execution Time Comparison

8\. ThreadPool

9\. BackgroundWorker

10\. Race Condition and Lock

11\. Atomic Operations using Interlocked

12\. Deadlock Prevention using Monitor.TryEnter



\## Race Condition Demonstration



The project demonstrates how concurrent access to a shared variable can produce incorrect results.



| Approach | Expected Value | Actual Value |

|---|---:|---:|

| Race Condition | 1,000,000 | 132,275 |

| Lock | 1,000,000 | 1,000,000 |

| Interlocked | 1,000,000 | 1,000,000 |



\*Note: The actual Race Condition result varies between executions.\*



\## How to Run



1\. Clone the repository.

2\. Open the solution in Visual Studio or navigate to the project directory.

3\. Run the following commands:



```bash

dotnet restore

dotnet build

dotnet run --project AsyncMultithreadingDemo

```



4\. Select an example from the interactive console menu.



\## Project Structure



```text

AsyncMultithreadingDemo/

│

├── AsyncMultithreadingDemo.sln

├── README.md

│

└── AsyncMultithreadingDemo/

&#x20;   ├── AsyncMultithreadingDemo.csproj

&#x20;   ├── Program.cs

&#x20;   └── Examples/

&#x20;       └── AsyncExamples.cs

```



\## Internship Information



\*\*Organization:\*\* Codveda Technologies



\*\*Program:\*\* .NET Development Internship



\*\*Level:\*\* Level 3



\*\*Task:\*\* Async Programming and Multithreading



