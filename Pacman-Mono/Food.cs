using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame {
	public class Food : Behaviour {

		public const string TEXTURE_ID = "food";

		private Vector2 scaleVec;

		public Food(Vector2 position) {
			Texture2D foodTex = Game.Sprites[TEXTURE_ID];
			float scaleX = World.sizeX / 256;
			float scaleY = World.sizeY / 256;
			scaleVec = new Vector2(scaleX, scaleY);
			Size = new Vector2(foodTex.Width, foodTex.Height) * scaleVec;
			Position = position + Size * 2;
		}

		public override Vector2 Position { get; set; }

		public override Vector2 Size { get; }

		public override void Update(GameTime time) {
			//NONE
		}

		public override void Draw(GameTime time, SpriteBatch batch) {
			Texture2D wallTex = Game.Sprites[TEXTURE_ID];
			batch.Draw(wallTex, Position, wallTex.Bounds, Color.White, 0, wallTex.Bounds.Center.ToVector2(), scaleVec, SpriteEffects.None, 0);
		}
	}
}
