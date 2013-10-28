using BlogSystemClient.Commands;
using BlogSystemClient.Helpers;
using BlogSystemClient.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BlogSystemClient.ViewModels
{
    public class MasterViewModel : ViewModelBase
    {
        private IPageViewModel currentViewModel;
        private ICommand logoutCommand;
        public ICommand Logout
        {
            get
            {
                if (this.logoutCommand == null)
                {
                    this.logoutCommand = new RelayCommand(this.HandleLogoutCommand);
                }

                return this.logoutCommand;
            }
        }

        public bool IsLogoutVisible{get;set;}

        private void HandleLogoutCommand(object obj)
        {
            this.IsLogoutVisible = false;
            OnPropertyChanged("IsLogoutVisible");
            this.NavigateToLoginRegister(this, null);
        }

        public LoginRegisterViewModel LoginRegisterViewModel { get; set; }

        public ArticlesViewModel ArticlesViewModel { get; set; }

        public CreateArticleViewModel CreateArticleViewModel { get; set; }

        public CreateCommentViewModel CreateCommentViewModel { get; set; }

        public CreateSubcommentViewModel CreateSubcommentViewModel { get; set; }

        public EditArticleViewModel EditArticleViewModel { get; set; }

        public SingleArticleViewModel SingleArticleViewModel { get; set; }

        public IPageViewModel CurrentViewModel
        {
            get
            {
                return this.currentViewModel;
            }
            set
            {
                this.currentViewModel = value;
                OnPropertyChanged("CurrentViewModel");
            }
        }

        public void NavigateToHome(object sender, EventArgs e)
        {
            this.CurrentViewModel = this.ArticlesViewModel;
            this.IsLogoutVisible = true;
            OnPropertyChanged("IsLogoutVisible");
            this.ArticlesViewModel.UpdateArticles();
            
        }

        public void NavigateToLoginRegister(object sender, EventArgs e)
        {
            this.CurrentViewModel = this.LoginRegisterViewModel;
        }

        public void NavigateToSingleArticle(object sender, SingleArticleEventArgs e)
        {
            this.CurrentViewModel = this.SingleArticleViewModel;
            this.SingleArticleViewModel.SetArticle(e.choosenArticle);
        }

        public void NavigateToCurrentArticle(object sender, EventArgs e)
        {
            this.CurrentViewModel = this.SingleArticleViewModel;
        }

        public void NavigateToCreateArticle(object sender, EventArgs e)
        {
            this.CurrentViewModel = this.CreateArticleViewModel;
        }

        public void NavigateToEditArticle(object sender, SingleArticleEventArgs e)
        {
            
            this.CurrentViewModel = this.EditArticleViewModel;
            this.EditArticleViewModel.SetChoosenArticle(e.choosenArticle);
        }

        public void NavigateToCreateSubcomment(object sender, CommentModel comment)
        {
            this.CurrentViewModel = this.CreateSubcommentViewModel;
            this.CreateSubcommentViewModel.SetComment(comment);
        }

        public void NavigateToCreateComment(object sender, SingleArticleEventArgs article)
        {
            this.CurrentViewModel = this.CreateCommentViewModel;
            this.CreateCommentViewModel.SetArticle(article.choosenArticle);
        }

        public MasterViewModel()
        {
            this.IsLogoutVisible = false;
            this.LoginRegisterViewModel = new LoginRegisterViewModel();
            this.LoginRegisterViewModel.LoginRegisterSuccess += this.NavigateToHome;

            this.CreateArticleViewModel = new CreateArticleViewModel();
            this.CreateArticleViewModel.CreateArticleSuccess += this.NavigateToHome;

            this.CreateCommentViewModel = new CreateCommentViewModel();
            this.CreateCommentViewModel.BackToSingleArticle += NavigateToSingleArticle;
            if (this.ArticlesViewModel == null)
            {
                this.ArticlesViewModel = new ArticlesViewModel();
                this.ArticlesViewModel.openSingleArticle += this.NavigateToSingleArticle;
                this.ArticlesViewModel.OpenCreateArticle += this.NavigateToCreateArticle;
            }
            if (this.SingleArticleViewModel == null)
            {
                this.SingleArticleViewModel = new SingleArticleViewModel();
                this.SingleArticleViewModel.OpenEditArticle += this.NavigateToEditArticle;
                this.SingleArticleViewModel.OpenAllArticles += this.NavigateToHome;
                this.SingleArticleViewModel.OpenAddComment += this.NavigateToCreateComment;
                this.SingleArticleViewModel.OpenAddSubComment += this.NavigateToCreateSubcomment;
            }
            this.EditArticleViewModel = new EditArticleViewModel();
            this.EditArticleViewModel.OpenAllArticles += this.NavigateToHome;
            this.CreateSubcommentViewModel = new CreateSubcommentViewModel();
            this.CreateSubcommentViewModel.BackToArticle += this.NavigateToCurrentArticle;
            this.CurrentViewModel = this.LoginRegisterViewModel;
        }

    }
}
