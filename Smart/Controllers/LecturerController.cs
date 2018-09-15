using MySql.Data.MySqlClient;
using Smart.Models;
using Smart.Models.Implementations;
using SmartSch.Models;
using SmartSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Smart.Controllers
{
    public class LecturerController : Controller
    {
        public LecturerData data = new LecturerData();
        public List<SimpleModel> lecturerHomeData;
        public List<Assignment> assignmentsData;
        public List<AssignmentSubmissions> lecturerClasses;

        // GET: Lecturer
        public ActionResult Index()
        {
            Session["userId"] = "KU123456";
            if (lecturerHomeData == null)
            {
                lecturerHomeData = data.getCohorts_SimplifiedData((String)Session["userId"]);
            }
            return View(lecturerHomeData);
        }
        public ActionResult AssignmentListForCohort()
        {
            String cmy = Request.Form["btnCohort"].ToString();
            if (assignmentsData == null)
            {
                assignmentsData = data.getAssignments_ViewData(cmy);
            }
            return View(assignmentsData);
        }
        public ActionResult LecturerClassViewModel()
        {
            if(lecturerClasses == null)
            {
                lecturerClasses = data.getAssignmentsIncludingSubmissions(Request.Form["btnCohort"]);
            }
            return View(lecturerClasses);
        }
        [HttpPost]
        public ActionResult createAssignment(String[] cmy)
        {
            String title = Request.Form["title"];
            DateTime deadline = Convert.ToDateTime(Request.Form["deadline"]);
            double moduleWeight = Convert.ToDouble(Request.Form["moduleWeight"]);
            String submissionFormat = Request.Form["submissionFormat"];
            data.addAssignmentToDatabase(cmy, title, moduleWeight, deadline, (String)Session["userId"], submissionFormat);
            return RedirectToAction("Index");
        }
    }
}