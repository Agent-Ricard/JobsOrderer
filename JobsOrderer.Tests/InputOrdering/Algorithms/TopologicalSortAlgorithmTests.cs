using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using JobsOrderer.Models;
using JobsOrderer.Tests.Comparers;

namespace JobsOrderer.InputOrdering.Algorithms.Tests
{
    [TestClass()]
    public class TopologicalSortAlgorithmTests
    {
        Job jobA = new Job('A');
        Job jobB = new Job('B');
        Job jobC = new Job('C');

        [TestMethod()]
        public void SortTest_Empty()
        {
            // arrange
            var jobs = new List<Job>();
            var expected = new List<Job>();
            var topologicalSortAlgorithm = new TopologicalSortAlgorithm();

            // act 
            var actual = topologicalSortAlgorithm.Sort(jobs);

            // assert 
            CollectionAssert.AreEqual(expected, actual, new JobsComparerByNameThenByFirstDifferentDependency());
        }

        [TestMethod()]
        public void SortTest_TwoJobsDependency()
        {
            // arrange
            jobB.Dependencies.Add(jobA);
            var jobs = new List<Job>
            {
                jobA,
                jobB
            };
            var expected = jobs;
            var topologicalSortAlgorithm = new TopologicalSortAlgorithm();

            // act 
            var actual = topologicalSortAlgorithm.Sort(jobs);

            // assert
            CollectionAssert.AreEqual(expected, actual, new JobsComparerByNameThenByFirstDifferentDependency());
        }

        [TestMethod()]
        public void SortTest_ThreeJobsDependency()
        {
            // arrange
            jobC.Dependencies.Add(jobB);
            jobB.Dependencies.Add(jobA);
            var jobs = new List<Job>
            {
                jobA,
                jobB,
                jobC
            };
            var expected = jobs;
            var topologicalSortAlgorithm = new TopologicalSortAlgorithm();

            // act 
            var actual = topologicalSortAlgorithm.Sort(jobs);

            // assert
            CollectionAssert.AreEqual(expected, actual, new JobsComparerByNameThenByFirstDifferentDependency());
        }

        [TestMethod()]
        public void SortTest_CircularDependencyError()
        {
            // arrange
            jobA.Dependencies.Add(jobB);
            jobB.Dependencies.Add(jobC);
            jobC.Dependencies.Add(jobA);
            var jobs = new List<Job>
            {
                jobA,
                jobB,
                jobC
            };
            var expected = "Error: There is atleast one cyclic dependency";
            var topologicalSortAlgorithm = new TopologicalSortAlgorithm();

            // act 
            try
            {
                var actual = topologicalSortAlgorithm.Sort(jobs);
            }
            catch (Exception e)
            {
                // assert
                Assert.AreEqual(expected, e.Message);
            }
        }
    }
}