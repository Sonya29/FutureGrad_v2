using MySql.Data.MySqlClient;
using SmartSch.Models;
using SmartSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smart.Models.Implementations
{
    public class StudentData
    {

        public List<SimpleModel> getModules_SimplifiedData(string userId)
        {
            String sql = "SELECT coursemodule_year.courseModule_YearId, module.moduleName From coursemodule inner join coursemodule_year on courseModule_Year.courseModuleId = coursemodule.courseModuleId inner join module on module.moduleId = coursemodule.moduleId inner join coursemoduleyear_student on coursemoduleyear_student.CourseModuleYearId = coursemodule_year.courseModule_YearId where coursemoduleyear_student.StudentId =@val1";
            Database database = new Database();
            database.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql);
            cmd.Connection = database.connection;
            cmd.Parameters.AddWithValue("@val1", userId);
            MySqlDataReader results = cmd.ExecuteReader();
            List<SimpleModel> studentHomeData = new List<SimpleModel>();
            while (results.Read())
            {
                SimpleModel data = new SimpleModel(results["courseModule_YearId"].ToString(), results["moduleName"].ToString());
                studentHomeData.Add(data);
            }
            database.CloseConnection();
            return studentHomeData;
        }
        public List<Submission> getAssignment_ViewData(String userId, String cmyId)
        {
            String sql_studentModuleView = "SELECT Assignment.submissionFormat,Assignment.filepath As assignmentLink,AssignmentStudent.assignmentStudentId, Assignment.assignmentId, Assignment.deadline, Assignment.assignmentTitle, Assignment.moduleWeight, CONCAT(User.firstname, ' ', User.lastname) as lecturer, AssignmentStudent.mark, AssignmentStudent.dateOfSubmission, AssignmentStudent.filepath, AssignmentStudent.filename FROM Assignment INNER JOIN User ON User.userId = Assignment.userId INNER JOIN assignmentstudent ON assignmentstudent.assignmentId = Assignment.assignmentId INNER JOIN Student ON Student.userId = AssignmentStudent.studentId AND Student.userId=@val1 where assignment.courseModuleYearId=@val2";
            Database database = new Database();
            database.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql_studentModuleView);
            cmd.Connection = database.connection;
            cmd.Parameters.AddWithValue("@val1", userId);
            cmd.Parameters.AddWithValue("@val2", cmyId);
            MySqlDataReader results_studentModule = cmd.ExecuteReader();
            List<Submission> smv = new List<Submission>();
            while (results_studentModule.Read())
            {
                Nullable<Double> mark = null;
                if (!Convert.IsDBNull(results_studentModule["mark"]))
                {
                    mark = Convert.ToDouble(results_studentModule["mark"]);
                }
                Nullable<DateTime> dateOfSubmission = null;
                if (!Convert.IsDBNull(results_studentModule["dateOfSubmission"]))
                {
                    dateOfSubmission = Convert.ToDateTime(results_studentModule["dateOfSubmission"]);
                }
                Submission smvModel = new Submission(
                        Convert.ToInt32(results_studentModule["assignmentStudentId"]),
                        Convert.ToInt32(results_studentModule["assignmentId"]),
                        results_studentModule["assignmentTitle"].ToString(),
                        Convert.ToDouble(results_studentModule["moduleWeight"]),
                        results_studentModule["lecturer"].ToString(),
                        mark,
                        dateOfSubmission,
                        results_studentModule["filepath"].ToString(),
                        results_studentModule["filename"].ToString(),
                        Convert.ToDateTime(results_studentModule["deadline"]),
                        results_studentModule["assignmentLink"].ToString(),
                        results_studentModule["submissionFormat"].ToString()
                );
                smv.Add(smvModel);
            }
            database.CloseConnection();
            return smv;
        }
        public ProgressTracker getProgressTracker(String userId, String cmyId)
        {
            String sql_progressTrackerView = "SELECT SUM((Assignment.moduleWeight/100)*IfNULL(AssignmentStudent.mark,0)) AS progress, SUM(Assignment.moduleWeight) AS maxMark FROM assignmentstudent INNER JOIN student ON student.userId = assignmentstudent.studentId AND assignmentstudent.studentId=@val1 INNER JOIN assignment ON assignment.assignmentId = assignmentstudent.assignmentId GROUP BY assignment.courseModuleYearId HAVING assignment.courseModuleYearId=@val2";
            Database database = new Database();
            database.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql_progressTrackerView.Replace("\u0001",""));
            cmd.Connection = database.connection;
            cmd.Parameters.AddWithValue("@val1", userId);
            cmd.Parameters.AddWithValue("@val2", cmyId);
            MySqlDataReader results_progressTracker = cmd.ExecuteReader();
            ProgressTracker ptv = null;
            while (results_progressTracker.Read())
            {
                    ptv = new ProgressTracker(
                        Convert.ToDouble(results_progressTracker["progress"]),
                        Convert.ToDouble(results_progressTracker["maxMark"])
                    );
            }
            database.CloseConnection();
            return ptv;
        }
        public List<SimpleModel> getThreads(String userId, String cmyId)
        {
            String sql = "SELECT thread.threadId, thread.title From thread WHERE thread.userId = @val1 AND thread.courseModuleYearId = @val2";
            Database database = new Database();
            database.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql);
            cmd.Connection = database.connection;
            cmd.Parameters.AddWithValue("@val1", userId);
            cmd.Parameters.AddWithValue("@val2", cmyId);
            MySqlDataReader results_threads = cmd.ExecuteReader();
            List<SimpleModel> threads = new List<SimpleModel>();
            while (results_threads.Read())
            {
                SimpleModel thread = new SimpleModel(Convert.ToInt32(results_threads["threadId"]), results_threads["title"].ToString());
                threads.Add(thread);
            }
            return threads;
        }
        public List<SimpleModel> getOtherThreads(String userId, String cmyId)
        {
            String sql = "SELECT thread.threadId, thread.title From thread WHERE thread.userId <> @val1 AND thread.courseModuleYearId = @val2";
            Database database = new Database();
            database.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql);
            cmd.Connection = database.connection;
            cmd.Parameters.AddWithValue("@val1", userId);
            cmd.Parameters.AddWithValue("@val2", cmyId);
            MySqlDataReader results_threads = cmd.ExecuteReader();
            List<SimpleModel> threads = new List<SimpleModel>();
            while (results_threads.Read())
            {
                SimpleModel thread = new SimpleModel(Convert.ToInt32(results_threads["threadId"]), results_threads["title"].ToString());
                threads.Add(thread);
            }
            return threads;
        }
        public void makeSubmission(String filename, String filepath, String assignmentId, String studentId)
        {
            Database database = new Database();
            database.OpenConnection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = database.connection;
            cmd.CommandText = "UPDATE AssignmentStudent SET filepath = @val1, filename = @val2, dateOfSubmission = @val3 WHERE assignmentId = @val4 AND studentId = @val5";
            cmd.Parameters.AddWithValue("@val1", filepath);
            cmd.Parameters.AddWithValue("@val2", filename);
            cmd.Parameters.AddWithValue("@val3", DateTime.Now);
            cmd.Parameters.AddWithValue("@val4", Convert.ToInt32(assignmentId));
            cmd.Parameters.AddWithValue("@val5", studentId);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            database.CloseConnection();
        }
        public void addThread(String newThread, String userId, String courseModuleYearId)
        {
            Database database = new Database();
            database.OpenConnection();
            MySqlCommand cmd = new MySqlCommand("INSERT INTO Thread VALUES(@val1, @val2, @val3, @val4)");
            cmd.Connection = database.connection;
            cmd.Parameters.AddWithValue("@val1", null);
            cmd.Parameters.AddWithValue("@val2", newThread);
            cmd.Parameters.AddWithValue("@val3", userId);
            cmd.Parameters.AddWithValue("@val4", courseModuleYearId);
            cmd.ExecuteNonQuery();
        }
        public List<Thread> getThread_ViewData(int threadId)
        {
            List<Thread> threadViewModels = new List<Thread>();
            threadViewModels = getThreadsAndComments(threadViewModels, threadId);
            if (threadViewModels.Count == 0)
            {
                threadViewModels = getThreadDetails(threadViewModels, threadId);
            }
            return threadViewModels;
        }
        private List<Thread> getThreadDetails(List<Thread> threadViewModels, int threadId)
        {
            Database database = new Database();
            database.OpenConnection();
            MySqlCommand cmd = new MySqlCommand("SELECT Thread.title, Concat(tUser.firstname, ' ', tUser.lastname) As threadOwner, Thread.courseModuleYearId From Thread Inner Join User tUser On tUser.userId = Thread.userId and Thread.threadId = @val2");
            cmd.Connection = database.connection;
            cmd.Parameters.AddWithValue("@val2", threadId);
            MySqlDataReader result = cmd.ExecuteReader();
            while (result.Read())
            {
                Thread tvm = new Thread(
                    result["title"].ToString(),
                    result["threadOwner"].ToString(),
                    result["courseModuleYearId"].ToString()
                 );
                threadViewModels.Add(tvm);
            }
            database.CloseConnection();
            return threadViewModels;
        }
        private List<Thread> getThreadsAndComments(List<Thread> threadViewModels, int threadId)
        {
            Database database = new Database();
            database.OpenConnection();
            MySqlCommand cmd = new MySqlCommand("SELECT Thread.title, Concat(tUser.firstname, ' ', tUser.lastname) As threadOwner, Thread.courseModuleYearId, Concat(pUser.firstname, ' ', pUser.lastname) As postOwner, post.message From Thread Inner Join Post On Post.threadId = Thread.threadId Inner Join User tUser On tUser.userId = Thread.userId Inner Join User pUser ON pUser.userId = Post.userId AND Thread.threadId = @val1");
            cmd.Connection = database.connection;
            cmd.Parameters.AddWithValue("@val1", threadId);
            MySqlDataReader threadViews = cmd.ExecuteReader();
            while (threadViews.Read())
            {
                Thread tvm = new Thread(
                     threadViews["title"].ToString(),
                     threadViews["threadOwner"].ToString(),
                     threadViews["courseModuleYearId"].ToString(),
                     threadViews["postOwner"].ToString(),
                     threadViews["message"].ToString()
                 );
                threadViewModels.Add(tvm);
            }
            database.CloseConnection();
            return threadViewModels;
        }
        public void addPost(String comment, String userId, int threadId)
        {
            Database database = new Database();
            database.OpenConnection();
            String sql = "Insert Into Post Values(@val1, @val2, @val3, @val4)";
            MySqlCommand cmd = new MySqlCommand(sql);
            cmd.Connection = database.connection;
            cmd.Parameters.AddWithValue("@val1", null);
            cmd.Parameters.AddWithValue("@val2", comment);
            cmd.Parameters.AddWithValue("@val3", userId);
            cmd.Parameters.AddWithValue("@val4", threadId);
            cmd.ExecuteNonQuery();
        }
        public void addTeam(String userId, String name, int assignmentId)
        {
            Database database = new Database();
            database.OpenConnection();
            MySqlCommand cmd = new MySqlCommand("Insert Into Team VALUES(null, @val1, @val2, @val3)");
            cmd.Connection = database.connection;
            cmd.Parameters.AddWithValue("@val1", userId);
            cmd.Parameters.AddWithValue("@val2", name);
            cmd.Parameters.AddWithValue("@val3", assignmentId);
            cmd.ExecuteNonQuery();
        }
        public int getTeamId(String leaderId, int assignmentId)
        {
            int x = 0;
            Database database = new Database();
            database.OpenConnection();
            MySqlCommand cmd = new MySqlCommand("SELECT team.teamId from Team inner join studentteam on studentTeam.teamId = team.teamId and studentteam.studentId = @val1 where team.assignmentId = @val2");
            cmd.Connection = database.connection;
            cmd.Parameters.AddWithValue("@val1", leaderId);
            cmd.Parameters.AddWithValue("@val2", assignmentId);
            MySqlDataReader results = cmd.ExecuteReader();
            while (results.Read())
            {
                x = Convert.ToInt32(results["teamId"]);
            }
            return x;
        }
    }
}