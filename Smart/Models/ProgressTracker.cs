using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartSchool.Models
{
    public class ProgressTracker
    {
        public String studentId { get; set; }
        public String courseModuleYearId { get; set; }
        public int year { get; set; }
        public double moduleProgress { get; set; }
        public double maxMark { get; set; }
        public String status { get; set; }
        public String grade { get; set; }

        public ProgressTracker(String studentId, String courseModuleYearId, int year, double moduleProgress, double maxMark)
        {
            this.studentId = studentId;
            this.courseModuleYearId = courseModuleYearId;
            this.year = year;
            if(moduleProgress.ToString() == null)
            {
                this.moduleProgress = 0;
            }else
            {
                this.moduleProgress = Math.Round(moduleProgress, 0, MidpointRounding.AwayFromZero);
            }
            if(maxMark.ToString() == null)
            {
                this.maxMark = 0;
            }else
            {
                this.maxMark = maxMark;
            }
            setStatusAndGrade(moduleProgress);
        }
        public ProgressTracker(double moduleProgress, double maxMark)
        {
            this.studentId = null;
            this.courseModuleYearId = null;
            this.year = 0;
            if (moduleProgress.ToString() == null)
            {
                this.moduleProgress = 0;
            }
            else
            {
                this.moduleProgress = Math.Round(moduleProgress, 0, MidpointRounding.AwayFromZero);
            }
            if (maxMark.ToString() == null)
            {
                this.maxMark = 0;
            }
            else
            {
                this.maxMark = maxMark;
            }
            setStatusAndGrade(moduleProgress);
        }
        private void setStatusAndGrade(double mark)
        {
            if(mark < 40)
            {
                this.status = "progress-bar progress-bar-danger";
                this.grade = "U";
            }
            else
            {
                this.status = "progress-bar progress-bar-success";
                if(mark >= 85)
                {
                    this.grade = "A+";
                }else if(mark >= 75)
                {
                    this.grade = "A";
                }
                else if (mark >= 70)
                {
                    this.grade = "A-";
                }
                else if (mark >= 67)
                {
                    this.grade = "B+";
                }
                else if (mark >= 64)
                {
                    this.grade = "B";
                }
                else if (mark >= 60)
                {
                    this.grade = "A-";
                }
                else if (mark >= 57)
                {
                    this.grade = "C+";
                }
                else if (mark >= 54)
                {
                    this.grade = "C";
                }
                else if (mark >= 50)
                {
                    this.grade = "C-";
                }
                else if (mark >= 47)
                {
                    this.grade = "D+";
                }
                else if (mark >= 44)
                {
                    this.grade = "D";
                }
                else if (mark >= 40)
                {
                    this.grade = "D-";
                }
            }
        }

    }
}