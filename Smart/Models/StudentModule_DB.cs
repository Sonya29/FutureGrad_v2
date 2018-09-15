using MySql.Data.MySqlClient;
using SmartSch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartSchool.Models
{
    public class StudentModule_DB
    {
        public int assignmentStudentId { get; set; }
        public int assignmentId { get; set; }
        public String moduleName { get; set; }
        public int year { get; set; }
        public String assignmentTitle { get; set; }
        public double moduleWeight { get; set; }
        public String lecturerName { get; set; }
        public String mark;
        public String dateOfSubmission;
        public String filepath { get; set; }
        public String filename { get; set; }
        public DateTime deadline { get; set; }
        public String assignmentLink { get; set; }
        public String submissionFormat { get; set; }

        public StudentModule_DB(int assignmentStudentId, int assignmentId,String moduleName,int year,String assignmentTitle, double moduleWeight, String lecturerName, String mark, String dateOfSubmission, String filepath, String filename, DateTime deadline, String assignmentLink, String submissionFormat)
        {
            this.assignmentStudentId = assignmentStudentId;
            this.assignmentId = assignmentId;
            this.moduleName = moduleName;
            this.year = year;
            this.assignmentTitle = assignmentTitle;
            this.moduleWeight = moduleWeight;
            this.lecturerName = lecturerName;
            if(mark == null)
            {
                this.mark = "Mark: --pending--";
            }else
            {
                this.mark = mark;
            }
            if (dateOfSubmission == null)
            {
                this.dateOfSubmission = "No submission has been made as of yet";
            }
            else
            {
                this.dateOfSubmission = dateOfSubmission;
            }
            this.filepath = filepath;
            this.filename = filename;
            this.deadline = deadline;
            this.assignmentLink = assignmentLink;
            this.submissionFormat = submissionFormat;
        }

        public String isSubmissionDisabled()
        {
            double markAsInt = -1;
            String status = "";
            try
            {
                markAsInt = this.getMark();
                status = "disabled";
            }catch(Exception e)
            {
                status = "";
            }
            return status;
        }

        public String displayMark()
        {
            String result = "";
            try
            {
                Convert.ToDouble(this.mark);
                result = "Mark: " + this.mark;
            }catch(Exception e)
            {
                try
                {
                    Convert.ToDateTime(this.dateOfSubmission);
                    result = "Mark: --pending--";
                }
                catch (Exception ex)
                {
                    result = "Mark: N/A";
                }
            }
            return result;
        }

        public double getMark()
        {
            return Convert.ToDouble(this.mark);
        }

        public String displayDateOfSubmission()
        {
            String result = "";
            try
            {
                Convert.ToDateTime(this.dateOfSubmission);
                result = "Last Submission made on " + this.dateOfSubmission;
            }catch(Exception e)
            {
                result = "No submission was made to this assignment";
            }
            return result;
        }

        public DateTime getDateOfSubmission()
        {
            return Convert.ToDateTime(this.dateOfSubmission);
        }

        public String displayFileLink()
        {
            String link = "";
            if(this.filename != null && this.filepath != null)
            {
                if(this.filename != "" && this.filepath != "")
                {
                    link = "../Content/Files/Submissions/" + this.filepath; 
                }
            }
            return link;
        }

        public Boolean isGroupTask()
        {
            Boolean result = false;
            if(this.submissionFormat.ToUpper() == "GROUP")
            {
                result = true;
            }
            return result;
        }

        public List<Model> getMembers_ids_names(String studentId)
        {
            List<Model> students = new List<Model>();
            Database database = new Database();
            database.OpenConnection();
            MySqlCommand cmd = new MySqlCommand("SELECT concat(User.firstname, ' ', User.lastname) As student, assignmentstudent.assignmentStudentId from AssignmentStudent inner join assignment on assignment.assignmentId = assignmentstudent.assignmentId and assignment.assignmentId = @val1 inner join team on team.assignmentId = assignment.assignmentId inner join User on user.userId = assignmentstudent.studentId inner join student on student.userId = user.userId inner join studentteam on studentTeam.teamId = team.teamId and studentTeam.studentId = user.userId and studentTeam.teamId =(SELECT team.teamId from team inner join studentTeam on studentTeam.studentId = @val2 and studentTeam.teamId = team.teamId and team.assignmentId = @val1)");
            cmd.Connection = database.connection;
            cmd.Parameters.AddWithValue("@val1", this.assignmentId);
            cmd.Parameters.AddWithValue("@val2", studentId);
            MySqlDataReader results = cmd.ExecuteReader();
            while (results.Read())
            {
                Model s = new Model(results["assignmentStudentId"].ToString(), results["student"].ToString());
                students.Add(s);
            }
            return students;
        }

        public String displayMembers(String studentId)
        {
            String result = "Members: ";
            List<Model> members = this.getMembers_ids_names(studentId);
            foreach(var m in members)
            {
                if(result != "Members: ")
                {
                    result = result + ",";
                }
                result = result + m.name;
            }
            if(result == "Members: ")
            {
                result = "Please create a team before you can add members.";
            }
            return result;
        }

        public String isMembershipFormDisabled(String studentId)
        {
            String result = "";
            List<Model> members = this.getMembers_ids_names(studentId);
            if(members.Count == 0)
            {
                result = "disabled";
            }
            return result;
        }

    }
}