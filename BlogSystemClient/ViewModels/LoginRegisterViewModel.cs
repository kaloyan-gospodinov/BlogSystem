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
    public class LoginRegisterViewModel : ViewModelBase, IPageViewModel
    {
        public static string Username { get; set; }
        private ICommand loginCommand;
        private ICommand registerCommand;
        public static string SessionKey { get; set; }

        public event EventHandler LoginRegisterSuccess;

        public string Name
        {
            get { return "Login Form"; }
        }

        public ICommand Login
        {
            get
            {
                if (this.loginCommand == null)
                {
                    this.loginCommand = new RelayCommand(this.HandleLoginCommand);
                }

                return this.loginCommand;
            }
        }

        public ICommand Register
        {
            get
            {
                if (this.registerCommand == null)
                {
                    this.registerCommand = new RelayCommand(this.HandleRegisterCommand);
                }

                return this.registerCommand;
            }
        }

        private void HandleLoginCommand(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            string password = passwordBox.Password;
            string authCode = this.PasswordToSha1(password, Encoding.UTF8);
            try
            {
                var sessionKeyModel = DataPersister.LoginUser(Username, authCode);
                if (sessionKeyModel.SessionKey != null)
                {
                    if (this.LoginRegisterSuccess != null)
                    {
                        SessionKey = sessionKeyModel.SessionKey;
                        Username = sessionKeyModel.Username;
                        this.LoginRegisterSuccess(this, null);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void HandleRegisterCommand(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            string password = passwordBox.Password;
            string authCode = this.PasswordToSha1(password, Encoding.UTF8); try
            {
                var sessionKeyModel = DataPersister.RegisterUser(Username, authCode);
                if (sessionKeyModel.SessionKey != null)
                {
                    if (this.LoginRegisterSuccess != null)
                    {
                        SessionKey = sessionKeyModel.SessionKey;
                        Username = sessionKeyModel.Username;
                        this.LoginRegisterSuccess(this, null);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string PasswordToSha1(string password, Encoding enc)
        {
            byte[] buffer = enc.GetBytes(password);
            SHA1CryptoServiceProvider cryptoTransformSHA1 =
                new SHA1CryptoServiceProvider();
            string hash = BitConverter.ToString(
                cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");
            return hash;
        }
    }
}
