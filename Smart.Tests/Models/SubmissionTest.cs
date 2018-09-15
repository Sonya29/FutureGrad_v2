using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartSchool.Models;

namespace Smart.Tests.Models
{
    [TestClass]
    public class SubmissionTest
    {
        [TestMethod]
        public void getDeadline4Student()
        {
            Submission a = new Submission();
            a.studentId = "K1406302";
            a.setDate = new DateTime(2017, 09, 25);
            a.deadline = new DateTime(2017, 10, 01);
            DateTime newDeadline = a.getDeadlineForStudent();
            Assert.AreEqual(new DateTime(2017, 10, 04), newDeadline);
        }
    }
}


