using Microsoft.Xna.Framework;
using Pacman.Structures;

namespace Pacman.World {
	public static class WorldHelper {
		public static Graph GenerateGraphOfOpenSpaces(char[,] world, Point start) {
			return new Graph(world, start);
		}
	}
}
