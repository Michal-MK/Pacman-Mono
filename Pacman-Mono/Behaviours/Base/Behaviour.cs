using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pacman.World;

namespace Pacman.Behaviours.Base {
	public abstract class Behaviour {

		public abstract Vector2 Position { get; set; }

		public virtual Vector2 Size { get; protected set; }

		protected virtual Vector2 Scale { get; set; }

		private string texture;


		protected virtual void Setup(Vector2 position, string textureID) {
			Texture2D tx = Main.Sprites[textureID];
			float scaleX = GameWorld.Instance.CellSizeX / tx.Width;
			float scaleY = GameWorld.Instance.CellSizeY / tx.Height;
			Scale = new Vector2(scaleX, scaleY);

			Vector2 offset = tx.Bounds.Center.ToVector2() * Scale;
			Size = new Vector2(tx.Width, tx.Height) * Scale;

			Position = position + offset;
			texture = textureID;
		}

		public virtual void Update(GameTime time) {
			//No need to update
		}

		public abstract void Draw(GameTime time, SpriteBatch batch);

		protected void SimpleDraw(GameTime time, SpriteBatch batch, string textureID = null) {
			SimpleDraw(time, batch, Color.White, textureID);
		}

		protected void SimpleDraw(GameTime _, SpriteBatch batch, Color color, string textureID = null) {
			Texture2D tex = Main.Sprites[textureID ?? texture];
			batch.Draw(tex, Position, tex.Bounds, color, 0, tex.Bounds.Center.ToVector2(), Scale, SpriteEffects.None, 0);
		}
	}
}
