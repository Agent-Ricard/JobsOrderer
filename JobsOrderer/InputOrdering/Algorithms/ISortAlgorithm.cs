using JobsOrderer.Models;
using System.Collections.Generic;

namespace JobsOrderer.InputOrdering.Algorithms
{
    public interface ISortAlgorithm
    {
        List<Job> Sort(List<Job> jobs);
    }
}
