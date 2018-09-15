using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartSchool.Models
{
    public class Thread_DB
    {
        public int threadId { get; set; }
        public String title { get; set; }
        public String userId { get; set; }
        public String studentId { get; set; }
        public String courseModuleYearId { get; set; }

        public Thread_DB(int threadId, String title, String userId, String studentId, String courseModuleYearId)
        {
            this.threadId = threadId;
            this.title = title;
            this.userId = userId;
            this.studentId = studentId;
            this.courseModuleYearId = courseModuleYearId;
        }
    }
}