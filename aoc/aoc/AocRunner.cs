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
        //2 => services.GetRequiredService<DayTwo>().Run().Wait(),
        //3 => services.GetRequiredService<DayThree>().Run().Wait(),
        //4 => services.GetRequiredService<DayFour>().Run().Wait(),
        //5 => services.GetRequiredService<DayFive>().Run().Wait(),
        //6 => services.GetRequiredService<DaySix>().Run().Wait(),
        //7 => services.GetRequiredService<DaySeven>().Run().Wait(),
        //8 => services.GetRequiredService<DayEight>().Run().Wait(),
        //9 => services.GetRequiredService<DayNine>().Run().Wait(),
        //10 => services.GetRequiredService<DayTen>().Run().Wait(),
        //11 => services.GetRequiredService<DayEleven>().Run().Wait(),
        //12 => services.GetRequiredService<DayTwelve>().Run().Wait(),
        //13 => services.GetRequiredService<DayThirteen>().Run().Wait(),
        //14 => services.GetRequiredService<DayFourteen>().Run().Wait(),
        //15 => services.GetRequiredService<DayFifteen>().Run().Wait(),
        //16 => services.GetRequiredService<DaySixteen>().Run().Wait(),
        //17 => services.GetRequiredService<DaySeventeen>().Run().Wait(),
        //18 => services.GetRequiredService<DayEighteen>().Run().Wait(),
        //19 => services.GetRequiredService<DayNineteen>().Run().Wait(),
        //20 => services.GetRequiredService<DayTwenty>().Run().Wait(),
        //21 => services.GetRequiredService<DayTwentyOne>().Run().Wait(),
        //22 => services.GetRequiredService<DayTwentyTwo>().Run().Wait(),
        //23 => services.GetRequiredService<DayTwentyThree>().Run().Wait(),
        //24 => services.GetRequiredService<DayTwentyFour>().Run().Wait(),
        //25 => services.GetRequiredService<DayTwentyFive>().Run().Wait(),
        _ => throw new NotImplementedException(),
    };

}
