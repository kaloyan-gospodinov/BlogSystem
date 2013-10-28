using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystemClient.Models
{
    public class Article : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public IList<ImageModel> Images { get; set; }
        public string ImageSource { get; set; }
        public DateTime Date { get; set; }
        public ICollection<CommentModel> Comments { get; set; }
        public ICollection<VoteModel> Votes { get; set; }
        private int votesUp;

        public int VotesUp
        {
            get { return this.votesUp; }
            set 
        {
            this.votesUp = value;
            this.OnPropertyChanged("VotesUp");
        }

        }

        private int votesDown;
        public int VotesDown
        {
            get { return this.votesDown; }
            set
            {
                this.votesDown = value;
                this.OnPropertyChanged("VotesDown");
            }
        }

        public int For 
        { 
            get 
            {
                var count = this.Votes.Count(x => x.Value == true);
                return count;
            }
            
        }
        public int Against
        {
            get
            {
                return this.Votes.Count(x => x.Value == false);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
