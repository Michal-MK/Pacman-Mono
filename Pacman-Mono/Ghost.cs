using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame {
	public class Ghost : GridAnimatedBehaviour {
		public override Vector2 Scale { get; }
		public override Vector2 Size { get; }

		public const string TEXTURE_ID = "ghost";

		public Ghost(Vector2 position) : base(World.Instance.SelectedWorld, World.Instance.GridCoordinates(position)) {
			Texture2D tx = Game.Sprites[TEXTURE_ID];
			float scaleX = World.Instance.CellSizeX / tx.Width;
			float scaleY = World.Instance.CellSizeY / tx.Height;
			Scale = new Vector2(scaleX, scaleY);
			Position = position + tx.Bounds.Center.ToVector2() * Scale;
			Size = new Vector2(tx.Width, tx.Height) * Scale;
		}

		public override void Update(GameTime time) {
			base.Update(time);
		}

		public override void Draw(GameTime time, SpriteBatch batch) {
			Texture2D wallTex = Game.Sprites[TEXTURE_ID];
			batch.Draw(wallTex, Position, wallTex.Bounds, Color.White, 0, wallTex.Bounds.Center.ToVector2(), Scale, SpriteEffects.None, 0);
		}
	}
}
