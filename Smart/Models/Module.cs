using SmartSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartSchool.Models
{
    public class Module
    {
        public List<Submission> studentModuleDetails { get; set; }
        public ProgressTracker progressTracker { get; set; }
        public List<SimpleModel> threads { get; set; }
        public List<SimpleModel> otherThreads { get; set; }
        public string moduleName { get; set; }

        public Module(List<Submission> studentModuleDetails, ProgressTracker progressTracker, List<SimpleModel> threads, List<SimpleModel> otherThreads)
        {
            this.studentModuleDetails = studentModuleDetails;
            this.progressTracker = progressTracker;
            this.threads = threads;
            this.otherThreads = otherThreads;
        }

    }
}