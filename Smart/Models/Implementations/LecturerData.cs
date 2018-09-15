using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartSchool.Models;
using SmartSch.Models;
using MySql.Data.MySqlClient;

namespace Smart.Models.Implementations
{
    public class LecturerData
    {
        public List<SimpleModel> getCohorts_SimplifiedData(String userId)
        {
            String sql = "SELECT coursemodule_year.courseModule_YearId, course.courseTitle,Module.moduleName, coursemodule_year.year, CONCAT(uSER.FIRSTNAME, ' ', uSER.LASTNAME) AS lecturer, AVG(ifnull(coursemoduleyear_student.mark,0)) AS avgMark FROM Coursemodule_year INNER JOIN coursemodule ON coursemodule.courseModuleId = coursemodule_year.courseModuleId inner join coursemoduleyear_student on CourseModuleYearId = coursemodule_year.courseModule_YearId inner join course on course.courseId = coursemodule.courseId inner join module on module.moduleId = coursemodule.moduleId inner join lecturerModule on lecturermodule.moduleId = module.moduleId inner join User on user.userId = lecturermodule.lecturerId AND lecturermodule.lecturerId = @val1 Group By coursemodule_year.courseModule_YearId";
            Database database = new Database();
            database.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql);
            cmd.Connection = database.connection;
            cmd.Parameters.AddWithValue("@val1", userId);
            MySqlDataReader results = cmd.ExecuteReader();
            List<SimpleModel> lecturerHomeData = new List<SimpleModel>();
            while (results.Read())
            {
                String year2 = (Convert.ToUInt32(results["year"]) + 1).ToString().Substring(results["year"].ToString().Length - 2, 2);
                String label = results["courseTitle"].ToString() + " " + results["moduleName"] + " " + results["year"].ToString() + "/" + year2;
                SimpleModel m = new SimpleModel(results["courseModule_YearId"].ToString(), label);
                lecturerHomeData.Add(m);
            }
            return lecturerHomeData;
        }
        public List<Assignment> getAssignments_ViewData(String courseModuleYearId)
        {
            Database database = new Database();
            database.OpenConnection();
            MySqlCommand cmd = new MySqlCommand("SELECT Assignment.assignmentId, Assignment.assignmentTitle, Assignment.courseModuleYearId, Assignment.deadline, Assignment.filename, Assignment.filepath, Assignment.moduleWeight, Assignment.setDate, Assignment.submissionFormat, concat(User.firstname, ' ', User.lastname) as lecturer FROM Assignment INNER JOIN User oN Assignment.userId = User.userId AND Assignment.courseModuleYearId = @val1");
            cmd.Connection = database.connection;
            cmd.Parameters.AddWithValue("@val1", courseModuleYearId);
            MySqlDataReader results = cmd.ExecuteReader();
            List<Assignment> assignmentsData = new List<Assignment>();
            while (results.Read())
            {
                Assignment a = new Assignment(
                    Convert.ToInt32(results["assignmentId"]),
                    results["assignmentTitle"].ToString(),
                    Convert.ToDateTime(results["deadline"]),
                    results["filename"].ToString(),
                    results["filepath"].ToString(),
                    Convert.ToDouble(results["moduleWeight"]),
                    Convert.ToDateTime(results["setDate"]),
                    results["submissionFormat"].ToString(),
                    results["lecturer"].ToString()
                );
            }
            return assignmentsData;
        }
        public List<AssignmentSubmissions> getAssignmentsIncludingSubmissions(String courseModuleYearId)
        {
            Database database = new Database();
            database.OpenConnection();
            MySqlCommand cmd = new MySqlCommand("SELECT assignment.assignmentId, assignmentStudent.assignmentStudentId, Assignment.assignmentTitle, assignment.moduleWeight, assignment.filepath As assignmentFilepath, assignment.deadline, assignment.setDate, assignment.submissionFormat, assignmentstudent.filepath, assignmentStudent.mark From AssignmentStudent Inner Join assignment on assignment.assignmentId = AssignmentStudent.assignmentId Inner Join coursemodule_year on coursemodule_year.courseModule_YearId = assignment.courseModuleYearId AND coursemodule_year.courseModule_YearId = @val1 ORDER BY assignment.assignmentId asc");
            cmd.Connection = database.connection;
            cmd.Parameters.AddWithValue("@val1", courseModuleYearId);
            MySqlDataReader data = cmd.ExecuteReader();
            List<AssignmentSubmissions> result = getAssignmentIncludingSubmissions_AsObjects(data);
            return result;
        }
        private List<AssignmentSubmissions> getAssignmentIncludingSubmissions_AsObjects(MySqlDataReader data)
        {
            Assignment assignment = new Assignment();
            assignment.id = -1;
            List<Submission> submissions = new List<Submission>();
            List<AssignmentSubmissions> result = new List<AssignmentSubmissions>();
            while (data.Read())
            {
                //check if any assignment has been added and if so, has this particular assignment been added?
                if (assignment.id == -1 || assignment.id != Convert.ToInt32(data["assignmentId"]))
                {
                    if(assignment.id != -1)
                    {
                        result.Add(new AssignmentSubmissions(assignment, submissions));
                    }
                    assignment = new Assignment(Convert.ToInt32(data["assignmentId"]),data["assignmentTitle"].ToString(),Convert.ToDouble(data["moduleWeight"]),data["filepath"].ToString(),Convert.ToDateTime(data["deadline"]),Convert.ToDateTime(data["setDate"]),data["submissionFormat"].ToString());
                    submissions = new List<Submission>();
                    Submission submission = new Submission(Convert.ToInt32(data["assignmentStudentId"]), data["filepath"].ToString());
                    submissions.Add(submission);
                }
                else
                {
                    //if assignment has alreeady been added, add submission to this assignment
                    Submission submission = new Submission(Convert.ToInt32(data["assignmentStudentId"]), data["filepath"].ToString());
                    submissions.Add(submission);
                }
            }
            result.Add(new AssignmentSubmissions(assignment, submissions));
            return result;
        }
        public void addAssignmentToDatabase(String[] cmy, String title, double moduleWeight, DateTime deadline, String userId, String submissionFormat)
        {
            foreach (var i in cmy)
            {
                Database database = new Database();
                database.OpenConnection();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO Assignment VALUES(null, @val1, @val2,@val3,@val4,@val5,@val6,@val7,@val8,@val9)");
                cmd.Connection = database.connection;
                cmd.Parameters.AddWithValue("@val1", title);
                cmd.Parameters.AddWithValue("@val2", i);
                cmd.Parameters.AddWithValue("@val3", moduleWeight);
                cmd.Parameters.AddWithValue("@val4", deadline);
                cmd.Parameters.AddWithValue("@val5", null);
                cmd.Parameters.AddWithValue("@val6", null);
                cmd.Parameters.AddWithValue("@val7", userId);
                cmd.Parameters.AddWithValue("@val8", DateTime.Now);
                cmd.Parameters.AddWithValue("@val9", submissionFormat);
                cmd.ExecuteNonQuery();
                database.CloseConnection();
            }
        }
    }
}