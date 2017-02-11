using Microsoft.VisualStudio.TestTools.UnitTesting;
using JobsOrderer.InputOrdering.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobsOrderer.InputOrdering.Validators.Tests
{
    [TestClass()]
    public class StringValidatorForCharJobsTests
    {
        [TestMethod()]
        public void IsValidStringTest_EmptyString()
        {
            // arrange
            var jobString = "";
            var expected = true;
            var stringValidator = new StringValidatorForCharJobs();

            // act 
            var actual = stringValidator.IsValidString(jobString.ToUpper());

            // assert 
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsValidStringTest_ValidString()
        {
            // arrange
            var jobString = "a;b=>a;c";
            var expected = true;
            var stringValidator = new StringValidatorForCharJobs();

            // act 
            var actual = stringValidator.IsValidString(jobString.ToUpper());

            // assert 
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsValidStringTest_SpecialCharacterString()
        {
            // arrange
            var jobString = "a;b=>a;c*";
            var expected = false;
            var stringValidator = new StringValidatorForCharJobs();

            // act 
            var actual = stringValidator.IsValidString(jobString.ToUpper());

            // assert 
            Assert.AreEqual(expected, actual);
        }
        
        [TestMethod()]
        public void IsValidStringTest_DoubleChar()
        {
            // arrange
            var jobString = "aa;b=>a;c";
            var expected = false;
            var stringValidator = new StringValidatorForCharJobs();

            // act 
            var actual = stringValidator.IsValidString(jobString.ToUpper());

            // assert 
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsValidStringTest_DoubleSeparation()
        {
            // arrange
            var jobString = "a;;b=>a;c";
            var expected = false;
            var stringValidator = new StringValidatorForCharJobs();

            // act 
            var actual = stringValidator.IsValidString(jobString.ToUpper());

            // assert 
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsValidStringTest_DoubleDependency()
        {
            // arrange
            var jobString = "a;b=>=>a;c";
            var expected = false;
            var stringValidator = new StringValidatorForCharJobs();

            // act 
            var actual = stringValidator.IsValidString(jobString.ToUpper());

            // assert 
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsValidStringTest_DoublMixedDelimiter1()
        {
            // arrange
            var jobString = "a;b=>;a;c";
            var expected = false;
            var stringValidator = new StringValidatorForCharJobs();

            // act 
            var actual = stringValidator.IsValidString(jobString.ToUpper());

            // assert 
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsValidStringTest_DoublMixedDelimiter2()
        {
            // arrange
            var jobString = "a;b;=>a;c";
            var expected = false;
            var stringValidator = new StringValidatorForCharJobs();

            // act 
            var actual = stringValidator.IsValidString(jobString.ToUpper());

            // assert 
            Assert.AreEqual(expected, actual);
        }
    }
}