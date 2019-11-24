using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame {
	public class Ghost : GridAnimatedBehaviour {
		public override Vector2 Scale { get; protected set; }

		public Color Tint { get; set; } = Color.White;

		public const string TEXTURE_ID_SHAPE = "ghost/ghost_shape";
		public const string TEXTURE_ID_EYES = "ghost/ghost_eyes";

		public Ghost(Vector2 position) : base(World.Instance.SelectedWorld, World.Instance.GridCoordinates(position)) {
			Setup(position, TEXTURE_ID_SHAPE);
		}

		public override void Draw(GameTime time, SpriteBatch batch) {
			SimpleDraw(time, batch, TEXTURE_ID_SHAPE, Tint);
			SimpleDraw(time, batch, TEXTURE_ID_EYES, Color.White);
		}
	}
}
