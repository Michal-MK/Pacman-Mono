using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pacman.Behaviours.Base;

namespace Pacman.Behaviours {
	public class Bonus : Behaviour {
		public override Vector2 Position { get; set; }

		public event EventHandler OnCollected;

		public const string TEXTURE_ID = "bonus";

		public Bonus(Vector2 position) {
			Setup(position, TEXTURE_ID);
		}

		internal void Collect() {
			OnCollected?.Invoke(this, EventArgs.Empty);
		}

		public override void Draw(GameTime time, SpriteBatch batch) => SimpleDraw(time, batch);
	}
}
