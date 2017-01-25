using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace RedmineTasker.DAL.RedmineModels
{
    public class Version
    {
        [XmlElement("id")]
        public int Id { get; set; }
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("project")] 
        public ProjectName ProjectName;
    }

    public class ProjectName
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("id")]
        public string Id { get; set; }
    }

    [Serializable]
    [XmlRoot("versions")]
    public class Versions 
    {
        [XmlElement("version")]
        public List<Version> ProjectVersions = new List<Version>();

        public string ToXml()
        {
            string xml;
            try
            {
                var s = new XmlSerializer(typeof(Versions));
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

        public static Versions FromXml(string xml)
        {
            if (string.IsNullOrEmpty(xml))
                return null;
            Versions versions;
            try
            {
                var s = new XmlSerializer(typeof(Versions));
                using (var strReader = new StringReader(xml))
                {
                    versions = (Versions)s.Deserialize(strReader);
                }
            }
            catch (Exception ex)
            {
                //реализация логера
                throw;
            }
            return versions;
        }
    }
}
