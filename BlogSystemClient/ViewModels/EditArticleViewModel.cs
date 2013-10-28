using BlogSystemClient.Commands;
using BlogSystemClient.Data;
using BlogSystemClient.Helpers;
using BlogSystemClient.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BlogSystemClient.ViewModels
{
    public class EditArticleViewModel : ViewModelBase, IPageViewModel
    {
        public Article Article { get; set; }

        private ICommand editCommant;
        private ICommand getImageCommand;

        public event EventHandler<SingleArticleEventArgs> EditArticleSuccess;

        public string Name
        {
            get { return "Edit Article Form"; }
        }

        public EditArticleViewModel(Article article = null)
        {

        }

        public void SetChoosenArticle(Article article)
        {
            if (article != null)
            {
                this.Article = article;
            }
        }

        public ICommand EditArticle
        {
            get
            {
                if (this.editCommant == null)
                {
                    this.editCommant = new RelayCommand(this.HandleEditArticleCommand);
                }

                return this.editCommant;
            }

        }

        public ICommand GetImage
        {
            get
            {
                if (this.getImageCommand == null)
                {
                    this.getImageCommand = new RelayCommand(this.HandleGetImageCommand);
                }

                return this.getImageCommand;
            }

        }

        public event EventHandler OpenAllArticles;

        private void HandleEditArticleCommand(object obj)
        {
            var author = LoginRegisterViewModel.Username;
            var authCode = LoginRegisterViewModel.SessionKey;
            var title = this.Article.Title;
            var content = this.Article.Content;
            var imageToBytes = this.Article.Images[0].Image;

            try
            {
                var response = DataPersister.EditArticle(author, title, content, imageToBytes, authCode, this.Article.Id);
                if (this.EditArticleSuccess != null)
                {
                    this.EditArticleSuccess(this, new SingleArticleEventArgs
                    {
                        choosenArticle = this.Article
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.OpenAllArticles(this, null);
        }

        private void HandleGetImageCommand(object obj)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            //fileDialog.Multiselect = true;
            fileDialog.ShowDialog();
            if (!fileDialog.FileNames.Any())
            {
                return;
            }
            var image = fileDialog.FileName;
            Bitmap bitmapImage = new Bitmap(image);
            MemoryStream stream = new MemoryStream();
            bitmapImage.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            this.Article.Images[0].Image = stream.ToArray();
            OnPropertyChanged("ImageSources");
        }
    }
}
