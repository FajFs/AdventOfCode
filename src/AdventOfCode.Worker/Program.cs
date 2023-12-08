using AdventOfCode.Common;
using AdventOfCode.Worker;
using AdventOfCode.Worker.Extensions;
using AdventOfCode.Worker.Interfaces;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((hostContext, services) =>
{
    services.AddAdventOfCodeCommon();
    services.AddRangeTransient<IDay>();
});

var app = builder.Build();

await app.Services.GetRequiredService<DayNine>().ExecuteAsync();

