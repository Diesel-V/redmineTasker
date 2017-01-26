using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace RedmineTasker.DAL.RedmineModels
{
    [Serializable]
    [XmlRoot("issue")]
    public class IssueClass
    {
        [XmlElement("project_id")]
        public int ProjectId{ get; set; }
        [XmlElement("tracker_id")]
        public int TrackerId{ get; set; }
        [XmlElement("status_id")]
        public int StatusID { get; set; }
        [XmlElement("priority_id")]
        public int PriorityId { get; set; }
        [XmlElement("subject")]
        public string Subject { get; set; }
        [XmlElement("description")]
        public string Description { get; set; }
        [XmlElement("category_id")]
        public int CategoryId { get; set; }
        [XmlElement("fixed_version_id")]
        public int FixedVersionId { get; set; }
        [XmlElement("assigned_to_id")]
        public int AssignedToId { get; set; }
        [XmlElement("parent_issue_id")]
        public int ParentIssueId { get; set; }
        [XmlElement("custom_fields")]
        public string CustomFields { get; set; }
        [XmlElement("watcher_user_ids")]
        public int WatcherUserIds { get; set; }
        [XmlElement("is_private")]
        public bool IsPrivate { get; set; }
        [XmlElement("estimated_hours")]
        public string EstimatedHours { get; set; }

        public string ToXml()
        {
            string xml;
            try
            {
                var s = new XmlSerializer(typeof(IssueClass));
                var strBuilder = new StringBuilder();
                using (var strWriter = new StringWriter(strBuilder))
                {
                    s.Serialize(strWriter, this);
                }
                xml = strBuilder.ToString();
            }
            catch (Exception ex)
            {
                //реализация логера и запись в логер
                throw;
            }
            return xml;
        }

        public static IssueClass FromXml(string xml)
        {
            if (string.IsNullOrEmpty(xml))
                return null;
            IssueClass issue;
            try
            {
                var s = new XmlSerializer(typeof(IssueClass));
                using (var strReader = new StringReader(xml))
                {
                    issue = (IssueClass)s.Deserialize(strReader);
                }
            }
            catch (Exception ex)
            {
                //реализация логера и запись в логер
                throw;
            }
            return issue;
        }
    }
}
