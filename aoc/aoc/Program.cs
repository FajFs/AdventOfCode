using AdventOfCode.Common;
using AdventOfCode.Worker;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((hostContext, services) =>
{
    services.AddAdventOfCodeCommon();
    services.AddRangeTransient<IDay>();
});

var app = builder.Build();

await app.Services.GetRequiredService<DayOne>().ExecuteAsync();

