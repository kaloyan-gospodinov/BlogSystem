using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogSystemClient.Models
{
    public class VoteModel
    {
        public int Id { get; set; }

        public string Author { get; set; }

        public int ArticleId { get; set; }

        public bool Value { get; set; }
    }
}
