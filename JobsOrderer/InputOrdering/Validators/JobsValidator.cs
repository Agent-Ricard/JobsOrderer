using JobsOrderer.Models;
using System.Collections.Generic;
using System.Linq;

namespace JobsOrderer.InputOrdering.Validators
{
    /// <summary>
    /// Checks the validity of a list of jobs
    /// </summary>
    /// <remarks>
    /// I did not include the check for cycles here as the TopologicalSort does it.
    /// </remarks>
    public class JobsValidator
    {
        public JobsValidator()
        {

        }
 
        public bool IsValidListOfJobs(List<Job> jobs)
        {
            return IsValidForDuplicates(jobs)
                && IsValidForSelfDependency(jobs);
        }

        /// <summary>
        /// Checks for duplicate jobs in the list
        /// </summary>
        /// <remarks>
        /// Distinct() removes duplicates based on the default comparer.
        /// Job implements IEquatable and two Jobs are considered equals if they share the same name.
        /// If there are duplicates, jobs.Distinct().Count() will be smaller than jobs.Count
        /// </remarks>
        /// <param name="jobs"></param>
        /// <returns>true if there is no duplicate</returns>
        private bool IsValidForDuplicates(List<Job> jobs)
        {
            return jobs.Distinct().Count() == jobs.Count;
        }

        /// <summary>
        /// Checks if any item has a dependency on himself
        /// </summary>
        /// <remarks>
        /// I'm pretty sure there is a better LINQ query to do that.
        /// </remarks>
        /// <param name="jobs"></param>
        /// <returns>true if there is no self dependency</returns>
        private bool IsValidForSelfDependency(List<Job> jobs)
        {
            foreach(var job in jobs)
                if (job.Dependencies.Any(dep => job.Name.Equals(dep.Name)))
                    return false;

            return true;
        }
    }
}
