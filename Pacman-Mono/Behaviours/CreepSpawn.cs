using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame {
	public class CreepSpawn : Behaviour {

		public const string TEXTURE_ID = "creep_spawn";

		public override Vector2 Position { get; set; }

		public override Vector2 Scale { get; protected set; }

		public CreepSpawn(Vector2 position) {
			Setup(position, TEXTURE_ID);
		}

		public override void Draw(GameTime time, SpriteBatch batch) => SimpleDraw(time, batch, TEXTURE_ID, new Color(255, 255, 255, 20) * 0.4f);
	}
}