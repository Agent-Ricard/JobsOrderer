using System;
using System.Text.RegularExpressions;
using JobsOrderer.InputOrdering.Delimiters;

namespace JobsOrderer.InputOrdering.Validators
{
    /// <summary>
    ///  Checks the syntax of a string input
    /// </summary>
    public class StringValidatorForCharJobs : IStringValidator
    {
        private string jobSeparation;
        private string jobDependency;

        public StringValidatorForCharJobs()
        {
            jobSeparation = JobDelimiters.JobSeparation;
            jobDependency = JobDelimiters.JobDependency;
        }

        /// <summary>
        /// Checks if the string is valid
        /// </summary>
        /// <param name="jobs"></param>
        /// <returns>true if empty or passes all the other syntaxical checks</returns>
        public bool IsValidString(string jobs)
        {
            if (IsEmpty(jobs)) return true;

            return IsValidForSpecialCharacters(jobs)
                && IsValidJobsInput(jobs)
                && IsValidDelimiters(jobs);
        }

        /// <summary>
        /// Checks if the string is empty
        /// </summary>
        /// <param name="jobs"></param>
        /// <returns>true if the string is empty</returns>
        private bool IsEmpty(string jobs)
        {
            return String.IsNullOrEmpty(jobs);
        }

        /// <summary>
        /// Checks if the string contains only the authorized characters
        /// </summary>
        /// <param name="jobs"></param>
        /// <returns>true if the string only contains uppercase characters, separator (;) or dependency (=>)</returns>
        private bool IsValidForSpecialCharacters(string jobs)
        {
            /* this regex matches everything that is not : 
             * - an uppercase character
             * - ; (separator symbol)
             * - => (dependency symbol)
             */
            var regex = new Regex(@"[^A-Z"+jobSeparation+"("+jobDependency+")]");

            // it's a success if the pattern does not have any match
            return !(regex.Match(jobs).Success);
        }

        /// <summary>
        /// Checks if the jobs in the string are formed well (ie. only one char)
        /// </summary>
        /// <param name="jobs"></param>
        /// <returns>false if there are two or more consecutive chars</returns>
        private bool IsValidJobsInput(string jobs)
        {
            // this regex matches every sequence of two or more consecutive chars
            var regex = new Regex(@"\w{2,}");

            // it's a success if the pattern does not have any match
            return !(regex.Match(jobs).Success);
        }

        /// <summary>
        /// Check if there is there are consecutive delimiters
        /// </summary>
        /// <param name="jobs"></param>
        /// <returns>false if there are two consecutive delimiters</returns>
        private bool IsValidDelimiters(string jobs)
        {
            // this regex matches every sequence of two or more consecutive delimiters
            var regex = new Regex(@"("+jobDependency+"|"+jobSeparation+"){2,}");
            
            // it's a success if the pattern does not have any match
            return !(regex.Match(jobs).Success);
        }
    }
}
