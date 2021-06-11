using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Behaviours.Base;

namespace MonoGame.Behaviours {
	public class CreepSpawn : Behaviour {

		public const string TEXTURE_ID = "creep_spawn";

		public override Vector2 Position { get; protected set; }

		protected override Vector2 Scale { get; set; }

		public CreepSpawn(Vector2 position) {
			Setup(position, TEXTURE_ID);
		}

		public override void Draw(GameTime time, SpriteBatch batch) => SimpleDraw(time, batch, new Color(255, 255, 255, 20) * 0.4f);
	}
}