using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace RedmineTasker.DAL.RedmineModels
{
    /// <summary>
    /// класс для серилизации статусов тасков
    /// </summary>
    public class IssueStatus
    {
        [XmlElement("id")]
        public int Id { get; set; }
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("is_closed")]
        public bool IsClosed { get; set; }        
    }

    [Serializable]
    [XmlRoot("issue_statuses")]
    public class IssueStatuses 
    {
        [XmlElement("issue_status")]
        public List<IssueStatus> TaskStatus = new List<IssueStatus>();

        public string ToXml()
        {
            string xml;
            try
            {
                var s = new XmlSerializer(typeof(IssueStatuses));
                var strBuilder = new StringBuilder();
                using (var strWriter = new StringWriter(strBuilder))
                {
                    s.Serialize(strWriter, this);
                }
                xml = strBuilder.ToString();
            }
            catch (Exception ex)
            {
                //реализация логера
                throw;
            }
            return xml;
        }

        public static IssueStatuses FromXml(string xml)
        {
            if (string.IsNullOrEmpty(xml))
                return null;
            var issueStatuses = new IssueStatuses();
            try
            {
                var s = new XmlSerializer(typeof(IssueStatuses));
                using (var strReader = new StringReader(xml))
                {
                    issueStatuses = (IssueStatuses)s.Deserialize(strReader);
                }
            }
            catch (Exception ex)
            {
                //реализация логера
            }
            return issueStatuses;
        }
    }
}
