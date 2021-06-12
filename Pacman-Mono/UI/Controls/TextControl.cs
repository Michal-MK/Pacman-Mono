using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.UI.Controls.Base;
using MonoGame.UI.Enums;

namespace MonoGame.UI.Controls {
	public class TextControl : UIElement {

		private float rotation;
		private float angle;

		public string Text { get; set; }

		public TextControl(string text, Point origin, string textureID, OriginMode mode) : base(origin, textureID, mode) {
			Text = text;
		}

		public override void Update(GameTime time) {
			base.Update(time);
			rotation = (float) Math.Sin(angle);
			angle += 0.02f;
			angle %= 2*(float)Math.PI;
		}

		public override void Draw(GameTime time, SpriteBatch batch) {
			batch.DrawString(Game.Font, Text, PositionV2, Color.Red, rotation, new Vector2(Game.Font.MeasureString(Text).X / 2, 0), Vector2.One*4, SpriteEffects.None, 0);
		}
	}
}
