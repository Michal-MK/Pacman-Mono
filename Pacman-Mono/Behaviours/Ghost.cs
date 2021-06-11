using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Behaviours.Base;

namespace MonoGame.Behaviours {
	public class Ghost : GridAnimatedBehaviour {
		protected override Vector2 Scale { get; set; }

		public Color Tint { get; set; } = Color.White;

		public const string TEXTURE_ID_SHAPE = "ghost/ghost_shape";
		public const string TEXTURE_ID_EYES = "ghost/ghost_eyes";

		public Ghost(Vector2 position) : base(World.GameWorld.Instance.SelectedWorld, World.GameWorld.Instance.GridCoordinates(position)) {
			Setup(position, TEXTURE_ID_SHAPE);
		}

		public override void Draw(GameTime time, SpriteBatch batch) {
			SimpleDraw(time, batch, Tint, TEXTURE_ID_SHAPE);
			SimpleDraw(time, batch, Color.White, TEXTURE_ID_EYES);
		}
	}
}
