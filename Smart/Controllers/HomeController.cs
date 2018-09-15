using MySql.Data.MySqlClient;
using SmartSch.Models;
using SmartSchool.Controllers;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace Smart.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login()
        {
            String sql = "SELECT UserId, password, userType FROM User WHERE userID = @val1 AND password = @val2";
            Database database = new Database();
            database.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql);
            cmd.Connection = database.connection;
            cmd.Parameters.AddWithValue("@val1", Request.Form["txtUsername"]);
            cmd.Parameters.AddWithValue("@val2", MD5Hash(Request.Form["txtPassword"]));
            MySqlDataReader results = cmd.ExecuteReader();
            String user = "Home";
            while (results.Read())
            {
                Session["userId"] = results["userId"].ToString();
                if (results["userType"].ToString() == "Student")
                {
                    user = "Student";
                }
                else if (results["userType"].ToString() == "Lecturer")
                {
                    user = "Lecturer";
                }
            }

            database.CloseConnection();
            return RedirectToAction("Index",user);
        }

        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }
   
}