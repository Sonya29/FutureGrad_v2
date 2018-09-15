using MySql.Data.MySqlClient;
using Smart.Models.Implementations;
using SmartSch.Models;
using SmartSchool.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace SmartSchool.Controllers
{
    public class StudentController : Controller
    {
        public StudentData data = new StudentData();
        public List<SimpleModel> studentHomeData;
        public List<Submission> submissionViewList;
        public ProgressTracker progressTrackerView;
        public List<SimpleModel> threadsView;
        public List<SimpleModel> otherThreadsView;
        public List<Thread> threadView;

        // GET: Student
        public ActionResult Index()
        {
            if(studentHomeData == null)
            {
                studentHomeData = data.getModules_SimplifiedData(Session["userId"].ToString());
            }
            return View(this.studentHomeData);
        }
        public ActionResult Module()
        {
            if (Request.Form["btnModule"] != null && Request.Form["btnModule"].ToString() != "")
            {
                try { Session["cmyId"] = Request.Form["btnModule"]; } catch (Exception ex) { }
            }
            if(submissionViewList == null && Session["userId"] != null)
            {
                submissionViewList = data.getAssignment_ViewData(Session["userId"].ToString(), Session["cmyId"].ToString());
            }
            if(progressTrackerView == null && Session["userId"] != null)
            {
                progressTrackerView = data.getProgressTracker((String)Session["userId"], (String)Session["cmyId"]);
            }
            if(threadsView ==null && Session["userId"] != null && Session["cmyId"] != null)
            {
                threadsView = data.getThreads((String)Session["userId"], (String)Session["cmyId"]);
            }
            if(otherThreadsView == null && Session["userId"] != null && Session["cmyId"] != null)
            {
                otherThreadsView = data.getOtherThreads((String)Session["userId"], (String)Session["cmyId"]);
            }
            
            Module smvm = new Module(submissionViewList, progressTrackerView, threadsView, otherThreadsView);
            if(Request.Form["moduleName"] != null)
            {
                Session["moduleName"] = Request.Form["moduleName"].ToString();
            }
            smvm.moduleName = Session["moduleName"].ToString();
            return View(smvm);
        }
        [HttpPost]
        public ActionResult uploadSubmission(HttpPostedFileBase btnFile)
        {
            var filepath = Path.GetFileName(btnFile.FileName);
            var orgFilepath = filepath;
            //Give uniqueFileName
            try
            {
                DateTime dt = DateTime.Now;
                var assignmentId = Request.Form["btnSubmitAssignment"];
                filepath = assignmentId + "_" + (String)(Session["userId"]) + "_" + dt.ToLongDateString() + ".docx";
                var path = Path.Combine(Server.MapPath("~/Content/Files/Submissions/"), filepath);
                btnFile.SaveAs(path);
                data.makeSubmission(orgFilepath, filepath, assignmentId, Session["userId"].ToString());
            }
            catch (Exception e)
            {
            }
            return RedirectToAction("Module");
        }
        public ActionResult startThread()
        {
            String newThread = Request.Form["newThread"].ToString();
            newThread.Replace("<", "&lt");
            newThread.Replace(">", "&gt");
            data.addThread(newThread, (String) Session["userId"], (String) Session["cmyId"]);
            return RedirectToAction("Module");
        }
        public ActionResult Thread()
        {
            if (Request.QueryString["btn"] != null && Request.QueryString["btn"].ToString() != "")
            {
                try { Session["threadId"] = Request.QueryString["btn"]; } catch (Exception ex) { }
            }
            threadView = data.getThread_ViewData(Convert.ToInt32(Session["threadId"]));
            return View(threadView);
        }
        public ActionResult SubmitPost()
        {
            String comment = Request.Form["comment"];
            comment.Replace("<", "&lt");
            comment.Replace(">", "&gt");
            data.addPost(comment, (String) Session["userId"], Convert.ToInt32(Session["threadId"]));
            return RedirectToAction("Thread");
        }
        public ActionResult createTeam()
        {
            String name = Request.Form["teamName"];
            String userId = (String)Session["userId"];
            String assignmentId = Request.Form["assignmentId"];
            data.addTeam(Session["userId"].ToString(), name, Convert.ToInt32(assignmentId));
            return RedirectToAction("StudentModuleView");
        }
        public ActionResult addMemberToTeam()
        {
            String studentId = Request.Form["studentId"];
            int assignmentId = Convert.ToInt32(Request.Form["assignmentId"]);
            int teamId = data.getTeamId((String)Session["userId"], assignmentId);
            Database database = new Database();
            database.OpenConnection();
            MySqlCommand cmd = new MySqlCommand("Insert Into StudentTeam Values(null, @val1, @val2)");
            cmd.Connection = database.connection;
            cmd.Parameters.AddWithValue("@val1", studentId);
            cmd.Parameters.AddWithValue("@val2", teamId);
            cmd.ExecuteNonQuery();
            return RedirectToAction("StudentModuleView");
        }
 
    }
}