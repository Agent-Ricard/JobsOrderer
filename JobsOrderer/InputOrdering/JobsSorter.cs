using JobsOrderer.InputOrdering.Algorithms;
using JobsOrderer.InputOrdering.Builders;
using JobsOrderer.InputOrdering.Validators;
using JobsOrderer.Models;
using System;
using System.Collections.Generic;

namespace JobsOrderer.InputOrdering
{
    /// <summary>
    /// The main class of this program. 
    /// Uses : 
    ///     - IStringValidator to check the query 
    ///     - JobsBuilder to create the Jobs from the string
    ///     - JobsValidator to check the Job List
    ///     - ISortAlgorithm to sort the Job List
    /// </summary>
    /// <remarks>
    /// The string validator is interfaced as the wanted syntax could change. For example, we could switch to jobs using strings instead of chars.
    /// The sort algorithm is interfaced as we might want to use another sort algorithm.
    /// I did not interfaced the jobs builder as it feels tightly coupled to the jobs themselves. Probably discussable. 
    /// The job validator is not interfaced as the 'rules' to have a valid sortable graph do not change over time. 
    /// </remarks>
    public class JobsSorter
    {
        IStringValidator _stringValidator;
        JobsBuilder _jobsBuilder;
        JobsValidator _jobsValidator;
        ISortAlgorithm _sortAlgorithm;

        public JobsSorter(IStringValidator stringValidator, JobsBuilder jobsBuilder, JobsValidator jobsValidator, ISortAlgorithm sortAlgorithm)
        {
            _stringValidator = stringValidator;
            _jobsBuilder = jobsBuilder;
            _jobsValidator = jobsValidator;
            _sortAlgorithm = sortAlgorithm;
        }
        
        public List<Job> GetOrderedJobList(string jobsString)
        {
            if (!_stringValidator.IsValidString(jobsString))
                throw new Exception("Error: wrong syntax");

            var jobs = _jobsBuilder.GetJobList(jobsString);

            if (!_jobsValidator.IsValidListOfJobs(jobs))
                throw new Exception("Error: the job list is not coherent");

            return _sortAlgorithm.Sort(jobs);
        }
    }
}
