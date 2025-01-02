using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AreTheyUsingAI.Models
{
    public class Image
    {
        public long Id { get; set; }
        public long PostId { get; set; }
        public string ImageName { get; set; }
        public byte[] ImageData { get; set; }

        //public
    }
}
