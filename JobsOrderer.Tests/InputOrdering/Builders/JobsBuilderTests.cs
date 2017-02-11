using JobsOrderer.Models;
using JobsOrderer.Tests.Comparers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace JobsOrderer.InputOrdering.Builders.Tests
{
    [TestClass()]
    public class JobsBuilderTests
    {
        Job jobA = new Job('A');
        Job jobB = new Job('B');
        Job jobC = new Job('C');

        [TestMethod()]
        public void GetJobListTest_EmptyString()
        {
            // arrange
            var jobString = "";
            var expected = new List<Job>();
            var jobsBuilder = new JobsBuilder();

            // act 
            var actual = jobsBuilder.GetJobList(jobString.ToUpper());

            // assert 
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetJobListTest_SimpleJob()
        {
            // arrange
            var jobString = "a";
            var expected = new List<Job>
            {
                jobA
            };
            var jobsBuilder = new JobsBuilder();

            // act 
            var actual = jobsBuilder.GetJobList(jobString.ToUpper());

            // assert 
            CollectionAssert.AreEqual(expected, actual, new JobsComparerByNameThenByFirstDifferentDependency());
        }

        [TestMethod()]
        public void GetJobListTest_TwoJobs()
        {
            // arrange
            var jobString = "a;b";
            var expected = new List<Job>
            {
                jobA,
                jobB
            };
            var jobsBuilder = new JobsBuilder();

            // act 
            var actual = jobsBuilder.GetJobList(jobString.ToUpper());

            // assert 
            CollectionAssert.AreEqual(expected, actual, new JobsComparerByNameThenByFirstDifferentDependency());
        }

        [TestMethod()]
        public void GetJobListTest_SimpleJobWithDependency1()
        {
            // arrange
            var jobString = "a;b=>a";
            var expected = new List<Job>
            {
                jobA,
                jobB
            };
            var jobsBuilder = new JobsBuilder();

            // act 
            var actual = jobsBuilder.GetJobList(jobString.ToUpper());

            // assert 
            CollectionAssert.AreNotEqual(expected, actual, new JobsComparerByNameThenByFirstDifferentDependency());
        }

        [TestMethod()]
        public void GetJobListTest_SimpleJobWithDependency2()
        {
            // arrange
            var jobString = "a;b=>a";

            jobB.Dependencies.Add(jobA);
            var expected = new List<Job>
            {
                jobA,
                jobB
            };
            var jobsBuilder = new JobsBuilder();

            // act 
            var actual = jobsBuilder.GetJobList(jobString.ToUpper());

            // assert 
            CollectionAssert.AreEqual(expected, actual, new JobsComparerByNameThenByFirstDifferentDependency());
        }

        [TestMethod()]
        public void GetJobListTest_ThreeJobs1()
        {
            // arrange
            var jobString = "a;b;c";
            
            var expected = new List<Job>
            {
                jobA,
                jobB, 
                jobC
            };
            var jobsBuilder = new JobsBuilder();

            // act 
            var actual = jobsBuilder.GetJobList(jobString.ToUpper());

            // assert 
            CollectionAssert.AreEqual(expected, actual, new JobsComparerByNameThenByFirstDifferentDependency());
        }

        [TestMethod()]
        public void GetJobListTest_ThreeJobs2()
        {
            // arrange
            var jobString = "a;c;b";
            
            var expected = new List<Job>
            {
                jobA,
                jobC,
                jobB
            };
            var jobsBuilder = new JobsBuilder();

            // act 
            var actual = jobsBuilder.GetJobList(jobString.ToUpper());

            // assert 
            CollectionAssert.AreEqual(expected, actual, new JobsComparerByNameThenByFirstDifferentDependency());
        }
    }
}