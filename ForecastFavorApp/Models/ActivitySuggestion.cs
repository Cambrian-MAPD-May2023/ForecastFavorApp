using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastFavorApp.Models
{
    /// <summary>
    /// Represents user-specific activity suggestions.
    /// 
    /// Fields:
    /// - Id: A unique identifier for each activity suggestion.
    /// - ActivityType: The type of activity recommended (e.g., hiking, biking).
    /// - WeatherConditions: Associated weather conditions recommended for the activity.
    /// - AdditionalInformation: Any extra information pertinent to the activity suggestion.
    /// </summary>


    public class ActivitySuggestion
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ActivityType { get; set; }
        public string WeatherConditions { get; set; }
        public string AdditionalInformation { get; set; }
    }


}