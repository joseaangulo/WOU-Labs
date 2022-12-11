//Jose Angulo

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

    public class AdjacencyMatrix : IGraph
    {
        public char[] _vertices;
        public string[] _edges;
        public int[,] _adjacencyMatrix;

        public AdjacencyMatrix(char[] vertices, string[] edges)
        {
            _vertices = vertices;
            _edges = edges;
            _adjacencyMatrix = new int[vertices.Length, vertices.Length];
            for (int i = 0; i < _vertices.Length; i++)
            {
                for (int j = 0; j < _vertices.Length; j++)
                {
                    _adjacencyMatrix[i, j] = 0;
                }
            }

            foreach (var edge in _edges)
            {
                _adjacencyMatrix[Convert.ToInt32(edge.ElementAt(0) % 97), Convert.ToInt32(edge.ElementAt(1) % 97)] = 1;
                _adjacencyMatrix[Convert.ToInt32(edge.ElementAt(1) % 97), Convert.ToInt32(edge.ElementAt(0) % 97)] = 1;
            }
        }

        public void Print()
        {
            Console.WriteLine("Adjacency Matrix: ");
            Console.Write("  ");
            foreach (var vertice in _vertices)
            {
                Console.Write($"{vertice} ");
            }

            Console.WriteLine();
            for (int i = 0; i < _vertices.Length; i++)
            {
                Console.Write($"{_vertices[i]} ");
                for (int j = 0; j < _vertices.Length; j++)
                {
                    Console.Write($"{_adjacencyMatrix[i, j]} ");
                }

                Console.WriteLine();
            }
        }

        public char[] getVertex()
        {
            return _vertices;
        }

        public string[] getEdges()
        {
            return _edges;
        }
    }

    public class AdjacencyList : IGraph
    {
        public char[] _vertices;
        public string[] _edges;
        public List<List<char>> _adjacencyList;

        public AdjacencyList(char[] vertices, string[] edges)
        {
            _vertices = vertices;
            _edges = edges;
            _adjacencyList = new List<List<char>>();
          
            for (int j = 0; j < _vertices.Length; j++)
            {
                var vertex = new List<char>();
                for (int i = 0; i < _vertices.Length; i++)
                {
                    if (_edges[i].ElementAt(0) % 97 == j)
                    {
                        vertex.Add(_edges[i].ElementAt(1));
                    }

                    if (_edges[i].ElementAt(1) % 97 == j)
                    {
                        vertex.Add(_edges[i].ElementAt(0));
                    }

                }

                _adjacencyList.Add(vertex);
            }
        }

        public void Print()
        {
            Console.WriteLine("Adjacency List");
            foreach (var list in _adjacencyList)
            {
                if (list.Count > 0)
                {
                    list.Sort();
                    Console.Write((char)(_adjacencyList.IndexOf(list) + 97));
                    for (int i = 0; i < list.Count; i++)
                    {
                        Console.Write($" -> {list[i]}");
                    }
                }

                Console.WriteLine();
            }
        }

        public char[] getVertex()
        {
            return _vertices;
        }

        public string[] getEdges()
        {
            return _edges;
        }


    }

    public class Node
    {
        public char _name;
        public List<Node> _children;

        public Node(char name)
        {
            _name = name;
            _children = new List<Node>();
        }

        /*public Node(char name, List<Node> children)
        {
            _name = name;
            _children = children;
        }*/

        public void AddChild(Node newNode)
        {
            _children.Add(newNode);
        }

        public char getName()
        {
            return _name;
        }

        public List<Node> getChildren()
        {
            return _children;
        }
    }
    public class AdjacencyListGraph : IGraph
    {
        public char[] _vertices;
        public string[] _edges;
        public List<Node> _nodes = new List<Node>();

        public AdjacencyListGraph(char[] vertices, string[] edges)
        {
            _vertices = vertices;
            _edges = edges;
            

            foreach (var vertex in _vertices)
            {
                Node newNode = new Node(vertex);
                _nodes.Add(newNode);
            }

            foreach (var edge in edges)
            {
                _nodes[edge.ElementAt(0) % 97].AddChild(_nodes[edge.ElementAt(1) % 97]);
                _nodes[edge.ElementAt(1) % 97].AddChild(_nodes[edge.ElementAt(0) % 97]);
            }
        }

        public void Print()
        {
            foreach (var node in _nodes)
            {
                Console.Write($"{node.getName()}");
                List<Node> childrenOfNode = node.getChildren();
                foreach (var child in childrenOfNode)
                {
                    Console.Write($" -> {child._name}");
                }
                Console.WriteLine();
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

    public class BFS
    {
        private List<string> _color = new List<string>();
        private List<double> _distance = new List<double>(5);
        private List<double> _pi = new List<double>(5);
        public List<Node> _adjacenyList;
        public char _source;

        public BFS(AdjacencyListGraph adjList, char source)
        {
            _adjacenyList = adjList._nodes;
            _source = source;
            foreach (var vertex in adjList._vertices)
            {
                _color.Add("white");
                _distance.Add(double.PositiveInfinity);
                _pi.Add(double.NaN);
            }

            _color[source % 97] = "gray";
            _distance[source % 97] = 0;
            _pi[source % 97] = double.NaN;

            Queue queue = new Queue();
            queue.Enqueue(source);

            while (queue.Count > 0)
            {
                char theVertex = (char) queue.Dequeue();
                foreach (var v in _adjacenyList[theVertex % 97]._children)
                {
                    if (_color[v._name % 97] == "white")
                    {
                        _color[v._name % 97] = "gray";
                        _distance[v._name % 97] = _distance[theVertex % 97] + 1;
                        _pi[v._name % 97] = theVertex % 97;
                        queue.Enqueue(v._name);
                    }
                }

                _color[theVertex % 97] = "black";
            }

        }

        public void Print()
        {
            Console.WriteLine($"BFS D array for {_source}");
            for (int i = 0; i < _distance.Count; i++)
            {
                Console.WriteLine($"{_adjacenyList[i].getName()} : {_distance[i]}");
            }
            Console.WriteLine();
        }
    }

    public class DFS
    {
        private List<string> _color = new List<string>();
        private List<int> _discovered = new List<int>(5);
        private List<double> _pi = new List<double>(5);
        private List<int> _finished = new List<int>(5);
        private int time;
        public List<Node> _adjacenyList;

        public DFS(AdjacencyListGraph adjacencyList)
        {
            _adjacenyList = adjacencyList._nodes;

            foreach (var vertex in adjacencyList._vertices)
            {
                _color.Add("white");
                _discovered.Add(-1);
                _pi.Add(double.NaN);
                _finished.Add(-1);
            }

            time = 0;

            foreach (var node in _adjacenyList)
            {
                if (_color[node.getName() % 97] == "white")
                {
                    DFSVisit(node);
                }
            }

        }

        public void DFSVisit(Node myNode)
        {
            _color[myNode._name % 97] = "gray";
            ++time;
            _discovered[myNode._name % 97] = time;

            foreach (var node in myNode._children)
            {
                if (_color[node._name % 97] == "white")
                {
                    _pi[node._name % 97] = myNode._name % 97;
                    DFSVisit(node);
                }
            }
            _color[myNode._name % 97] = "black";
            _finished[myNode.getName() % 97] = ++time;
        }

        public void Print()
        {
            Console.WriteLine("The results of DFS array d");
            for (int i = 0; i < _discovered.Count; i++)
            {
                Console.WriteLine($"{_adjacenyList[i].getName()} : {_discovered[i]}");
            }

            Console.WriteLine("The results of DFS array f");
            for (int i = 0; i < _finished.Count; i++)
            {
                Console.WriteLine($"{_adjacenyList[i].getName()} : {_finished[i]}");
            }
        }

    }



    class Program
    {
        static void Main(string[] args)
        {
            AdjacencyList ConvertFromAdjacencyMatrixToAdjacencyList(AdjacencyMatrix myMatrix)
            {
                AdjacencyList myList = new AdjacencyList(myMatrix.getVertex(), 
                    myMatrix.getEdges()); 
                return myList;
            }
            AdjacencyMatrix ConvertFromAdjacencyListToAdjacencyMatrix(AdjacencyList myList)
            {
                AdjacencyMatrix myMatrix = new AdjacencyMatrix(myList.getVertex(), 
                    myList.getEdges());
                return myMatrix;
            }



            char[] vertices = {'a', 'b', 'c', 'd', 'e'};
            string[] edges = {"ab", "ac", "bd", "cd", "de"};

            /*Console.WriteLine("Problem 1: ");*/
            //AdjacencyMatrix myMatrix = new AdjacencyMatrix(vertices, edges);
            //myMatrix.Print();

            /*Console.WriteLine("Problem 2: Converted From Matrix");
            myMatrix.Print();
            Console.WriteLine("To a list.");
            AdjacencyList convertedList = ConvertFromAdjacencyMatrixToAdjacencyList(myMatrix);
            convertedList.Print();

            Console.WriteLine("Problem 3: Converted From a List");
            myList.Print();
            Console.WriteLine("To a Matrix");
            AdjacencyMatrix convertedMatrix = ConvertFromAdjacencyListToAdjacencyMatrix(myList);
            convertedMatrix.Print();*/

            AdjacencyListGraph myGraph = new AdjacencyListGraph(vertices, edges);
            myGraph.Print();
            AdjacencyList myList = new AdjacencyList(vertices, edges);
            myList.Print();
            Console.WriteLine("Problem 4: ");
            BFS myBFS = new BFS(myGraph, 'a');
            myBFS.Print();
            Console.WriteLine("Problem 5: ");
            DFS myDFS = new DFS(myGraph);
            myDFS.Print();
        }
    }
}