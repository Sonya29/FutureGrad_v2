using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartSchool.Controllers;
using SmartSchool.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Smart.Tests.Controllers
{
    [TestClass]
    public class StudentTest
    {
        [TestMethod]
        public void StudentHome()
        {
            StudentController sc = new StudentController();
            List<SimpleModel> smList = new List<SimpleModel>();
            SimpleModel a = new SimpleModel("CI4100", "a");
            SimpleModel b = new SimpleModel("CI6530", "b");
            smList.Add(a);
            smList.Add(b);
            sc.studentHomeData = smList;
            ViewResult v = (ViewResult) sc.Index();
            Assert.AreEqual(2,  ((List<SimpleModel>) v.Model).Count);
        }
        //[TestMethod]
        //public void ModuleTest()
        //{
        //    StudentController sc = new StudentController();
        //    sc.progressTrackerView = new ProgressTrackerViewModel("K123456789", "G600_CI65320_2016", 2017, 72.5, 80);
        //    SimpleModel t1 = new SimpleModel(1, "Hello World!");
        //    SimpleModel t2 = new SimpleModel(1, "Threads");
        //    List<SimpleModel> tList = new List<SimpleModel>();
        //    tList.Add(t1);
        //    tList.Add(t2);
        //    sc.threadsView = tList;
        //    SimpleModel t3 = new SimpleModel(1, "?");
        //    SimpleModel t4 = new SimpleModel(1, "What is a...?");
        //    List<SimpleModel> otherTlist = new List<SimpleModel>();
        //    sc.otherThreadsView = otherTlist;
        //    List<SubmissionViewModel> svmList = new List<SubmissionViewModel>();
        //}
    }
}
