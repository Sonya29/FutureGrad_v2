using MySql.Data.MySqlClient;
using SmartSch.Models;
using SmartSchool.Controllers;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;

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
            String sql = "SELECT UserId, password, userType FROM User WHERE userID = @val1 AND password = MD5(@val2)";
            Database database = new Database();
            database.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql);
            cmd.Connection = database.connection;
            cmd.Parameters.AddWithValue("@val1", Request.Form["txtUsername"]);
            cmd.Parameters.AddWithValue("@val2", Request.Form["txtPassword"]);
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

    }
   
}