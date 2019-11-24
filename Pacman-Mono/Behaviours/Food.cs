using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame {
	public class Food : Behaviour {

		public const string TEXTURE_ID = "food";

		public override Vector2 Position { get; set; }

		public override Vector2 Scale { get; protected set; }

		public Food(Vector2 position) {
			Setup(position, TEXTURE_ID);
		}

		public override void Draw(GameTime time, SpriteBatch batch) => SimpleDraw(time, batch, TEXTURE_ID, Color.White);
	}
}
