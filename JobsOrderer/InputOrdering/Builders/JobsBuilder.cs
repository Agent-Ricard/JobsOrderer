using JobsOrderer.InputOrdering.Delimiters;
using JobsOrderer.Models;
using System;
using System.Collections.Generic;

namespace JobsOrderer.InputOrdering.Builders
{
    /// <summary>
    /// Transforms a valid string input into a list of jobs
    /// </summary>
    public class JobsBuilder
    {
        public JobsBuilder()
        {

        }

        public List<Job> GetJobList(string jobs)
        {
            var results = new List<Job>();

            if (string.IsNullOrWhiteSpace(jobs)) return results;

            var jobsAndDependenciesStrings = jobs.Split(new string[] { JobDelimiters.JobSeparation }, StringSplitOptions.None); // returns jobs and their eventual dependencies in an array of string
            foreach(var jobAndDependencyString in jobsAndDependenciesStrings)
            {
                var name = jobAndDependencyString.ToCharArray()[0]; 
                var job = new Job(name);
                if (jobAndDependencyString.Contains(JobDelimiters.JobDependency))
                {
                    char dependency = jobAndDependencyString.ToCharArray()[JobDelimiters.JobDependency.Length + 1];
                    job.Dependencies.Add(new Job(dependency));
                }
                
                results.Add(job);
            }

            return results;
        }
    }
}
