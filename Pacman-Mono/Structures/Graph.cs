using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MonoGame.Structures {
	public class Graph {

		public Graph(char[,] world, Point start) {
			visited = new bool[world.GetLength(0), world.GetLength(1)];
			graphData = new bool[world.GetLength(0), world.GetLength(1)];

			for (int y = 0; y < world.GetLength(1); y++) {
				for (int x = 0; x < world.GetLength(0); x++) {
					graphData[x, y] = world[x, y] != 'W';
				}
			}

			visited[start.Y, start.X] = true;
			toGo.Enqueue(start);
		}

		private readonly bool[,] visited;
		private readonly bool[,] graphData;
		private readonly Queue<Point> toGo = new Queue<Point>();

		public bool FullyDiscovered { get; private set; }

		public Point[] IterateBFS() {
			List<Point> ret = new List<Point>();

			Queue<Point> toGoCopy = new Queue<Point>(toGo);
			toGo.Clear();

			while (toGoCopy.Count != 0) {
				Point p = toGoCopy.Dequeue();
				Point[] emptyNeighbors = GetEmptyNeighbors(p);
				foreach (Point newlyFound in emptyNeighbors) {
					visited[newlyFound.Y, newlyFound.X] = true;
					toGo.Enqueue(newlyFound);
					ret.Add(newlyFound);
				}
			}

			FullyDiscovered = toGo.Count == 0;
			return ret.ToArray();
		}

		private Point[] GetEmptyNeighbors(Point p) {
			List<Point> points = new List<Point>();

			int xL = p.X - 1;
			int xR = p.X + 1;
			int yB = p.Y + 1;
			int yT = p.Y - 1;

			if (xL >= 0 && graphData[p.Y, xL] && !visited[p.Y, xL]) {
				points.Add(new Point(xL, p.Y));
			}
			if (xR < visited.GetLength(0) && graphData[p.Y, xR] && !visited[p.Y, xR]) {
				points.Add(new Point(xR, p.Y));
			}
			if (yT >= 0 && graphData[yT, p.X] && !visited[yT, p.X]) {
				points.Add(new Point(p.X, yT));
			}
			if (yB <= visited.GetLength(1) && graphData[yB, p.X] && !visited[yB, p.X]) {
				points.Add(new Point(p.X, yB));
			}

			return points.ToArray();
		}

		public Point[] GetEmptyNeighborsIgnoreVisited(Point p) {
			List<Point> points = new List<Point>();

			int xL = p.X - 1;
			int xR = p.X + 1;
			int yB = p.Y + 1;
			int yT = p.Y - 1;

			if (xL >= 0 && graphData[p.Y, xL]) {
				points.Add(new Point(xL, p.Y));
			}
			if (xR < visited.GetLength(0) && graphData[p.Y, xR]) {
				points.Add(new Point(xR, p.Y));
			}
			if (yT >= 0 && graphData[yT, p.X]) {
				points.Add(new Point(p.X, yT));
			}
			if (yB <= visited.GetLength(1) && graphData[yB, p.X]) {
				points.Add(new Point(p.X, yB));
			}

			return points.ToArray();
		}


		public void PPrint() {
			int ndDimLength = visited.GetLength(1);
			Console.WriteLine(new string('-', 40));

			Console.WriteLine("Data:");
			for (int i = 0; i < graphData.GetLength(0); i++) {
				Console.Write('[');
				for (int j = 0; j < ndDimLength; j++) {
					Console.Write(graphData[i, j] + (ndDimLength - 1 != j ? ", " : ""));
				}
				Console.WriteLine(']');
			}
			Console.WriteLine(new string('-', 60));
			Console.WriteLine("Visited:");
			for (int i = 0; i < visited.GetLength(0); i++) {
				Console.Write('[');
				for (int j = 0; j < ndDimLength; j++) {
					Console.Write(visited[i, j] + (ndDimLength - 1 != j ? ", " : ""));
				}
				Console.WriteLine(']');
			}
			Console.WriteLine(new string('-', 40));
		}

		public void PPrint<T>(T[,] arr) {
			Console.WriteLine(new string('-', 40));
			for (int i = 0; i < arr.GetLength(0); i++) {
				Console.Write('[');
				for (int j = 0; j < arr.GetLength(1); j++) {
					Console.Write(arr[i, j] + (arr.GetLength(1) - 1 != j ? ", " : ""));
				}
				Console.WriteLine(']');
			}
			Console.WriteLine(new string('-', 40));
		}
	}
}