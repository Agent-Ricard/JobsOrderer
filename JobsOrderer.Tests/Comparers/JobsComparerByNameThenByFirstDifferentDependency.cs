using JobsOrderer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobsOrderer.Tests.Comparers
{
    /// <summary>
    /// Compares two jobs based on their names,
    /// then on their dependency count, 
    /// then on the first different dependency name.
    /// If no difference, returns 0
    /// </summary>
    public class JobsComparerByNameThenByFirstDifferentDependency : Comparer<Job>
    {
        public override int Compare(Job x, Job y)
        {
            if (x.Name != y.Name)
                return x.Name.CompareTo(y.Name);
            if (x.Dependencies.Count != y.Dependencies.Count)
                return x.Dependencies.Count.CompareTo(y.Dependencies.Count);
            for (var i = 0; i < x.Dependencies.Count; i++)
                if (x.Dependencies[i].Name != y.Dependencies[i].Name)
                    return x.Dependencies[i].Name.CompareTo(y.Dependencies[i].Name);
            return 0;
        }
    }
}
