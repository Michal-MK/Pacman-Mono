using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame {
	public class CreepSpawn : Behaviour {

		public const string TEXTURE_ID = "creep_spawn";

		public override Vector2 Position { get; set; }

		public override Vector2 Scale { get; protected set; }

		public CreepSpawn(Vector2 position) {
			Setup(position, TEXTURE_ID);
		}

		public override void Draw(GameTime time, SpriteBatch batch) {
			Texture2D creepTex = Game.Sprites[TEXTURE_ID];
			batch.Draw(creepTex, Position, creepTex.Bounds, new Color(255,255,255,20) * 0.2f, 0, creepTex.Bounds.Center.ToVector2(), Scale, SpriteEffects.None, 0);
		}
	}
}