//Jose Angulo

using System;
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
        private int[,] _adjacencyMatrix;

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
        private List<List<char>> _adjacencyList;

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

            Console.WriteLine("Problem 1: ");
            AdjacencyMatrix myMatrix = new AdjacencyMatrix(vertices, edges);
            myMatrix.Print();

            AdjacencyList myList = new AdjacencyList(vertices, edges);
            myList.Print();

            Console.WriteLine("Problem 2: Converted From Matrix");
            myMatrix.Print();
            Console.WriteLine("To a list.");
            AdjacencyList convertedList = ConvertFromAdjacencyMatrixToAdjacencyList(myMatrix);
            convertedList.Print();

            Console.WriteLine("Problem 3: Converted From a List");
            myList.Print();
            Console.WriteLine("To a Matrix");
            AdjacencyMatrix convertedMatrix = ConvertFromAdjacencyListToAdjacencyMatrix(myList);
            convertedMatrix.Print();
        }
    }
}