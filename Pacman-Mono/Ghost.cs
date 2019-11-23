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
			Texture2D shape = Game.Sprites[TEXTURE_ID_SHAPE];
			batch.Draw(shape, Position, shape.Bounds, Tint, 0, shape.Bounds.Center.ToVector2(), Scale, SpriteEffects.None, 0);

			Texture2D eyes = Game.Sprites[TEXTURE_ID_EYES];
			batch.Draw(eyes, Position, eyes.Bounds, Color.White, 0, eyes.Bounds.Center.ToVector2(), Scale, SpriteEffects.None, 0);
		}
	}
}
