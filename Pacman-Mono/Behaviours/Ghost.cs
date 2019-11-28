using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame {
	public class Ghost : GridAnimatedBehaviour {
		public override Vector2 Scale { get; protected set; }

		public Color Tint { get; set; } = Color.White;
		public bool IsAfraid { get; private set; }

		public const string TEXTURE_ID_SHAPE = "ghost/ghost_shape";
		public const string TEXTURE_ID_EYES = "ghost/ghost_eyes";
		public static readonly Color AFRAID_TINT = Color.Blue;

		private int afraidTicksRemaining = 0;

		public Ghost(Vector2 position) : base(World.Instance.SelectedWorld, World.Instance.GridCoordinates(position)) {
			Setup(position, TEXTURE_ID_SHAPE);
		}

		public void Scare(int energizerCount) {
			IsAfraid = true;
			afraidTicksRemaining = 400 + energizerCount * 200;
		}

		public override void Update(GameTime time) {
			if (IsAfraid) {
				afraidTicksRemaining--;
				if(afraidTicksRemaining == 0) {
					IsAfraid = false;
				}

			}
			base.Update(time);
		}

		public override void Draw(GameTime time, SpriteBatch batch) {
			SimpleDraw(time, batch, TEXTURE_ID_SHAPE, IsAfraid ? AFRAID_TINT : Tint);
			SimpleDraw(time, batch, TEXTURE_ID_EYES, Color.White);
		}
	}
}
