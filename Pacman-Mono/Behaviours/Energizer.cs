using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Behaviours.Base;

namespace MonoGame.Behaviours {
	public class Energizer : Behaviour {
		public override Vector2 Position { get; set; }

		protected override Vector2 Scale { get; set; }

		public const string TEXTURE_ID = "energizer";

		public Energizer(Vector2 position) {
			Setup(position, TEXTURE_ID);
		}

		public override void Draw(GameTime time, SpriteBatch batch) => SimpleDraw(time, batch);
	}
}
