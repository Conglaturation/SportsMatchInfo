using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Objects
{
    /// <summary>
    /// Summary description for Match
    /// </summary>
    public class Match
    {
        public int Id { get; set; }
        public string Round { get; set; }
        public int RoundNumber { get; set; }
        public string MatchDate { get; set; }
        public HomeTeam HomeTeam { get; set; }
        public AwayTeam AwayTeam { get; set; }
        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }
        public int Status { get; set; }
        public int PlayedMinutes { get; set; }
        public string SecondHalfStarted { get; set; }
        public string GameStarted { get; set; }
        public IList<MatchEvent> MatchEvents { get; set; }
        public IList<string> PeriodResults { get; set; }
        public string OnlyResultAvailable { get; set; }
        public int Season { get; set; }
        public string Country { get; set; }
        public string League { get; set; }
    }
}