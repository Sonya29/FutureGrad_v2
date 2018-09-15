using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Web.Mvc;
using Smart.Models;
using SmartSch.Models;
using MySql.Data.MySqlClient;

namespace Smart.Controllers
{
    public class Docx2HtmlController : Controller
    {
        // GET: Docx2Html
        public ActionResult Index()
        {
            if(Request.Form["btnSubmission"] != null)
            {
                Session["path"] = Request.Form["btnSubmission"];
            }
            XElement w2h = Word2Html.convert("C:\\Users\\Sonya.SHAJI-DOMAIN\\Documents\\GitHub\\FutureGrad\\Smart\\Content\\Files\\Submissions\\" + (String) Session["path"]);
            List<List<String>> coordinates = new List<List<string>>();
            List<String> c = new List<string>();
            c.Add(".");
            c.Add(".");
            c.Add(".");
            coordinates.Add(c);
            Database database = new Database();
            database.OpenConnection();
            String code = "";
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM AssignmentStudent WHERE filepath = @val1");
            cmd.Connection = database.connection;
            cmd.Parameters.AddWithValue("@val1", (String)Session["path"]);
            MySqlDataReader results = cmd.ExecuteReader();
            while (results.Read())
            {
                code = results["annotations"].ToString();
            }
            DocumentViewer dv = new DocumentViewer(w2h, code, (String) Session["userId"]);
            return View(dv);
        }

        public ActionResult saveAnnotations()
        {
            Database database = new Database();
            database.OpenConnection();
            MySqlCommand cmd = new MySqlCommand("UPDATE AssignmentStudent SET annotations = @val1 where filepath = @val2");
            cmd.Connection = database.connection;
            cmd.Parameters.AddWithValue("@val1", Request.Form["btnSave"]);
            cmd.Parameters.AddWithValue("@val2", (String)Session["path"]);
            cmd.ExecuteNonQuery();
            return RedirectToAction("Index");
        }

    }
}