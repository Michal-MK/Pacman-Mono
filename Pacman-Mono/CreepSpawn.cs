using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame {
	public class CreepSpawn : Behaviour {

		public const string TEXTURE_ID = "creep";

		public override Vector2 Position { get; set; }

		public override Vector2 Size { get; }
		public override Vector2 Scale { get; }

		public CreepSpawn(Vector2 position) {
			Texture2D creepTex = Game.Sprites[TEXTURE_ID];
			float scaleX = World.Instance.CellSizeX / creepTex.Width;
			float scaleY = World.Instance.CellSizeY / creepTex.Height;
			Scale = new Vector2(scaleX, scaleY);
			Size = new Vector2(creepTex.Width, creepTex.Height) * Scale;
			Position = position + Size / 2;
		}

		public override void Update(GameTime time) {
			//No need to update
		}

		public override void Draw(GameTime time, SpriteBatch batch) {
			Texture2D creepTex = Game.Sprites[TEXTURE_ID];
			batch.Draw(creepTex, Position, creepTex.Bounds, new Color(255,255,255,20) * 0.2f, 0, creepTex.Bounds.Center.ToVector2(), Scale, SpriteEffects.None, 0);
		}
	}
}