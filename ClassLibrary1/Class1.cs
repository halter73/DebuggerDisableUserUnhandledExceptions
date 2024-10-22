using System.Diagnostics;
using System.Net.NetworkInformation;

namespace ClassLibrary1;

public class Class1
{
    public static async Task ThrowAsync()
    {
        await Task.Yield();
        throw new Exception();
    }

    public static void ThrowImmediately()
    {
        throw new Exception();
    }

    public static async Task RunAndCatchAsync(Func<Task> callback)
    {
        try
        {
            await callback();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Handled async exception: {ex}");
        }
    }

    [DebuggerDisableUserUnhandledExceptions]
    public static async Task RunAndCatchAndRethrowAsync(Func<Task> callback)
    {
        try
        {
            await callback();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Handled async exception: {ex}");
            throw;
        }
    }

    [DebuggerDisableUserUnhandledExceptions]
    public static async Task RunAndCatchAndThrowNewAsync(Func<Task> callback)
    {
        try
        {
            await callback();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Handled async exception: {ex}");
            //throw new Exception();
        }

        throw new Exception();
    }


    public static async Task RunAndCatchTupleAsync(Func<Task> callback)
    {
        var ex = await AwaitAndReturnTuple(callback);
        if (ex != null)
        {
            Console.WriteLine($"Handled async exception: {ex}");
        }
    }

    [DebuggerDisableUserUnhandledExceptions]
    public static async Task RunNestedAsync(Func<Task> callback)
    {
        try
        {
            await AwaitForFun(callback);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Handled async exception: {ex}");
        }
    }

    private static Task NonAsyncMiddle(Func<Task> callback) => AwaitForFun(callback);

    private static async Task AwaitForFun(Func<Task> callback)
    {
        try
        {
            await callback();
        }
        catch
        {
            throw;
        }
    }

    [DebuggerDisableUserUnhandledExceptions]
    private static async Task<Exception?> AwaitAndReturnTuple(Func<Task> callback)
    {
        try
        {
            await callback();
        }
        catch (Exception ex)
        {
            return ex;
        }

        return null;
    }

    [DebuggerDisableUserUnhandledExceptions]
    public static async Task DisableUserUnhandledExceptionsAsync(Func<Task> callback)
    {
        try
        {
            await callback();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[DebuggerDisable...] Handled async exception: {ex}");
        }
    }

    [DebuggerDisableUserUnhandledExceptions]
    public static async Task DisableUserUnhandledExceptionsAndStoreTaskAsync(Func<Task> callback)
    {
        Task? callbackTask = null;

        try
        {
            callbackTask = callback();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[DebuggerDisable...] Handled sync exception: {ex}");
        }

        // Pretend we store the task and do something useful with it.
        await Task.Yield();

        try
        {
            if (callbackTask is not null)
            {
                //await callbackTask;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[DebuggerDisable...] Handled callbackTask exception: {ex}");
        }
    }
}
