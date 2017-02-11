using System;
using System.Collections.Generic;

namespace JobsOrderer.Models
{
    /// <summary>
    /// A Job is a unit of work. It has a name and a list of dependent jobs.
    /// </summary>
    /// <remarks>
    /// Two jobs are considered equals if they have the same name.
    /// Oddly, to remove duplicates jobs from a List, it does not use Equals (IEquatable implementation) but GetHashCode(), which is why I implemented it here.
    /// 
    /// Also, it would probably be slicker to use a string instead of a char for the Name property (cf the way Jobs are created in JobsBuilder),
    /// but the specs of the exercise mention that a job name is a single letter, so I decided to have a model as close as possible to the specs.
    /// </remarks>
    public class Job : IEquatable<Job>
    {
        public char Name;
        public List<Job> Dependencies;

        public Job(char name)
        {
            Name = name;
            Dependencies = new List<Job>();
        }

        // IEquatable
        public bool Equals(Job other)
        {
            return Name.Equals(other.Name);
        }

        // If Equals() returns true for a pair of objects  
        // then GetHashCode() must return the same value for these objects. 
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
