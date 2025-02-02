﻿//Jose Angulo

using System;
using System.Collections;
using System.Numerics;

namespace Default
{
    //A Simple Program 
    public interface IGraph
    {
        public void Print();
        public char[] getVertex();
        public string[] getEdges();

    }

    public class NodeWeighted
    {
        public SingleNode _myNode;
        public List<Tuple<SingleNode, int>> _children;

        public NodeWeighted(char name)
        {//Make pass in node, make a tuple with the node and weight for children not node
            _myNode = new SingleNode(name);
            _children = new List<Tuple<SingleNode, int>>();
        }

        /*public Node(char name, List<Node> children)
        {
            _name = name;
            _children = children;
        }*/

        public void AddChild(SingleNode newNode, int weight)
        {
            _children.Add(new Tuple<SingleNode, int>(newNode, weight));
        }


        public List<Tuple<SingleNode, int>> getChildren()
        {
            return _children;
        }
    }

    public class SingleNode
    {
        public char _name;

        public SingleNode(char name)
        {
            _name = name;
        }
        public char getName()
        {
            return _name;
        }
    }

    public class AdjacencyListGraphWeightedAndDirected : IGraph
    {
        public char[] _vertices;
        public string[] _edges;
        public int[] _weightsOnEdges;
        public List<NodeWeighted> _nodesWeighted = new List<NodeWeighted>();

        public AdjacencyListGraphWeightedAndDirected(char[] vertices, string[] edges, int[] weightsOnEdges)
        {
            _vertices = vertices;
            _edges = edges;
            _weightsOnEdges = weightsOnEdges;


            foreach (var vertex in _vertices)
            {
                NodeWeighted newNodeWeighted = new NodeWeighted(vertex);
                _nodesWeighted.Add(newNodeWeighted);
            }


            for (int i = 0; i < _edges.Length; i++)
            {
                _nodesWeighted[_edges[i].ElementAt(0) % 97].AddChild(_nodesWeighted[_edges[i].ElementAt(1) % 97]._myNode, _weightsOnEdges[i]);
            }
        }

        public void Print()
        {
            foreach (var node in _nodesWeighted)
            {

                List<Tuple<SingleNode, int>> childrenOfNode = node.getChildren();
                foreach (var child in childrenOfNode)
                {
                    Console.Write($"{node._myNode._name}");
                    Console.Write($" -> {child.Item1._name}, with weight {child.Item2}");
                    Console.WriteLine();
                }

            }
        }

        public char[] getVertex()
        {
            throw new NotImplementedException();
        }

        public string[] getEdges()
        {
            throw new NotImplementedException();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            //I seperated weights from edges, but used them together when initializing
            char[] vertices = { 'a', 'b', 'c', 'd', 'e' };
            string[] edges = { "ab", "bc", "ae", "eb", "be", "ec", "ed", "dc", "cd"};
            int[] weights = { 10, 2, 3, 1, 4, 8, 2, 7, 9 };

            char mindistance(Queue queue, List<double> distance)
            {
                //Take first element
                char min = (char)queue.Peek();

                //Compare with rest and give back vertex that is minimum
                //Also empties queue
                while (queue.Count > 0)
                {
                    char place = (char)queue.Dequeue();
                    if (distance[place % 97] < distance[min % 97])
                    {
                        min = place;
                    }
                }
                return min;
            }

            void Dijkstra(AdjacencyListGraphWeightedAndDirected myList, char source)
            {
                List<double> distance = new List<double>(myList._vertices.Length);
                List<char> pi = new List<char>(myList._vertices.Length);
                List<bool> visited = new List<bool>(myList._vertices.Length);

                //Initialized distance, previous, and visited arrays
                foreach (var vertex in myList._vertices)
                {
                    distance.Add(-1);
                    pi.Add(source);
                    visited.Add(false);
                }
                //Distance of source on array set to zero
                distance[source % 97] = 0;

                //sets all other vertices distance to infiniti
                foreach (var vertex in myList._vertices)
                {
                    if (vertex != source)
                    {
                        distance[vertex % 97] = double.PositiveInfinity;
                    }
                }
                //New queue
                Queue queue = new Queue();
                
                //Add all vertices to queue
                foreach (var vertex in myList._vertices)
                {
                    queue.Enqueue(vertex);
                }
                //Initial State
                Console.WriteLine("Distance from source: ");

                for (int i = 0; i < vertices.Length; i++)
                {
                    Console.WriteLine($"{vertices[i]} cost {distance[i]}");
                }

                //Go through the queue
                while (queue.Count > 0)
                {
                    //returns minimum vertice that has not been visited
                    char minimumElement = mindistance(queue, distance);

                    //marks newest minimum vertice to visited
                    visited[minimumElement % 97] = true;

                    //resets queue to only have vertices not visited
                    for (int i = 0; i < vertices.Length; i++)
                    {
                        if (visited[i] == false)
                            queue.Enqueue(vertices[i]);
                    }

                    //Updates all vertices neighbors with new value if possible
                    foreach (var child in myList._nodesWeighted[minimumElement % 97]._children)
                    {
                        //checks to see if the new distance is shorter
                        if (distance[child.Item1._name % 97] > (distance[minimumElement % 97] + child.Item2))
                        {
                            //Sets new distance and previous vertex
                            distance[child.Item1._name % 97] = distance[minimumElement % 97] + child.Item2;
                            pi[child.Item1._name % 97] = minimumElement;
                            
                            Console.WriteLine("Distance from source: ");

                            for (int i = 0; i < vertices.Length; i++)
                            {
                                Console.WriteLine($"{vertices[i]} cost {distance[i]}");
                            }

                        }

                    }

                }

                Console.WriteLine("Distance from source: ");

                for (int i = 0; i < vertices.Length; i++)
                {
                    Console.WriteLine($"{vertices[i]} cost {distance[i]}");
                }
                Console.WriteLine("Vertice was from: ");

                for (int i = 0; i < vertices.Length; i++)
                {
                    Console.WriteLine($"{vertices[i]} was from {pi[i]}");
                }
            }

            AdjacencyListGraphWeightedAndDirected newGraph =
                new AdjacencyListGraphWeightedAndDirected(vertices, edges, weights);

            newGraph.Print();

            Dijkstra(newGraph, 'a');

                //I didn't see a graph on slide 16
        }
    }
}