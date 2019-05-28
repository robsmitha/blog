using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models
{
    public class BlogCreateViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Title")]
        [Required]
        public string Title { get; set; }
        [Display(Name = "Content")]
        [Required]
        public string Content { get; set; }
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdated { get; set; } = null;
    }
}
