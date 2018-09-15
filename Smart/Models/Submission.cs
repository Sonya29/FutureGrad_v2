using MySql.Data.MySqlClient;
using SmartSch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartSchool.Models
{
    public class Submission
    {
        public int assignmentStudentId { get; set; }
        public int assignmentId { get; set; }
        public Nullable<double> mark { get; set; }
        public String filepath { get; set; }
        public String filename { get; set; }
        public DateTime deadline { get; set; }
        public Nullable<DateTime> dateOfSubmission { get; set; }
        public String submissionFormat { get; set; }
        public DateTime setDate { get; set; }
        public String studentId { get; set; }
        public String moduleName { get; set; }
        public int year { get; set; }
        public String assignmentTitle { get; set; }
        public double moduleWeight { get; set; }
        public String lecturerName { get; set; }
        public String assignmentLink { get; set; }
        public int extraTime { get; set; }
       
        public Submission()
        {

        }

        public Submission(int assignmentStudentId, int assignmentId, String moduleName,int year, String assignmentTitle, double moduleWeight, String lecturerName, Nullable<Double> mark, Nullable<DateTime> dateOfSubmission, String filepath, String filename, DateTime deadline, String assignmentLink, String submissionFormat){
            this.assignmentStudentId = assignmentStudentId;
            this.assignmentId = assignmentId;
            this.moduleName = moduleName;
            this.year = year;
            this.assignmentTitle = assignmentTitle;
            this.moduleWeight = moduleWeight;
            this.lecturerName = lecturerName;
            this.mark = mark;
            this.dateOfSubmission = dateOfSubmission;
            this.filepath = filepath;
            this.filename = filename;
            this.deadline = deadline;
            this.assignmentLink = assignmentLink;
            this.submissionFormat = submissionFormat;
        }

        public Submission(int assignmentStudentId, int assignmentId, String assignmentTitle, double moduleWeight, String lecturerName, Nullable<Double> mark, Nullable<DateTime> dateOfSubmission, String filepath, String filename, DateTime deadline, String assignmentLink, String submissionFormat)
        {
            this.assignmentStudentId = assignmentStudentId;
            this.assignmentId = assignmentId;
            this.moduleName = null;
            this.year = 0;
            this.assignmentTitle = assignmentTitle;
            this.moduleWeight = moduleWeight;
            this.lecturerName = lecturerName;
            this.mark = mark;
            this.dateOfSubmission = dateOfSubmission;
            this.filepath = filepath;
            this.filename = filename;
            this.deadline = deadline;
            this.assignmentLink = assignmentLink;
            this.submissionFormat = submissionFormat;
        }

        public Submission(int assignmentStudentId, int assignmentId, double mark, String filepath, String filename, DateTime deadline, Nullable<DateTime> dateOfSubmission, String submissionFormat, DateTime setDate, String studentId)
        {
            this.assignmentStudentId = assignmentStudentId;
            this.assignmentId = assignmentId;
            this.mark = mark;
            this.filepath = filepath;
            this.filename = filename;
            this.deadline = deadline;
            this.dateOfSubmission = dateOfSubmission;
            this.submissionFormat = submissionFormat;
            this.setDate = setDate;
            this.studentId = studentId;
        }

        public Submission(int id, String filepath, double mark)
        {
            this.assignmentStudentId = id;
            this.filepath = filepath;
            this.mark = mark;
        }

        public Submission(int id, String filepath)
        {
            this.assignmentStudentId = id;
            this.filepath = filepath;
        }

        public String colourSubmission()
        {
		    String status = "success";
            if (this.mark == null)
            {
			    status = "danger";
            }
            return status;
        }

        public Double capMark(double mark)
        {
            if (this.submissionFormat == "Group"){
                if (this.dateOfSubmission > this.deadline){
				    mark = mark * 0.4;
                }
			    this.assignMarksToGroupMembers(mark);
            }
		    else if (this.submissionFormat == "Single"){
                if (this.dateOfSubmission > this.getDeadlineForStudent()){
				    mark = mark * 0.4;
                }
                this.setMark(mark);
            }
            return mark;
        }

        public void setMark(double mark)
        {
            Database database = new Database();
            database.OpenConnection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = database.connection;
            cmd.CommandText = "UPDATE `db624820831`.`AssignmentStudent` SET `mark` = @val1 WHERE `AssignmentStudent`.`assignmentStudentId` = @val2";
            cmd.Parameters.AddWithValue("@val1", mark);
            cmd.Parameters.AddWithValue("@val2", this.assignmentStudentId);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            database.CloseConnection();
        }

        public DateTime getDeadlineForStudent()
        {
			DateTime deadline = Convert.ToDateTime(this.deadline);
			int days = (int) (deadline - Convert.ToDateTime(this.setDate)).TotalDays;
			int extraTime = this.getExtraTimeFromDB();
            if ((extraTime.ToString()) != null)
            {
                int extraDays = Convert.ToInt32(days * (Convert.ToDouble(extraTime) / 100));
                deadline = deadline.AddDays(extraDays);
            }
            return deadline;
        }

        public int getExtraTimeFromDB()
        {
            Database database = new Database();
            database.OpenConnection();
            MySqlCommand cmd = new MySqlCommand("SELECT description FROM SpecialNeed WHERE type = 'Extra Time(%)' AND studentId = @val1");
            cmd.Connection = database.connection;
            cmd.Parameters.AddWithValue("@val1", this.studentId);
            MySqlDataReader result = cmd.ExecuteReader();
            int t = 0;
            while (result.Read()) {
                t = Convert.ToInt32(result["description"]);
            }
            database.CloseConnection();
            return t;
        }

        public void assignMarksToGroupMembers(double mark)
        {
            Database database = new Database();
            database.OpenConnection();
            MySqlCommand cmd = new MySqlCommand("SELECT AssignmentStudent.assignmentStudentId FROM AssignmentStudent INNER JOIN Team on Team.teamId = (SELECT Team.teamId FROM AssignmentStudentINNER JOIN StudentTeam ON StudentTeam.studentId = AssignmentStudent.studentId AND AssignmentStudent.StudentId = @val1 INNER JOIN Team ON Team.teamId = StudentTeam.teamId INNER JOIN Assignment ON Assignment.assignmentId = Team.assignmentId AND AssignmentStudent.assignmentId = @val2) AND AssignmentStudent.assignmentId = Team.assignmentId");
            cmd.Connection = database.connection;
            cmd.Parameters.AddWithValue("@val1", this.studentId);
            cmd.Parameters.AddWithValue("@val2", this.assignmentId);
            MySqlDataReader submissions = cmd.ExecuteReader();
            while (submissions.Read())
            {
                String sql = "UPDATE `db624820831`.`AssignmentStudent` SET `mark` = @val2_1 WHERE `AssignmentStudent`.`assignmentStudentId` = @val2_2";
                Database database2 = new Database();
                database2.OpenConnection();
                MySqlCommand cmd2 = new MySqlCommand(sql);
                cmd2.Connection = database2.connection;
                cmd.Parameters.AddWithValue("@val2_1", mark);
                cmd.Parameters.AddWithValue("@val2_2", submissions["assignmentStudentId"]);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                database2.CloseConnection();
            }
        }
        
        public String displayMark()
        {
            String result = this.mark.ToString() + "%";
            if(dateOfSubmission == null)
            {
                result = "N/A";
            }
            else if(this.mark == null)
            {
                result = "--Pending--";
            }
            return result;
        }
       
        public String displayDateOfSubmission()
        {
            String result = "Last Submission was made on " + this.dateOfSubmission;
            if(dateOfSubmission == null)
            {
                result = "No submission has been made as of yet.";
            }
            return result;
        }
        
        public String isSubmissionDisabled()
        {
            String status = "enabled";
            if(this.mark != null)
            {
                status = "disabled";
            }
            return status;
        }
        public String isViewSubmissionDisabled()
        {
            String status = "enabled";
            if (this.dateOfSubmission == null)
            {
                status = "disabled";
            }
            return status;
        }
        public String displayFileLink()
        {
            String link = "";
            if (this.filename != null && this.filepath != null)
            {
                if (this.filename != "" && this.filepath != "")
                {
                    link = "../Content/Files/Submissions/" + this.filepath;
                }
            }
            return link;
        }
        
        public Boolean isGroupTask()
        {
            Boolean result = false;
            if (this.submissionFormat.ToUpper() == "GROUP")
            {
                result = true;
            }
            return result;
        }
        
        public List<SimpleModel> getMembers_ids_names(String studentId)
        {
            List<SimpleModel> students = new List<SimpleModel>();
            Database database = new Database();
            database.OpenConnection();
            MySqlCommand cmd = new MySqlCommand("SELECT concat(User.firstname, ' ', User.lastname) As student, assignmentstudent.assignmentStudentId from AssignmentStudent inner join assignment on assignment.assignmentId = assignmentstudent.assignmentId and assignment.assignmentId = @val1 inner join team on team.assignmentId = assignment.assignmentId inner join User on user.userId = assignmentstudent.studentId inner join student on student.userId = user.userId inner join studentteam on studentTeam.teamId = team.teamId and studentTeam.studentId = user.userId and studentTeam.teamId =(SELECT team.teamId from team inner join studentTeam on studentTeam.studentId = @val2 and studentTeam.teamId = team.teamId and team.assignmentId = @val1)");
            cmd.Connection = database.connection;
            cmd.Parameters.AddWithValue("@val1", this.assignmentId);
            cmd.Parameters.AddWithValue("@val2", studentId);
            MySqlDataReader results = cmd.ExecuteReader();
            while (results.Read())
            {
                SimpleModel s = new SimpleModel(results["assignmentStudentId"].ToString(), results["student"].ToString());
                students.Add(s);
            }
            return students;
        }
        
        public String displayMembers(String studentId)
        {
            String result = "Members: ";
            List<SimpleModel> members = this.getMembers_ids_names(studentId);
            foreach (var m in members)
            {
                if (result != "Members: ")
                {
                    result = result + ",";
                }
                result = result + m.name;
            }
            if (result == "Members: ")
            {
                result = "Please create a team before you can add members.";
            }
            return result;
        }
        
        public String isMembershipFormDisabled(String studentId)
        {
            String result = "";
            List<SimpleModel> members = this.getMembers_ids_names(studentId);
            if (members.Count == 0)
            {
                result = "disabled";
            }
            return result;
        }
    }
}