using JobsOrderer.Models;
using System.Collections.Generic;

namespace JobsOrderer.Tests.Comparers
{
    public class JobsComparerByName : Comparer<Job>
    {
        public override int Compare(Job x, Job y)
        {
            if (x.Name != y.Name)
                return x.Name.CompareTo(y.Name);
            return 0;
        }
    }
}
