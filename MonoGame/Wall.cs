using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame {
	public class Wall : Behaviour {
		public const string TEXTURE_ID = "wall";

		public override Vector2 Position { get; set; }
		public Vector2 Size { get; }

		private Vector2 scaleVec;
		public Wall(Vector2 center) {
			Texture2D wallTex = Game.Sprites[TEXTURE_ID];
			float scaleX = World.sizeX / wallTex.Width;
			float scaleY = World.sizeY / wallTex.Height;
			scaleVec = new Vector2(scaleX, scaleY);
			Position = center + wallTex.Bounds.Center.ToVector2() * scaleVec;
			Size = new Vector2(wallTex.Width, wallTex.Height) * scaleVec;
		}

		public override void Update(GameTime time) {

		}

		public override void Draw(GameTime time, SpriteBatch batch) {
			Texture2D wallTex = Game.Sprites[TEXTURE_ID];
			batch.Draw(wallTex, Position, wallTex.Bounds, Color.White, 0, wallTex.Bounds.Center.ToVector2(), scaleVec, SpriteEffects.None, 0);
		}
	}
}
