using JobsOrderer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace JobsOrderer.InputOrdering.Validators.Tests
{
    [TestClass()]
    public class JobsValidatorTests
    {
        Job jobA = new Job('A');
        Job jobB = new Job('B');
        Job jobC = new Job('C');

        [TestMethod()]
        public void IsValidListOfJobsTest_EmptyListOfJobs()
        {
            // arrange
            var jobs = new List<Job>();

            var expected = true;
            var jobsValidator = new JobsValidator();

            // act 
            var actual = jobsValidator.IsValidListOfJobs(jobs);

            // assert 
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsValidListOfJobsTest_SingleJob()
        {
            // arrange
            var jobs = new List<Job>
            {
                jobA
            };
            var expected = true;
            var jobsValidator = new JobsValidator();

            // act 
            var actual = jobsValidator.IsValidListOfJobs(jobs);

            // assert 
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsValidListOfJobsTest_TwoJobs()
        {
            // arrange
            var jobs = new List<Job>
            {
                jobA,
                jobB
            };
            var expected = true;
            var jobsValidator = new JobsValidator();

            // act 
            var actual = jobsValidator.IsValidListOfJobs(jobs);

            // assert 
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsValidListOfJobsTest_SelfDependency1()
        {
            // arrange
            jobA.Dependencies.Add(jobA);
            var jobs = new List<Job>
            {
                jobA
            };
            var expected = false;
            var jobsValidator = new JobsValidator();

            // act 
            var actual = jobsValidator.IsValidListOfJobs(jobs);

            // assert 
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsValidListOfJobsTest_SelfDependency2()
        {
            // arrange
            var jobACopy = new Job('A');
            jobA.Dependencies.Add(jobACopy);
            var jobs = new List<Job>
            {
                jobA
            };
            var expected = false;
            var jobsValidator = new JobsValidator();

            // act 
            var actual = jobsValidator.IsValidListOfJobs(jobs);

            // assert 
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsValidListOfJobsTest_Duplicates1()
        {
            // arrange
            var jobs = new List<Job>
            {
                jobA,
                jobA
            };
            var expected = false;
            var jobsValidator = new JobsValidator();

            // act 
            var actual = jobsValidator.IsValidListOfJobs(jobs);

            // assert 
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsValidListOfJobsTest_Duplicates2()
        {
            // arrange
            var jobACopy = new Job('A');
            var jobs = new List<Job>
            {
                jobA,
                jobACopy
            };
            var expected = false;
            var jobsValidator = new JobsValidator();

            // act 
            var actual = jobsValidator.IsValidListOfJobs(jobs);

            // assert 
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsValidListOfJobsTest_Duplicates3()
        {
            // arrange
            var jobACopy = new Job('A');
            jobACopy.Dependencies.Add(jobB);
            var jobs = new List<Job>
            {
                jobA,
                jobACopy
            };
            var expected = false;
            var jobsValidator = new JobsValidator();

            // act 
            var actual = jobsValidator.IsValidListOfJobs(jobs);

            // assert 
            Assert.AreEqual(expected, actual);
        }
    }
}