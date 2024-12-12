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
    }
}
