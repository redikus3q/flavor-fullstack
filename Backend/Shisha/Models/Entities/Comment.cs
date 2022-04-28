using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shisha.Models.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int FlavorId { get; set; }
        public Flavor Flavor { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
