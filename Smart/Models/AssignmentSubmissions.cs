using SmartSch.Models;
using SmartSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartSchool.Models
{
    public class AssignmentSubmissions
    {
        public Assignment assignment { get; set; }
        public List<Submission> submissions { get; set; }

        public AssignmentSubmissions(Assignment assignment, List<Submission> submissions)
        {
            this.assignment = assignment;
            this.submissions = submissions;
        }
    }
}