using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Objects
{
    /// <summary>
    /// Summary description for MatchEvent
    /// </summary>
    public class MatchEvent
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int EventMinute { get; set; }
        public int ElapsedSeconds { get; set; }
        public int TeamId { get; set; }
        public string Description { get; set; }
        public string FullDescription { get; set; }
        public string EventTypeIcon { get; set; }
        public string EventType { get; set; }
        public int EventTypeEnum { get; set; }
        public int PlayerId { get; set; }
        public string Player { get; set; }
        public string Identifier { get; set; }
        public string AssistPlayers { get; set; }
        public string AssistPlayerNames { get; set; }
        public string Modifier { get; set; }
        public string Score { get; set; }
        public string IsGoal { get; set; }
    }
}