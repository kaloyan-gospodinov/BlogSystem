using BlogSystemClient.Commands;
using BlogSystemClient.Data;
using BlogSystemClient.Helpers;
using BlogSystemClient.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BlogSystemClient.ViewModels
{
    public class ArticlesViewModel : ViewModelBase, IPageViewModel
    {
        private string title;
        public event EventHandler<SingleArticleEventArgs> openSingleArticle;
        public event EventHandler OpenCreateArticle;
        public string Name
        {
            get
            {
                return "All articles";
            }
        }

        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
                this.OnPropertyChanged("Title");
            }
        }

        public ICommand Click { get; set; }

        public ObservableCollection<Article> Articles { get; set; }

        public ArticlesViewModel()
        {
            this.Articles = new ObservableCollection<Article>();
            string[] filePaths = Directory.GetFiles(@"..\..\Images\");
            for (int i = 0; i < filePaths.Count(); i++)
            {
                File.Delete(filePaths[i]);
            }
        }

        public void UpdateArticles() 
        {
            this.Articles.Clear();
            var articles = DataPersister.GetAll();
            foreach (var article in articles)
            {
                this.Articles.Add(article);
            }
            this.Click = new RelayCommand(this.HandleClick);
           
        }

        private void HandleClick(object obj)
        {
            var choosenArticle = obj as Article;
            if (openSingleArticle != null)
            {
                openSingleArticle(this, new SingleArticleEventArgs
                {
                    choosenArticle = choosenArticle
                });
            }
        }

        public ICommand clickOpenCreateArticle;

        public ICommand ClickOpenCreateArticle
        {
            get
            {
                if (this.clickOpenCreateArticle == null)
                {
                    this.clickOpenCreateArticle = new RelayCommand(this.HandleOpenCreateArticle);
                }

                return this.clickOpenCreateArticle;
            }
        }

        private void HandleOpenCreateArticle(object param)
        {
            this.OpenCreateArticle(this, null);
        }
    }
}
