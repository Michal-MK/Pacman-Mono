using Microsoft.Xna.Framework;
using MonoGame.Structures;

namespace MonoGame.World {
	public static class WorldHelper {
		public static Graph GenerateGraphOfOpenSpaces(char[,] world, Point start) {
			return new Graph(world, start);
		}
	}
}
