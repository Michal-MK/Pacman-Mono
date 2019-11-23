using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame {
	public abstract class Behaviour {

		public abstract Vector2 Position { get; set; }

		public virtual Vector2 Size { get; protected set; }

		public virtual Vector2 Scale { get; protected set; }


		protected virtual void Setup(Vector2 position, string textureID) {
			Texture2D tx = Game.Sprites[textureID];
			float scaleX = World.Instance.CellSizeX / tx.Width;
			float scaleY = World.Instance.CellSizeY / tx.Height;
			Scale = new Vector2(scaleX, scaleY);

			Vector2 offset = tx.Bounds.Center.ToVector2() * Scale;
			Size = new Vector2(tx.Width, tx.Height) * Scale;

			Position = position + offset;
		}

		public virtual void Update(GameTime time) {
			//No need to update
		}

		public abstract void Draw(GameTime time, SpriteBatch batch);
	}
}
