using ClassLibrary1;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", async () =>
{
    await Class1.RunAndCatchAsync(async () =>
    {
        await Class1.ThrowAsync();
    });

    return "Hello World!";
});

app.MapGet("/disable-rethrow", async () =>
{
    await Class1.RunAndCatchAndRethrowAsync(async () =>
    {
        await Class1.ThrowAsync();
    });
});


app.MapGet("/disable-throw-new", async () =>
{
    await Class1.RunAndCatchAndThrowNewAsync(async () =>
    {
        await Class1.ThrowAsync();
    });
});

app.MapGet("/tuple", async () =>
{
    await Class1.RunAndCatchTupleAsync(async () =>
    {
        await Class1.ThrowAsync();
    });
});

app.MapGet("/nested", async () =>
{
    await Class1.RunNestedAsync(async () =>
    {
        await Class1.ThrowAsync();
    });
});

app.MapGet("/disable-async", async () =>
{
    await Class1.DisableUserUnhandledExceptionsAsync(async () =>
    {
        await Class1.ThrowAsync();
    });

    return "Hello World!";
});

app.MapGet("/disable-sync", async () =>
{
    await Class1.DisableUserUnhandledExceptionsAndStoreTaskAsync(() =>
    {
        Class1.ThrowImmediately();
        return Task.CompletedTask;
    });

    return "Hello World!";
});

app.Run();
