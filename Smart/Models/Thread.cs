using SmartSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartSchool.Models
{
    public class Thread
    {
        public String title { get; set; }
        public String threadOwner { get; set; }
        public String courseModuleTYearId { get; set; }
        public String postOwner { get; set; }
        public String message { get; set; }

        public Thread(String title, String threadOwner, String courseModuleYearId, String postOwner, String message)
        {
            this.title = title;
            this.threadOwner = threadOwner;
            this.courseModuleTYearId = courseModuleTYearId;
            this.postOwner = postOwner;
            this.message = message;
        }
        public Thread(String title, String threadOwner, String courseModuleYearId)
        {
            this.title = title;
            this.threadOwner = threadOwner;
            this.courseModuleTYearId = courseModuleTYearId;
        }
    }
}