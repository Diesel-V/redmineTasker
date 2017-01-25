using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace RedmineTasker.DAL.RedmineModels
{
    /// <summary>
    /// реализация класса для серилизации проектов редмайна
    /// </summary>
    public class Project
    {
        [XmlElement("id")]
        public int Id { get; set; }
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("identifier")]
        public string Identifier { get; set; }
        [XmlElement("description")]
        public string Description { get; set; }
        [XmlElement("status")]
        public string Status { get; set; }
        [XmlElement("is_public")]
        public string IsPublic { get; set; }
        [XmlElement("created_on")]
        public string CreatedOn { get; set; }
        [XmlElement("updated_on")]
        public string UpdatedOn { get; set; }
    }

    [Serializable]
    [XmlRoot("projects")]
    public class Projects
    {
        [XmlElement("project")]
        public List<Project> RedmineProjects = new List<Project>();

        public string ToXml()
        {
            string xml;
            try
            {
                var s = new XmlSerializer(typeof(Projects));
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

        public static Projects FromXml(string xml)
        {
            if (string.IsNullOrEmpty(xml))
                return null;
            Projects projects;
            try
            {
                var s = new XmlSerializer(typeof(Projects));
                using (var strReader = new StringReader(xml))
                {
                    projects = (Projects)s.Deserialize(strReader);
                }
            }
            catch (Exception ex)
            {
                //реализация логера
                throw;
            }
            return projects;
        }
    }
}
