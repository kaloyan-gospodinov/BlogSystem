using BlogSystemClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystemClient.Helpers
{
    public class SingleArticleEventArgs:EventArgs
    {
        public Article choosenArticle { get; set; }
    }
}
