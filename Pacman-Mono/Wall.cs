using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame {
	public class Wall : Behaviour {
		public const string TEXTURE_ID = "wall";

		public override Vector2 Position { get; set; }

		public override Vector2 Size { get; }
		public override Vector2 Scale { get; }

		public Wall(Vector2 center) {
			Texture2D wallTex = Game.Sprites[TEXTURE_ID];
			float scaleX = World.Instance.CellSizeX / wallTex.Width;
			float scaleY = World.Instance.CellSizeY / wallTex.Height;
			Scale = new Vector2(scaleX, scaleY);
			Position = center + wallTex.Bounds.Center.ToVector2() * Scale;
			Size = new Vector2(wallTex.Width, wallTex.Height) * Scale;
		}

		public override void Update(GameTime time) {
			//No need to update
		}

		public override void Draw(GameTime time, SpriteBatch batch) {
			Texture2D wallTex = Game.Sprites[TEXTURE_ID];
			batch.Draw(wallTex, Position, wallTex.Bounds, Color.White, 0, wallTex.Bounds.Center.ToVector2(), Scale, SpriteEffects.None, 0);
		}
	}
}
