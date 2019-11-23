using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame {
	public class Creep : Behaviour {

		public const string TEXTURE_ID = "creep";
		public const int CREEP_SPREAD_PERIOD = 2000;

		public override Vector2 Position { get; set; }

		public override Vector2 Scale { get; protected set; }

		private readonly Graph pathGraph;
		private readonly List<CreepSpawn> spawns = new List<CreepSpawn>();

		public (int X, int Y) Start { get; set; }

		public Creep(Vector2 position) {
			Setup(position, TEXTURE_ID);
			pathGraph = WorldHelper.GenerateGraphOfOpenSpaces(World.Instance.SelectedWorld, World.Instance.GridCoordinates(position));
		}

		public override void Draw(GameTime time, SpriteBatch batch) {
			Texture2D creepTex = Game.Sprites[TEXTURE_ID];
			batch.Draw(creepTex, Position, creepTex.Bounds, Color.White, 0, creepTex.Bounds.Center.ToVector2(), Scale, SpriteEffects.None, 0);
			foreach (CreepSpawn sp in spawns) {
				sp.Draw(time, batch);
			}
		}

		public override void Update(GameTime time) {
			if ((int)time.TotalGameTime.TotalMilliseconds % CREEP_SPREAD_PERIOD == 0 && !pathGraph.FullyDiscovered) {
				Point[] newlyOccupied = pathGraph.IterateBFS();

				foreach (Point point in newlyOccupied) {
					CreepSpawn spawn = new CreepSpawn(World.Instance.WorldCoordinates(point));
					spawns.Add(spawn);
				}
			}
		}
	}
}
