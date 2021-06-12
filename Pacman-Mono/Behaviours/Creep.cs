using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pacman.Behaviours.Base;
using Pacman.Structures;
using Pacman.World;

namespace Pacman.Behaviours {
	public class Creep : Behaviour {

		public const string TEXTURE_ID = "creep";
		private const int CREEP_SPREAD_PERIOD = 2000;

		public override Vector2 Position { get; set; }

		protected override Vector2 Scale { get; set; }

		private readonly Graph pathGraph;
		private readonly List<CreepSpawn> spawns = new();

		public Creep(Vector2 position) {
			Setup(position, TEXTURE_ID);
			pathGraph = WorldHelper.GenerateGraphOfOpenSpaces(GameWorld.Instance.SelectedWorld, GameWorld.Instance.GridCoordinates(position));
		}

		public override void Draw(GameTime time, SpriteBatch batch) {
			SimpleDraw(time, batch);
			foreach (CreepSpawn sp in spawns) {
				sp.Draw(time, batch);
			}
		}

		public override void Update(GameTime time) {
			if ((int)time.TotalGameTime.TotalMilliseconds % CREEP_SPREAD_PERIOD != 0 || pathGraph.FullyDiscovered) return;

			Point[] newlyOccupied = pathGraph.IterateBFS();

			foreach (Point point in newlyOccupied) {
				CreepSpawn spawn = new(GameWorld.Instance.WorldCoordinates(point));
				spawns.Add(spawn);
			}
		}
	}
}
