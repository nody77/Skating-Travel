using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{
    public static class PROBLEM_CLASS
    {
        #region YOUR CODE IS HERE
        //Your Code is Here:
        //==================
        /// <summary>
        /// Ali Baba decides to go on a skating travel in the alpine mountain. He has stolen a pair of skis and a trail map listing 
        /// the mountain’s surfaces and slopes (n in total), and he wants to ski from surface S to surface T where a treasure is exists. 
        /// </summary>
        /// <param name="vertices">array of surfaces and their elevations </param>
        /// <param name="edges">array of trails and their lengths </param>
        /// <param name="startVertex">name of the start surface to begin from it</param>
        /// <returns>the minimum valid distance from source “S” to target “T”.</returns>
        public static int RequiredFunction(Dictionary<string, int> vertices, Dictionary<KeyValuePair<string, string>, int> edges, string startVertex)
        {
            //REMOVE THIS LINE BEFORE START CODING
            //throw new NotImplementedException();
            int shortest_path = 0, time = 0;
            
            Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>();
            Dictionary<string, int> discovery_time = new Dictionary<string, int>();
            Dictionary<string, int> finishing_time = new Dictionary<string, int>();
            Dictionary<string, int> color_vertex = new Dictionary<string, int>();
            Dictionary<string, int> distances = new Dictionary<string, int>();

            foreach(string vertex in vertices.Keys)
            {
                graph[vertex] = new List<string>();
                color_vertex[vertex] = 0;
                /*
                  white vertex --> 0
                  grey vertex --> 1
                  black vertex --> 2
                */
                distances[vertex] = int.MaxValue;
            }

            foreach(KeyValuePair<string, string> edge in edges.Keys)
            {
                if (vertices[edge.Key] >= vertices[edge.Value])
                {
                    graph[edge.Key].Add(edge.Value);
                }
                else if (vertices[edge.Key] < vertices[edge.Value])
                {
                    graph[edge.Value].Add(edge.Key);
                }
            }

            /*foreach (string vertex in graph.Keys)
            {
                if (color_vertex[vertex] == 0)
                {
                    DFS(vertex, ref time, ref color_vertex, ref finishing_time, ref discovery_time, graph);
                }
            }*/

            DFS(startVertex, ref time, ref color_vertex, ref finishing_time, ref discovery_time, graph);

            finishing_time = finishing_time.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);

            distances[startVertex] = 0;
            foreach(string vertex in finishing_time.Keys)
            {
                foreach(string adjacent_vertex in graph[vertex])
                {
                    KeyValuePair<string,string> edge_1 = new KeyValuePair<string,string>(vertex, adjacent_vertex);
                    KeyValuePair<string, string> edge_2 = new KeyValuePair<string, string>(adjacent_vertex, vertex);
                    if(edges.ContainsKey(edge_1))
                    {
                        if (distances[vertex] != int.MaxValue && distances[vertex] + edges[edge_1] < distances[adjacent_vertex])
                        {
                            distances[adjacent_vertex] = distances[vertex] + edges[edge_1];
                        }
                    }
                    else if (edges.ContainsKey(edge_2))
                    {
                        if (distances[vertex] != int.MaxValue && distances[vertex] + edges[edge_2] < distances[adjacent_vertex])
                        {
                            distances[adjacent_vertex] = distances[vertex] + edges[edge_2];
                        }
                    }
                    
                }
            }
            shortest_path = distances["T"];
            return shortest_path;
        }
        public static void DFS(string vertex, ref int time,ref Dictionary<string, int> color_vertex, ref Dictionary<string, int> finishing_time, ref Dictionary<string, int> discovery_time, Dictionary<string, List<string>> graph)
        {
            color_vertex[vertex] = 1;
            time += 1;
            discovery_time[vertex] = time;
            foreach(string adjacent_vertex in graph[vertex])
            {
                if (color_vertex[adjacent_vertex] == 0)
                { 
                    DFS(adjacent_vertex, ref time, ref color_vertex, ref finishing_time, ref discovery_time, graph);
                }
            }
            color_vertex[vertex] = 2;
            time += 1;
            finishing_time[vertex] = time;
        }
        #endregion
    }
}
