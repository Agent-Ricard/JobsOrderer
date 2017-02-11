using JobsOrderer.Builder;
using JobsOrderer.Models;
using System;
using System.Collections.Generic;

namespace InputOrdering
{
    class Program
    {
        static void Main(string[] args)
        {
            // the input
            var jobs = @"a;b=>c;c=>f;d=>a;e=>b;f";
            
            try
            {
                var orderedJobList = GetOrderedJobListFromString(jobs);

                // display
                if (orderedJobList != null)
                    foreach (var job in orderedJobList)
                        Console.Write(job.Name);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }

        static List<Job> GetOrderedJobListFromString(string jobs)
        {
            // create the sorter
            var jobsSorter = JobsSorterBuilder.CreateJobsSorter();
            return jobsSorter.GetOrderedJobList(jobs.ToUpper());
        }
    }
}
