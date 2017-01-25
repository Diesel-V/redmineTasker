using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace RedmineTasker.DAL.RedmineModels
{
    /// <summary>
    /// класс для серилизации приоритета тасков 
    /// </summary>
    public class IssuePriority
    {
        [XmlElement("id")]
        public int Id { get; set; }
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("is_default")]
        public bool IsDefault { get; set; }
    }

    [Serializable]
    [XmlRoot("issue_priorities")]
    public class IssuePriorities 
    {
        [XmlElement("issue_priority")]
        public List<IssuePriority> TaskPriority = new List<IssuePriority>();

        public string ToXml()
        {
            string xml;
            try
            {
                var s = new XmlSerializer(typeof(IssuePriorities));
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

        public static IssuePriorities FromXml(string xml)
        {
            if (string.IsNullOrEmpty(xml))
                return null;
            var issuePriority = new IssuePriorities();
            try
            {
                var s = new XmlSerializer(typeof(IssuePriorities));
                using (var strReader = new StringReader(xml))
                {
                    issuePriority = (IssuePriorities)s.Deserialize(strReader);
                }
            }
            catch (Exception ex)
            {
                //реализация логера и запись в логер
                throw;
            }
            return issuePriority;
        }
    }
}
