using MySql.Data.MySqlClient;
using SmartSch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Xml.Linq;

namespace Smart.Models
{
    public class DocumentViewer
    {
        public XElement document { get; set; }
        public String coordinates { get; set; }
        public String annotationStatus { get; set; }

        public DocumentViewer(XElement docAselement, String coordinates, String userId)
        {
            this.document = docAselement;
            this.coordinates = coordinates;
            this.annotationStatus = getAnnotationStatus(userId);
        }

        public String getAnnotationStatus(String userId)
        {
            Database database = new Database();
            database.OpenConnection();
            MySqlCommand cmd = new MySqlCommand("SELECT userType FROM User WHERE userId = @val1");
            cmd.Connection = database.connection;
            cmd.Parameters.AddWithValue("@val1", userId);
            MySqlDataReader userTypes = cmd.ExecuteReader();
            String userType = "";
            while (userTypes.Read())
            {
                userType = userTypes["userType"].ToString();
            }
            String result = "";
            if(userType.ToUpper() != "LECTURER")
            {
                result = "disabled";
            }
            return result;
        }
    }
}