using JobsOrderer.InputOrdering;
using JobsOrderer.InputOrdering.Algorithms;
using JobsOrderer.InputOrdering.Builders;
using JobsOrderer.InputOrdering.Validators;

namespace JobsOrderer.Builder
{
    /// <summary>
    /// Static class to build a JobSorter.
    /// Uses singleton pattern.
    /// </summary>
    public static class JobsSorterBuilder
    {
        private static JobsSorter instance;

        public static JobsSorter CreateJobsSorter()
        {
            if (instance == null)
            {
                var stringValidator = new StringValidatorForCharJobs();
                var jobsBuilder = new JobsBuilder();
                var jobsValidator = new JobsValidator();
                var sortAlgorithm = new TopologicalSortAlgorithm();

                instance = new JobsSorter(stringValidator, jobsBuilder, jobsValidator, sortAlgorithm);
            }

            return instance;
        }
    }
}
