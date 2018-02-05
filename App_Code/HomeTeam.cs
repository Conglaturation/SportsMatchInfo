using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Objects
{
    /// <summary>
    /// Summary description for HomeTeam
    /// </summary>
    public class HomeTeam
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Logo { get; set; }
        public string LogoUrl { get; set; }
        public int Ranking { get; set; }
        public string Message { get; set; }
    }
}