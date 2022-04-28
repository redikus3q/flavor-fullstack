using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shisha.Models.Entities
{
    public class Flavor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string ImageLink { get; set; }
        public string Description { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}
