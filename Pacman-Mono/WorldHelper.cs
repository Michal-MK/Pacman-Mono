using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGame {
	public class WorldHelper {
		public static Graph GenerateGraphOfOpenSpaces(World instance) {
			return new Graph(WorldDefinitions.LARGE_WORLD_19x19, new Point(7,1));
		}
	}
}
