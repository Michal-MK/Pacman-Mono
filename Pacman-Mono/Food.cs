using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame {
	public class Food : Behaviour {

		public const string TEXTURE_ID = "food";

		public override Vector2 Position { get; set; }

		public override Vector2 Scale { get; }
		public override Vector2 Size { get; }

		public Food(Vector2 position) {
			Texture2D foodTex = Game.Sprites[TEXTURE_ID];
			float scaleX = World.CellSizeX / 256;
			float scaleY = World.CellSizeY / 256;
			Scale = new Vector2(scaleX, scaleY);
			Size = new Vector2(foodTex.Width, foodTex.Height) * Scale;
			Position = position + Size * 2;
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
