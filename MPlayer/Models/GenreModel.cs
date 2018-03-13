using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MPlayer.Models
{
    public class GenreModel
    {
        [Required]
        public string Name { get; set; }

        public string Id { get; set; }
    }
}