using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace RedmineTasker.DAL.RedmineModels
{
    /// <summary>
    /// класс для преобразования участников проекта в класс при помощи LinqToXml
    /// </summary>
    public class Members
    {
        private string _userId;
        private string _id;
        private string _roleId;
        private string _roleName;

        public Members(string userName, string userId, string id, string roleId, string roleName)
        {
            this.UserName = userName;
            this._userId = userId;
            this._id = id;
            this._roleId = roleId;
            this._roleName = roleName;
        }


        public void SetMember(string userName, string userId, string id, string roleId, string roleName)
        {
            this.UserName = userName;
            this._userId = userId;
            this._id = id;
            this._roleId = roleId;
            this._roleName = roleName;
        }

        public string UserName { get; private set; }

        public string GetName()
        {
            return UserName;
        }

        public string GetId()
        {
            return _id;
        }
        public static List<Members> GetMemberShipList(string projectMembershipXml)
        {
            var memberList = new List<Members>();
            var membershipXml = XDocument.Parse(projectMembershipXml);
            try
            {
                foreach (var temp in membershipXml.Root.Elements("membership"))
                {
                    try
                    {
                        memberList.Add(new Members(
                            temp.Element("user").Attribute("name").Value,
                            temp.Element("user").Attribute("id").Value,
                            temp.Element("id").Value,
                            temp.Element("roles").Element("role").Attribute("id").Value,
                            temp.Element("roles").Element("role").Attribute("name").Value));
                    }
                    catch
                    {
                        memberList.Add(new Members(
                            temp.Element("group").Attribute("name").Value,
                            temp.Element("group").Attribute("id").Value,
                            temp.Element("id").Value,
                            temp.Element("roles").Element("role").Attribute("id").Value,
                            temp.Element("roles").Element("role").Attribute("name").Value));
                    }
                }
            }
            catch (Exception e)
            {
                //реализация логера
                throw;
            }
            return memberList;
        }
    }
}
