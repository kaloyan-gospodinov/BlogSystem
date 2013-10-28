using BlogSystemClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BlogSystemClient.Commands
{
    public static class CommandsBehavior
    {
        private static void ExecuteClickCommand(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as UIElement;
            if (control == null)
            {
                return;
            }
            if ((e.NewValue != null) && (e.OldValue == null))
            {
                control.MouseLeftButtonDown += (snd, args) =>
                {
                    var command = (snd as UIElement).GetValue(CommandsBehavior.ClickProperty) as ICommand;
                    command.Execute(args);
                };
            }
        }

        private static void ExecuteLoadedCommand(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as FrameworkElement;
            if (element == null)
            {
                return;
            }

            if ((e.NewValue != null) && (e.OldValue == null))
            {
                element.Loaded += (snd, args) =>
                {
                    var command = (snd as FrameworkElement).GetValue(CommandsBehavior.LoadedProperty) as ICommand;
                    command.Execute(args);
                };
            }

        }

        public static ICommand GetClick(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(ClickProperty);
        }

        public static void SetClick(DependencyObject obj, ICommand value)
        {
            obj.SetValue(ClickProperty, value);
        }


        // Using a DependencyProperty as the backing store for Click.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClickProperty =
            DependencyProperty.RegisterAttached("Click",
                typeof(ICommand),
                typeof(CommandsBehavior),
                new PropertyMetadata(ExecuteClickCommand));

        public static Article GetCurrentArticle(DependencyObject obj)
        {
            return (Article)obj.GetValue(CurrentArticleProperty);
        }

        public static void SetCurrentArticle(DependencyObject obj, Article value)
        {
            obj.SetValue(CurrentArticleProperty, value);
        }

        // Using a DependencyProperty as the backing store for CurrentArticle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentArticleProperty =
            DependencyProperty.RegisterAttached("CurrentArticle", typeof(Article), typeof(CommandsBehavior));


        public static ICommand GetLoaded(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(LoadedProperty);
        }

        public static void SetLoaded(DependencyObject obj, ICommand value)
        {
            obj.SetValue(LoadedProperty, value);
        }

        // Using a DependencyProperty as the backing store for Load.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoadedProperty =
            DependencyProperty.RegisterAttached("Loaded", typeof(ICommand), typeof(CommandsBehavior), new PropertyMetadata(ExecuteLoadedCommand));


    }
}
