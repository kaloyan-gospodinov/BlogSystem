using BlogSystemClient.Commands;
using BlogSystemClient.Data;
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
    public class CreateArticleViewModel : ViewModelBase, IPageViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        private byte[] imageToBytes;

        public event EventHandler CreateArticleSuccess;

        private ICommand createCommand;
        private ICommand getImageCommand;

        public string Name
        {
            get { return "Create Article Form"; }
        }

        public ICommand CreateArticle
        {
            get
            {
                if (this.createCommand == null)
                {
                    this.createCommand = new RelayCommand(this.HandleCreateArticleCommand);
                }

                return this.createCommand;
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

        private void HandleCreateArticleCommand(object obj)
        {
            var author = LoginRegisterViewModel.Username;
            var authCode = LoginRegisterViewModel.SessionKey;
            var title = this.Title;
            var content = this.Content;

            try
            {

                if (string.IsNullOrWhiteSpace(title))
                {
                    throw new Exception("You have not written a title to the blog post!");
                }

                if (string.IsNullOrWhiteSpace(content))
                {
                    throw new Exception("You have not written any content!");
                }

                if (imageToBytes == null)
                {
                    throw new Exception("You have not selected any image to be posted in the blog article!");
                }

                DataPersister.CreateArticle(author, title, content, imageToBytes, authCode);
                if (this.CreateArticleSuccess != null)
                {
                    this.CreateArticleSuccess(this, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            imageToBytes = stream.ToArray();
            OnPropertyChanged("ImageSources");
        }
    }
}
