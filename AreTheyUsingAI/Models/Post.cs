using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AreTheyUsingAI.Models
{
    public class Post
    {
        public long Id { get; set; }
        public string PostTitle { get; set; }
        public string PostDesc { get; set; }
        public int ThumbsUp { get; set; } = 0;
        public int ThumbsDown { get; set; } = 0;

        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Image> Images { get; set; } = new List<Image>();
        public List<Link> Links { get; set; } = new List<Link>();
    }
}
