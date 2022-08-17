using System;
using System.Collections.Generic;

namespace Better_Call_Saul.Models
{
    public partial class FavoriteEpisode
    {
        public int Id { get; set; }
        public int? Season { get; set; }
        public int? NumberWithInSeason { get; set; }
        public string? Title { get; set; }
    }
}
