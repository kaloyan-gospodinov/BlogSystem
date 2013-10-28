using BlogSystemClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystemClient.Data
{
    public class DataPersister
    {
        private const string BaseUrl = "http://blogsystem-1.apphb.com/api/";
        private static HttpRequester requester = new HttpRequester(BaseUrl);

        public static SessionKeyModel RegisterUser(string username, string authCode)
        {
            string registerUrl = "users/register";
            UserModel user = new UserModel
            {
                Username = username,
                Password = authCode
            };
            var response = requester.Post<SessionKeyModel>(registerUrl, user);
            return response;
        }

        public static SessionKeyModel LoginUser(string username, string authCode)
        {
            string loginUrl = "users/login";
            UserModel user = new UserModel
            {
                Username = username,
                Password = authCode
            };
            var response = requester.Post<SessionKeyModel>(loginUrl, user);
            return response;
        }

        public static void CreateArticle(string author, string title, string content, byte[] image, string sessionKey)
        {
            string loginUrl = "Articles/create/" + sessionKey;
            ArticleModel article = new ArticleModel
            {
                Author = author,
                Title = title,
                Content = content,
                ArticleImage = image
            };
            var response = requester.Post<string>(loginUrl, article);
        }


        public static int CreateComment(int articleId, string author, string content, string sessionKey)
        {
            string commentUrl = "comments/Add?sessionKey=" + sessionKey;
            CommentModel comment = new CommentModel
            {
                ArticleId = articleId,
                Author = author,
                Content = content,
                Date = DateTime.Now
            };
            var response = requester.Post<int>(commentUrl, comment);
            return response;
        }

        public static IEnumerable<Article> GetAll()
        {
            var response = requester.Get<IEnumerable<Article>>("articles/all");
            return response;
        }

        public static Article GetArticleById(int articleId)
        {
            return requester.Get<Article>("articles/singleArticle/" + articleId);
        }

        public static int CreateSubcomment(int parentId, string author, string content, string sessionKey)
        {
            string subcommentUrl = "subComments/create?sessionKey=" + sessionKey;
            SubcommentModel subcomment = new SubcommentModel
            {
                ParentCommentId = parentId,
                Author = author,
                Content = content
            };
            var response = requester.Post<int>(subcommentUrl, subcomment);
            return response;
        }

        public static string EditArticle(string author, string title, string content, byte[] image, string sessionKey, int id)
        {
            string loginUrl = "articles/Update?sessionKey=" + sessionKey;
            ArticleModel article = new ArticleModel
            {
                Id=id,
                Author = author,
                Title = title,
                Content = content,
                ArticleImage = image
            };
            var response = requester.Post<string>(loginUrl, article);            
            return response;
        }

        public static string VoteArticle(int articleId, string author, bool voteValue, string sessionKey)
        {
            string voteUrl = "votes/create?sessionKey=" + sessionKey;
            VoteModel vote = new VoteModel
            {
                ArticleId = articleId,
                Author = author,
                Value = voteValue
            };
            var response = requester.Post<string>(voteUrl, vote);
            return response;
        }
    }
}
