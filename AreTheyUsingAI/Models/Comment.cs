using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AreTheyUsingAI.Models
{
    public class Comment
    {
        public long Id { get; set; }
        public long PostId { get; set; }
        public string CommentText { get; set; }
    }
}
