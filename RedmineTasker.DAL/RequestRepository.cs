using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using RedmineTasker.AuthModel.Auth;
using RedmineTasker.DAL.RedmineModels;

namespace RedmineTasker.DAL
{
    public class RequestRepository
    {
        private readonly Uri _redmineUri;
        private readonly RedmineCredentials _credentials;

        public RequestRepository(Uri redmineUri, RedmineCredentials credentials)
        {
            _redmineUri = redmineUri;
            _credentials = credentials;
        }

        private string GetRequest (string path)
        {
            var url = new UriBuilder(_redmineUri);
            url.Path += path;
            
            var request = (HttpWebRequest)WebRequest.Create(url.Uri);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
            request.Method = WebRequestMethods.Http.Get;
            if (_credentials.ApiKeyCredentials != null)
            {
                request.Headers.Add("X-Redmine-API-Key", _credentials.ApiKeyCredentials.ApiKey);
            }
            else if (_credentials.LoginPasswordCredentials != null)
            {
                request.Credentials = new CredentialCache
                {
                    {
                        url.Uri, "Basic",
                        new NetworkCredential(_credentials.LoginPasswordCredentials.UserName,
                            _credentials.LoginPasswordCredentials.UserPassword)
                    }
                };
                request.PreAuthenticate = true;
            }

                var temp = request.GetResponse();

            using (var stream = temp.GetResponseStream())
            {
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private void PostRequest(string path, string postData)
        {
            var url = new UriBuilder(_redmineUri);
            url.Path += path;
            
            var request = (HttpWebRequest)WebRequest.Create(url.Uri);

            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = WebRequestMethods.Http.Post;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
            if (_credentials.ApiKeyCredentials != null)
            {
                request.Headers.Add("X-Redmine-API-Key", _credentials.ApiKeyCredentials.ApiKey);
            }
            else if (_credentials.LoginPasswordCredentials != null)
            {
                request.Credentials = new CredentialCache
                {
                    {
                        url.Uri, "Basic",
                        new NetworkCredential(_credentials.LoginPasswordCredentials.UserName,
                            _credentials.LoginPasswordCredentials.UserPassword)
                    }
                };
                request.PreAuthenticate = true;
            }
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        }

        public string GetUsers(string project = "KSM")
        {
            var responce = GetRequest(@"/projects/"+ project + "/memberships.xml");
            return responce; 
        }

        public Projects GetProjects()
        {
            var responce = GetRequest(@"/projects.xml");
            return Projects.FromXml(responce);
        }

        public Trackers GetTrackers()
        {
            var responce = GetRequest(@"/trackers.xml");
            return Trackers.FromXml(responce);
        }

        public IssueStatuses GetIssueStatuses()
        {
            var responce = GetRequest(@"/issue_statuses.xml");
            return IssueStatuses.FromXml(responce);
        }
        
        public IssuePriorities GetIssuePriority()
        {
            var responce = GetRequest(@"/enumerations/issue_priorities.xml");
            return IssuePriorities.FromXml(responce);
        }

        public List<Members> GetProjectMemberships(string project = "KSM")
        {
            var responce = GetRequest(@"/projects/" + project + @"/memberships.xml");
            return Members.GetMemberShipList(responce);
        }

        public IssueCategories GetProjectCategories(string project)
        {
            var responce = GetRequest(@"/projects/" + project + @"/issue_categories.xml");
            return IssueCategories.FromXml(responce);
        }

        public Versions GetProjectVersions(string project)
        {
            var responce = GetRequest(@"/projects/" + project + @"/versions.xml");
            return Versions.FromXml(responce);
        }

        public void PostTask()
        {
            var issue = new IssueClass
            {
                ProjectId = 1,
                Subject = "example",
                PriorityId = 4
            };
            var temp = issue.ToXml();
            PostRequest(@"/issues.xml", temp);
        }

        /// <summary>
        /// медот шифрованя в base64
        /// </summary>
        /// <param name="encript">текс для шифрования</param>
        /// <returns></returns>
        private string EncryptToBase64(string encript)
        {
            if (encript == null)
            {
                //throw new ArgumentNullException(nameof(encript));
                return null;
            }

            //encrypt data
            var data = Encoding.Unicode.GetBytes(encript);
            var encrypted = ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);
            //return as base64 string
            return Convert.ToBase64String(encrypted);
        }

        /// <summary>
        /// метод дешифрования
        /// </summary>
        /// <param name="decript">текс для дешифровки</param>
        /// <returns></returns>
        private string DecryptFromBase64(string decript)
        {
            if (decript == null)
            {
                //throw new ArgumentNullException(nameof(decript));
            }

            //parse base64 string
            byte[] data = Convert.FromBase64String(decript);
            //decrypt data
            byte[] decrypted = ProtectedData.Unprotect(data, null, DataProtectionScope.CurrentUser);
            return Encoding.Unicode.GetString(decrypted);
        }
    }
}
