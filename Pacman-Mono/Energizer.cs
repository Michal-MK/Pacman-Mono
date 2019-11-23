using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame {
	public class Energizer : Behaviour {
		public override Vector2 Position { get; set; }

		public override Vector2 Scale { get; protected set; }

		public const string TEXTURE_ID = "energizer";

		public Energizer(Vector2 position) {
			Setup(position, TEXTURE_ID);
		}

		public override void Draw(GameTime time, SpriteBatch batch) {
			Texture2D shape = Game.Sprites[TEXTURE_ID];
			batch.Draw(shape, Position, shape.Bounds, Color.White, 0, shape.Bounds.Center.ToVector2(), Scale, SpriteEffects.None, 0);

		}
	}
}
