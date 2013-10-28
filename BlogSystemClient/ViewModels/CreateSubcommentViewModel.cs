using BlogSystemClient.Commands;
using BlogSystemClient.Data;
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
    public class CreateSubcommentViewModel : ViewModelBase, IPageViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int ParentCommentId { get; set; }

        public CommentModel Comment { get; set; }

        public event EventHandler BackToArticle;

        private ICommand createSubcommentCommand;

        public ICommand CreateSubcomment
        {
            get
            {
                if (this.createSubcommentCommand == null)
                {
                    this.createSubcommentCommand = new RelayCommand(this.HandleCreateSubcommentCommand);
                }

                return this.createSubcommentCommand;
            }
        }

        public void SetComment(CommentModel comment)
        {
            this.Comment = comment;
            this.ParentCommentId = comment.Id;
        }

        private void HandleCreateSubcommentCommand(object parameter)
        {
            var content = this.Content;
            var author = LoginRegisterViewModel.Username;
            var sessionKey = LoginRegisterViewModel.SessionKey;
            var parent = this.ParentCommentId;

            try
            {
                this.Id = DataPersister.CreateSubcomment(parent, author, content, sessionKey);
                if (this.BackToArticle != null)
                {
                    this.BackToArticle(this, null);
                }
                if (this.Comment.SubComments == null)
                {
                    this.Comment.SubComments = new List<SubcommentModel>();
                }
                    this.Comment.SubComments.Add(new SubcommentModel()
                    {
                        Content = content,
                        Author = author
                    });
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string Name
        {
            get { return "Create subcomment form"; }
        }
    }
}
