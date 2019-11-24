using Microsoft.Xna.Framework;

namespace MonoGame {
	public class WorldHelper {
		public static Graph GenerateGraphOfOpenSpaces(char[,] world, Point start) {
			return new Graph(world, start);
		}
	}
}
