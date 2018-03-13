using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPlayer.Models
{
    public class MusicChangeModel
    {
        [Required]
        public string Name { get; set; }
        public string Id { get; set; }
        public static IEnumerable<SelectListItem> GetGenreSelectItems { get; set; }
        public static IEnumerable<SelectListItem> GetSingerSelectItems { get; set; }
        public string SingerId { get; set; }
        public string GenreId { get; set; }
        public string Path { get; set; }
        [Required]
        public HttpPostedFileBase File { get; set; }

    }
}