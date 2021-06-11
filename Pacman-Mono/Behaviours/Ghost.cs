using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.AI;

namespace MonoGame.Behaviours {
	public class Ghost : GridAnimatedBehaviour {
		protected override Vector2 Scale { get; set; }

		public Color Tint { get; set; } = Color.White;
		public bool IsAfraid { get; private set; }

		public const string TEXTURE_ID_SHAPE = "ghost/ghost_shape";
		public const string TEXTURE_ID_EYES = "ghost/ghost_eyes";
		public static readonly Color AFRAID_TINT = Color.Blue;

		private int afraidTicksRemaining = 0;
		private Vector2 respawnPosition;


		public Ghost(Vector2 position, Type aiType) : base(World.Instance.SelectedWorld, World.Instance.GridCoordinates(position)) {
			Setup(position, TEXTURE_ID_SHAPE);
			respawnPosition = Position;
			AddAI(aiType);
			AI.Initialize(this);
		}

		public void Scare(int energizerCount) {
			IsAfraid = true;
			afraidTicksRemaining = 400 + energizerCount * 200;
		}

		public override void Update(GameTime time) {
			if (IsAfraid) {
				afraidTicksRemaining--;
				if (afraidTicksRemaining == 0) {
					IsAfraid = false;
				}
			}
			base.Update(time);
		}

		public override void Draw(GameTime time, SpriteBatch batch) {
			SimpleDraw(time, batch, Tint, TEXTURE_ID_SHAPE);
			SimpleDraw(time, batch, Color.White, TEXTURE_ID_EYES);
		}

		public void Respawn() {
			IsAfraid = false;
			Position = respawnPosition;
			base.AI.Reset();
		}
	}
}
