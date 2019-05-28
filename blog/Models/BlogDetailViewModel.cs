using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models
{
    public class BlogDetailViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdated { get; set; } = null;
        public BlogDetailViewModel() { }
        public BlogDetailViewModel(Blog x)
        {
            Id = x.Id;
            Title = x.Title;
            Content = x.Content;
            ImageUrl = x.ImageUrl;
            CreateDate = x.CreateDate;
            LastUpdated = x.LastUpdated;
        }
    }
}
