using JobsOrderer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JobsOrderer.InputOrdering.Algorithms
{
    public class TopologicalSortAlgorithm : ISortAlgorithm
    {
        public TopologicalSortAlgorithm()
        {

        }

        public List<Job> Sort(List<Job> jobs)
        {
            var nodes = new HashSet<Job>();
            var edges = new HashSet<Tuple<Job, Job>>();

            // creates the nodes and edges based on the list of jobs 
            foreach (var job in jobs)
            {
                nodes.Add(job);
                foreach (var dependency in job.Dependencies)
                    edges.Add(Tuple.Create(dependency, job)); // order is a bit confusing, in a graph you represent dependency -> job, whereas the exercise syntax is job => dependency
            }

            // use the nodes and edges to perform a topological sort on the nodes and edges
            return TopologicalSort(nodes, edges);
        }


        // Kahn's algorithm, found here https://gist.github.com/Sup3rc4l1fr4g1l1571c3xp14l1d0c10u5/3341dba6a53d7171fe3397d13d00ee3f
        // Direct implementation of https://en.wikipedia.org/wiki/Topological_sorting#Kahn.27s_algorithm
        private List<T> TopologicalSort<T>(HashSet<T> nodes, HashSet<Tuple<T, T>> edges) where T : IEquatable<T>
        {
            // Empty list that will contain the sorted elements
            var L = new List<T>();

            // Set of all nodes with no incoming edges
            var S = new HashSet<T>(nodes.Where(n => edges.All(e => e.Item2.Equals(n) == false)));

            // while S is non-empty do
            while (S.Any())
            {

                //  remove a node n from S
                var n = S.First();
                S.Remove(n);

                // add n to tail of L
                L.Add(n);

                // for each node m with an edge e from n to m do
                foreach (var e in edges.Where(e => e.Item1.Equals(n)).ToList())
                {
                    var m = e.Item2;

                    // remove edge e from the graph
                    edges.Remove(e);

                    // if m has no other incoming edges then
                    if (edges.All(me => me.Item2.Equals(m) == false))
                    {
                        // insert m into S
                        S.Add(m);
                    }
                }
            }

            // if graph has edges then
            if (edges.Any())
            {
                // throw error (graph has at least one cycle)
                throw new Exception("Error: There is atleast one cyclic dependency");
            }

            // return L (a topologically sorted order)
            return L;
        }
    }
}
