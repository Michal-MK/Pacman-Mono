using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Behaviours.Base;

namespace MonoGame.Behaviours {
	public class GhostRemover : Behaviour {
		public const string TEXTURE_ID = "no_ghost";

		public event EventHandler OnCollected;

		public override Vector2 Position { get; protected set; }

		public GhostRemover(Vector2 position) {
			Setup(position, TEXTURE_ID);
		}

		internal void Collect() {
			OnCollected?.Invoke(this, EventArgs.Empty);
		}

		public override void Draw(GameTime time, SpriteBatch batch) => SimpleDraw(time, batch);
	}
}
