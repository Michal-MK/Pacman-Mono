using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame {
	public class Wall : Behaviour {
		public const string TEXTURE_ID = "wall";

		public override Vector2 Position { get; set; }

		public override Vector2 Scale { get; protected set; }

		public Wall(Vector2 position) {
			Setup(position, TEXTURE_ID);
		}

		public override void Draw(GameTime time, SpriteBatch batch) {
			Texture2D wallTex = Game.Sprites[TEXTURE_ID];
			batch.Draw(wallTex, Position, wallTex.Bounds, Color.White, 0, wallTex.Bounds.Center.ToVector2(), Scale, SpriteEffects.None, 0);
		}
	}
}
