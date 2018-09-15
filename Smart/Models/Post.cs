using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartSchool.Models
{
    public class Post
    {
        public int threadId { get; set; }
        public String courseModuleYearId { get; set; }
        public String title { get; set; }
        public String message { get; set; }
        public String threadOwnerName { get; set; }
        public String postOwnerName { get; set; }

        public Post(int threarId, String courseModuleYearId, String title, String message, String threadOwnerName, String postOwnerName)
        {
            this.threadId = threadId;
            this.courseModuleYearId = courseModuleYearId;
            this.title = title;
            this.message = message;
            this.threadOwnerName = threadOwnerName;
            this.postOwnerName = postOwnerName;
        }

        public String displayPostUser()
        {
            String x = "";
            if (this.postOwnerName != null)
            {
	            x = this.postOwnerName + " says:";
            }
            return x;
        }

    }
}