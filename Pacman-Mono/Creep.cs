using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame {
	public class Creep : Behaviour {
		public override Vector2 Position { get; set; }

		public const string TEXTURE_ID = "creep";

		public (int X, int Y) Start { get; set; }

		public override Vector2 Size { get; }

		private Vector2 scaleVec;

		private Graph pathGraph;

		private List<CreepSpawn> spawns = new List<CreepSpawn>();

		public Creep(Vector2 position) {
			Texture2D creepTex = Game.Sprites[TEXTURE_ID];
			float scaleX = World.sizeX / creepTex.Width;
			float scaleY = World.sizeY / creepTex.Height;
			scaleVec = new Vector2(scaleX, scaleY);
			Size = new Vector2(creepTex.Width, creepTex.Height) * scaleVec;
			Position = position + Size / 2;
			pathGraph = WorldHelper.GenerateGraphOfOpenSpaces(World.Instance);
		}


		public override void Draw(GameTime time, SpriteBatch batch) {
			Texture2D creepTex = Game.Sprites[TEXTURE_ID];
			batch.Draw(creepTex, Position, creepTex.Bounds, Color.White, 0, creepTex.Bounds.Center.ToVector2(), scaleVec, SpriteEffects.None, 0);
			foreach (CreepSpawn sp in spawns) {
				sp.Draw(time, batch);
			}
		}

		public override void Update(GameTime time) {
			if ((int)time.TotalGameTime.TotalMilliseconds % 2000 == 0 && !pathGraph.FullyDiscovered) {
				Point[] newlyOccupied = pathGraph.IterateBFS();

				foreach (Point point in newlyOccupied) {
					CreepSpawn spawn = new CreepSpawn(World.Instance.WorldCoordinates(point));
					spawns.Add(spawn);
				}
			}
		}
	}
}
