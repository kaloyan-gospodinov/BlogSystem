using BlogSystemClient.Commands;
using BlogSystemClient.Data;
using BlogSystemClient.Helpers;
using BlogSystemClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BlogSystemClient.ViewModels
{
    public class CreateCommentViewModel : ViewModelBase, IPageViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int ArticleId { get; set; }

        private ICommand createCommentCommand;

        public ICommand CreateComment
        {
            get
            {
                if (this.createCommentCommand == null)
                {
                    this.createCommentCommand = new RelayCommand(this.HandleCreateCommentCommand);
                }

                return this.createCommentCommand;
            }
        }

        Article Article { get; set; }

        public void SetArticle(Article article)
        {
            this.Article = article;
            this.ArticleId = article.Id;
        }

        public event EventHandler<SingleArticleEventArgs> BackToSingleArticle;

        private void HandleCreateCommentCommand(object parameter)
        {
            var content = this.Content;
            var author = LoginRegisterViewModel.Username;
            var sessionKey = LoginRegisterViewModel.SessionKey;
            var article = this.ArticleId;

            try
            {
                this.Id = DataPersister.CreateComment(article, author, content, sessionKey);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.Article.Comments.Add(new CommentModel()
            {
                ArticleId = this.Article.Id,
                Id=this.Id,
                Author = author,
                Content = content
            });

            this.BackToSingleArticle(this, new SingleArticleEventArgs() { 
                choosenArticle=this.Article
            });
        }

        public string Name
        {
            get { return "Create comment form"; }
        }
    }
}
