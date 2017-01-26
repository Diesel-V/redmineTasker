using System;
using System.Collections.Generic;
using RedmineTasker.AuthModel.Auth;
using RedmineTasker.DAL;
using RedmineTasker.DAL.RedmineModels;

namespace RedmineTasker.BL
{
    public class RequestService
    {
        private readonly RequestRepository _requestRepository;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="host"></param>
        /// <param name="redmineCredentials"></param>
        public RequestService(Uri redmineUri, RedmineCredentials redmineCredentials)
        {
            _requestRepository = new RequestRepository(redmineUri, redmineCredentials);
        }

        public void GetUsers()
        {
            _requestRepository.GetUsers();
        }
        public Projects GetProject()
        {
            return _requestRepository.GetProjects();
        }

        public Trackers GetTrackers()
        {
            return _requestRepository.GetTrackers();
        }

        public IssueStatuses GetIssuesStatuses()
        {
            return _requestRepository.GetIssueStatuses();
        }

        public IssuePriorities GetIssuesPriority()
        {
            return _requestRepository.GetIssuePriority();
        }

        public List<Members> GetProjectMemberships(string projectName)
        {
            return _requestRepository.GetProjectMemberships(projectName);
        }
        public Versions GetProjectVersions(string projectName)
        {
            return _requestRepository.GetProjectVersions(projectName);
        }

        public IssueCategories GetProjectCategories(string projectName)
        {
            return _requestRepository.GetProjectCategories(projectName);
        }

        public void PostTask()
        {
            _requestRepository.PostTask();
        }
    }
}
