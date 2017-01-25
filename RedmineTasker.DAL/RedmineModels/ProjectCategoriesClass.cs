using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace RedmineTasker.DAL.RedmineModels
{
    /// <summary>
    ////класс для серилизации категории таска
    /// </summary>
    public class IssueCategory
    {
        [XmlElement("id")]
        public int Id { get; set; }
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("project")]
        public ProjectName ProjectName;
    }

    [Serializable]
    [XmlRoot("issue_categories")]
    public class IssueCategories 
    {
        [XmlElement("issue_category")]
        public List<IssueCategory> TaskCategory = new List<IssueCategory>();

        public string ToXml()
        {
            string xml;
            try
            {
                var s = new XmlSerializer(typeof(IssueCategories));
                //XmlSerializer.FromTypes(new[] { typeof(AllSettingsApplication) })[0];
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

        public static IssueCategories FromXml(string xml)
        {
            if (string.IsNullOrEmpty(xml))
                return null;
            IssueCategories issueCategories;
            try
            {
                var s = new XmlSerializer(typeof(IssueCategories));
                using (var strReader = new StringReader(xml))
                {
                    issueCategories = (IssueCategories)s.Deserialize(strReader);
                }
            }
            catch (Exception ex)
            {
                //реализация логера
                throw;
            }
            return issueCategories;
        }
    }
}
