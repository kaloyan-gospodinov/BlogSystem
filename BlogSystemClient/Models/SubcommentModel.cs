using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystemClient.Models
{
    public class SubcommentModel
    {
        public int Id { get; set; }

        public int ParentCommentId { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public DateTime Date { get; set; }
    }
}
