using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace RedmineTasker.DAL.RedmineModels
{
    /// <summary>
    /// класс для серилизации трекера таска
    /// </summary>
    public class Tracker
    {
        [XmlElement("id")]
        public int Id { get; set; }
        [XmlElement("name")]
        public string Name { get; set; }
    }

    [Serializable]
    [XmlRoot("trackers")]
    public class Trackers
    {
        [XmlElement("tracker")] public List<Tracker> IssueTrackers = new List<Tracker>();

        public string ToXml()
        {
            string xml;
            try
            {
                var s = new XmlSerializer(typeof (Trackers));
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

        public static Trackers FromXml(string xml)
        {
            if (string.IsNullOrEmpty(xml))
                return null;
            var trackers = new Trackers();
            try
            {
                var s = new XmlSerializer(typeof (Trackers));
                using (var strReader = new StringReader(xml))
                {
                    trackers = (Trackers) s.Deserialize(strReader);
                }
            }
            catch (Exception ex)
            {
                //реализация логера
                throw;
            }
            return trackers;
        }
    }

}
