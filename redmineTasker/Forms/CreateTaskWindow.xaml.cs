using System;
using System.Linq;
using System.Windows;
using RedmineTasker.AuthModel.Auth;
using RedmineTasker.BL;
using RedmineTasker.DAL.RedmineModels;

namespace redmineTasker.Forms
{
    /// <summary>
    /// Interaction logic for CreateTaskWindow.xaml
    /// </summary>
    public partial class CreateTaskWindow : Window
    {
        private readonly RequestService _requestService;
        private Project _curentProject;

        public CreateTaskWindow(RedmineCredentials credentials, Uri redmineUri, Window owner = null)
        {
            Owner = owner;
            InitializeComponent();
            _requestService = new RequestService(redmineUri, credentials);
            RedmineProjectComboBox.SelectedIndex = 0;
        }
 
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Owner = null;
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = this;

            var redmineProjects = _requestService.GetProject();
            _curentProject = redmineProjects.RedmineProjects.FirstOrDefault();
            var redmineTaskers = _requestService.GetTrackers();
            var redmineIssueStatus = _requestService.GetIssuesStatuses();
            var redmineIssuePriority = _requestService.GetIssuesPriority();


            foreach (var item in redmineProjects.RedmineProjects)
            {
                RedmineProjectComboBox.Items.Add(item);
            }
            RedmineProjectComboBox.SelectionChanged += RedmineProjectComboBox_SelectionChanged;

            foreach (var item in redmineTaskers.IssueTrackers)
            {
                RedmineTaskerComboBox.Items.Add(item);
            }

            foreach (var item in redmineIssueStatus.TaskStatus)
            {
                RedmineStatusComboBox.Items.Add(item);
            }

            foreach (var item in redmineIssuePriority.TaskPriority)
            {
                RedminePriorityComboBox.Items.Add(item);
            }

            if (_curentProject != null)
            {
                var redmineMembership = _requestService.GetProjectMemberships(_curentProject.Name);
                foreach (var item in redmineMembership)
                {
                    RedmineAppointedComboBox.Items.Add(item);
                }
            }
        }

        private void RedmineProjectComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _curentProject = (Project)RedmineProjectComboBox.SelectionBoxItem;

            var redmineMembership = _requestService.GetProjectMemberships(_curentProject.Name);
            /*var redmineProjectVersion = _requestService.GetProjectVersions(_curentProject.name);
            var redmineProjectCategory = _requestService.GetProjectCategories(_curentProject.name);*/

            foreach (var item in redmineMembership)
            {
                RedmineAppointedComboBox.Items.Add(item);
            }
            /*foreach (var item in redmineProjectVersion.Versions)
            {
                RedmineVersionComboBox.Items.Add(item);
            }
            foreach (var item in redmineProjectCategory.IssueCategory)
            {
                RedmineCategoryComboBox.Items.Add(item);
            }*/
        }
    }
}
