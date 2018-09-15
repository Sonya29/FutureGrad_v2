using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartSchool.Models
{
    public class Assignment
    {
        public int id { get; set; }
        public String title { get; set; }
        public DateTime deadline { get; set; }
        public String filename { get; set; }
        public String filepath { get; set; }
        public double moduleWeight { get; set; }
        public DateTime setDate { get; set; }
        public String submissionFormat { get; set; }
        public String lecturerName { get; set; }


        public Assignment()
        {
            this.id = -1;
        }

        public Assignment(int id, String title, double weight, String filepath, DateTime deadline, DateTime setDate, String submissionFormat)
        {
            this.id = id;
            this.title = title;
            this.moduleWeight = weight;
            this.filepath = filepath;
            this.deadline = deadline;
            this.setDate = setDate;
            this.submissionFormat = submissionFormat;
        }

        public Assignment(int id,String title, DateTime deadline,String filename,String filepath,double moduleWeight, DateTime setDate,String submissionFormat,String lecturerName)
        {
            this.id = id;
		    this.title = title;
		    this.deadline = deadline;
		    this.filepath = filepath;
		    this.filename = filename;
		    this.moduleWeight = moduleWeight;
		    this.setDate = setDate;
		    this.submissionFormat = submissionFormat;
		    this.lecturerName = lecturerName;
        }
        
        public Assignment(String title, double weight, String filepath, DateTime deadline, DateTime setDate, String submissionFormat)
        {
            this.title = title;
            this.moduleWeight = weight;
            this.filepath = filepath;
            this.deadline = deadline;
            this.setDate = setDate;
            this.submissionFormat = submissionFormat;
        }

        public String displayFile()
        {
		    String result = "No File Selected.";
            if (this.filename != null  && this.filepath != null)
            {
			    result = this.filename;
            }
            return result;
        }


    }
}