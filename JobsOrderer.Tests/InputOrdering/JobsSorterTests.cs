using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using JobsOrderer.Models;
using JobsOrderer.Builder;
using JobsOrderer.Tests.Comparers;
using System;

namespace JobsOrderer.InputOrdering.Tests
{
    [TestClass()]
    public class JobsSorterTests
    {
        Job jobA = new Job('A');
        Job jobB = new Job('B');
        Job jobC = new Job('C');

        [TestMethod()]
        public void GetOrderedJobListTest_EmptyString()
        {
            // arrange
            var jobString = "";
            var expected = new List<Job>();
            var jobsSorter = JobsSorterBuilder.CreateJobsSorter();

            // act 
            var actual = jobsSorter.GetOrderedJobList(jobString.ToUpper());

            // assert 
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetOrderedJobListTest_SingleJob()
        {
            // arrange
            var jobString = "a";
            var expected = new List<Job>
            {
                jobA
            };
            var jobsSorter = JobsSorterBuilder.CreateJobsSorter();

            // act 
            var actual = jobsSorter.GetOrderedJobList(jobString.ToUpper());

            // assert 
            CollectionAssert.AreEqual(expected, actual, new JobsComparerByName()); 
        }

        [TestMethod()]
        public void GetOrderedJobListTest_TwoJobs1()
        {
            // arrange
            var jobString = "a;b=>a";
            var expected = new List<Job>
            {
                jobA,
                jobB
            };
            var jobsSorter = JobsSorterBuilder.CreateJobsSorter();

            // act 
            var actual = jobsSorter.GetOrderedJobList(jobString.ToUpper());

            // assert 
            CollectionAssert.AreEqual(expected, actual, new JobsComparerByName());
        }

        [TestMethod()]
        public void GetOrderedJobListTest_TwoJobs2()
        {
            // arrange
            var jobString = "a=>b;b";
            var expected = new List<Job>
            {
                jobB,
                jobA
            };
            var jobsSorter = JobsSorterBuilder.CreateJobsSorter();

            // act 
            var actual = jobsSorter.GetOrderedJobList(jobString.ToUpper());

            // assert 
            CollectionAssert.AreEqual(expected, actual, new JobsComparerByName());
        }

        [TestMethod()]
        public void GetOrderedJobListTest_ThreeJobs()
        {
            // arrange
            var jobString = "a;b=>a;c=>b";
            var expected = new List<Job>
            {
                jobA,
                jobB,
                jobC
            };
            var jobsSorter = JobsSorterBuilder.CreateJobsSorter();

            // act 
            var actual = jobsSorter.GetOrderedJobList(jobString.ToUpper());

            // assert 
            CollectionAssert.AreEqual(expected, actual, new JobsComparerByName());
        }

        [TestMethod()]
        public void GetOrderedJobListTest_SyntaxError()
        {
            // arrange
            var jobString = "a$b";
            var expected = "Error: wrong syntax";
            var jobsSorter = JobsSorterBuilder.CreateJobsSorter();
            
            // act 
            try
            {
                var actual = jobsSorter.GetOrderedJobList(jobString.ToUpper());
            }
            catch (Exception e)
            {
                // assert
                Assert.AreEqual(expected, e.Message);
            }
        }

        [TestMethod()]
        public void GetOrderedJobListTest_ConsistencyError()
        {
            // arrange
            var jobString = "a=>a";
            var expected = "Error: the job list is not coherent";
            var jobsSorter = JobsSorterBuilder.CreateJobsSorter();

            // act 
            try
            {
                var actual = jobsSorter.GetOrderedJobList(jobString.ToUpper());
            }
            catch (Exception e)
            {
                // assert
                Assert.AreEqual(expected, e.Message);
            }
        }

        [TestMethod()]
        public void GetOrderedJobListTest_CircularDependencyError()
        {
            // arrange
            var jobString = "a=>b;b=>c;c=>a";
            var expected = "Error: There is atleast one cyclic dependency";
            var jobsSorter = JobsSorterBuilder.CreateJobsSorter();

            // act 
            try
            {
                var actual = jobsSorter.GetOrderedJobList(jobString.ToUpper());
            }
            catch (Exception e)
            {
                // assert
                Assert.AreEqual(expected, e.Message);
            }
        }
    }
}