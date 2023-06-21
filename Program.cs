// See https://aka.ms/new-console-template for more information

using CameraTool.Helpers;
using CameraTool.Implements;
using CameraTool.Implements.Abstractions;
string[] ldPlayerNames = new[] { "LDPlayer", "LDPlayer-1", "LDPlayer-2" };


await InitLDPlayerAsync();

await RunningAppAsync();



async Task InitLDPlayerAsync()
{
    int width = 1080;
    int height = 1920;

    var ldPlayer = new LDPlayerHelper();
    Console.WriteLine("Setting Resolution");

    int newHeight = height / ldPlayerNames.Length;
    foreach (var name in ldPlayerNames)
        ldPlayer.SetResolution(name, width, newHeight, 320);

    await Task.Delay(3000);
    Console.WriteLine("Starting LDPlayer...");
    foreach (var name in ldPlayerNames)
        ldPlayer.OpenAppWithName(name);

    await Task.Delay(40 * 1000);
    ldPlayer.SortTabs();
    Console.WriteLine("Started LDPlayer");
}

async Task RunningAppAsync()
{
    var adb = new ADBHelper();
    var app = AppFactoryMaker.GetAppFactory("point");

    var devices = await WaitArrayAsync(() => adb.GetDevices().ToArray());
    List<AppBase> apps = new List<AppBase>();

    for (int i = 0; i < devices.Count(); i++)
    {
        Console.WriteLine($"Setup device: {devices[i]}");
        switch (i)
        {
            case 0:
                var yoosee = app.CreateYoosee();
                yoosee.SetDeviceId(devices[i]);
                apps.Add(yoosee);
                continue;
            case 1:
                var eye4 = app.CreateEye4();
                eye4.SetDeviceId(devices[i]);
                apps.Add(eye4);
                continue;
            case 2:
                var mi = app.CreateMI();
                mi.SetDeviceId(devices[i]);
                apps.Add(mi);
                continue;
        }
    }

    var tasks = apps.Select(s => s.RunAsync());
    await Task.WhenAll(tasks);
    Console.WriteLine("Runned complete");
}

async Task<T[]> WaitArrayAsync<T>(Func<T[]> func)
{
    while (true)
    {
        var result = func();
        if (result.Length != 0) return result;
        await Task.Delay(1000);
    }
}

async Task<T> WaitAsync<T>(Func<T> func)
{
    while (true)
    {
        var result = func();
        if (result is not null) return result;
        await Task.Delay(1000);
    }
}