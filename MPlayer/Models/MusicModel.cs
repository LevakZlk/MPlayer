using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPlayer.Models
{
    public class MusicModel
    {
        [Required]
        public string Name { get; set; }

        public string Id { get; set; }
        public string GenreName { get; set; }
        public string SingerName { get; set; }
        public string Path { get; set; }
        public string PlaylistName { get; set; }

        public string SearchField { get; set; }
        public string SearchFieldId { get; set; }
        public static IEnumerable<SelectListItem> GetSearchType =new List<SelectListItem>()
        {
            new SelectListItem() { Text = "Name", Value = "0"},
            new SelectListItem() { Text = "Singer", Value = "1"},
            new SelectListItem() { Text = "Genre", Value = "2"}

        };
        public string DeleteMusic { get; set; }
    }
}