using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPlayer.Models
{
    public class PlaylistChangeModel
    {

        [Required]
        public string Name { get; set; }
        public string Id { get; set; }
        public static MultiSelectList GetMusicSelectItems { get; set; }
        public IEnumerable<string> MusicsId { get; set; }
    }
}