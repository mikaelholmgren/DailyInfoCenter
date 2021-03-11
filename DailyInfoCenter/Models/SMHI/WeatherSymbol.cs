using System.ComponentModel.DataAnnotations;

namespace DailyInfoCenter.Models.SMHI
{
    public enum WeatherSymbol
    {
        [Display(Name = "Klart")]
        ClearSky = 1,
        [Display(Name = "Nästan klart")]
        NearlyClearSky,
        [Display(Name = "Växlande molnighet")]
        VariableCloudiness,
        [Display(Name = "Halvklart")]
        HalfclearSky,
        [Display(Name = "Molnigt")]
        CloudySky,
        [Display(Name = "Mulet")]
        Overcast,
        [Display(Name = "Dimmigt")]
        Fog,
        [Display(Name = "lätta regnskurar")]
        LightRainShowers,
        [Display(Name = "medel regnskurar")]
        ModerateRainShowers,
        HeavyRainShowers,
        Thunderstorm,
        LightSleetShowers,
        ModerateSleetShowers,
        HeavySleetShowers,
        LightSnowShowers,
        ModerateSnowShowers,
        HeavySnowShowers,
        LightRain,
        ModerateRain,
        HeavyRain,
        Thunder,
        LightSleet,
        ModerateSleet,
        HeavySleet,
        [Display(Name = "lätta snöbyar")]
        LightSnowfall,
        ModerateSnowfall,
        HeavySnowfall
    }
}