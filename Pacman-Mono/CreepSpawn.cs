using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame {
	public class CreepSpawn : Behaviour {

		public const string TEXTURE_ID = "creep";

		public CreepSpawn(Vector2 position) {
			Texture2D creepTex = Game.Sprites[TEXTURE_ID];
			float scaleX = World.sizeX / creepTex.Width;
			float scaleY = World.sizeY / creepTex.Height;
			scaleVec = new Vector2(scaleX, scaleY);
			Size = new Vector2(creepTex.Width, creepTex.Height) * scaleVec;
			Position = position + Size / 2;
		}

		public override Vector2 Position { get; set; }
		public override Vector2 Size { get; }
		private Vector2 scaleVec;

		public override void Draw(GameTime time, SpriteBatch batch) {
			Texture2D creepTex = Game.Sprites[TEXTURE_ID];
			batch.Draw(creepTex, Position, creepTex.Bounds, new Color(100,100,0,50), 0, creepTex.Bounds.Center.ToVector2(), scaleVec, SpriteEffects.None, 0);
		}

		public override void Update(GameTime time) {
			
		}
	}
}