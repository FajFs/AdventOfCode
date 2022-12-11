using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc22;

public class AocRunner
{

    public Task Run(ServiceProvider services, int day) => day switch
    {
        1 => services.GetRequiredService<DayOne>().Run(),
        2 => services.GetRequiredService<DayTwo>().Run(),
        3 => services.GetRequiredService<DayThree>().Run(),
        4 => services.GetRequiredService<DayFour>().Run(),
        5 => services.GetRequiredService<DayFive>().Run(),
        6 => services.GetRequiredService<DaySix>().Run(),
        7 => services.GetRequiredService<DaySeven>().Run(),
        8 => services.GetRequiredService<DayEight>().Run(),
        9 => services.GetRequiredService<DayNine>().Run(),
        10 => services.GetRequiredService<DayTen>().Run(),
        11 => services.GetRequiredService<DayEleven>().Run(),
        12 => services.GetRequiredService<DayTwelve>().Run(),
        //13 => services.GetRequiredService<DayThirteen>().Run(),
        //14 => services.GetRequiredService<DayFourteen>().Run(),
        //15 => services.GetRequiredService<DayFifteen>().Run(),
        //16 => services.GetRequiredService<DaySixteen>().Run(),
        //17 => services.GetRequiredService<DaySeventeen>().Run(),
        //18 => services.GetRequiredService<DayEighteen>().Run(),
        //19 => services.GetRequiredService<DayNineteen>().Run(),
        //20 => services.GetRequiredService<DayTwenty>().Run(),
        //21 => services.GetRequiredService<DayTwentyOne>().Run(),
        //22 => services.GetRequiredService<DayTwentyTwo>().Run(),
        //23 => services.GetRequiredService<DayTwentyThree>().Run(),
        //24 => services.GetRequiredService<DayTwentyFour>().Run(),
        //25 => services.GetRequiredService<DayTwentyFive>().Run(),
        _ => new Task(() => Console.WriteLine($"No solution found for day: {day}")),
    };

}
