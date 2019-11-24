using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame {
	public class Bonus : Behaviour {
		public override Vector2 Position { get; set; }

		public event EventHandler<Bonus> OnCollected;

		public const string TEXTURE_ID = "bonus";

		public Bonus(Vector2 position) {
			Setup(position, TEXTURE_ID);
		}

		public override void Draw(GameTime time, SpriteBatch batch) => SimpleDraw(time, batch, TEXTURE_ID, Color.White);

		internal void Collect() {
			OnCollected?.Invoke(this, this);
		}
	}
}
