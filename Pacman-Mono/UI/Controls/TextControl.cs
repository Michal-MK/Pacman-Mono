using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;
using Pacman.UI.Controls.Base;
using Pacman.UI.Enums;

namespace Pacman.UI.Controls {
	public class TextControl : SizedElement {

		private float rotation;
		private float angle;
		private readonly BitmapFont renderFont;

		public string Text { get; set; }

		public override int SizeX => (int)renderFont.MeasureString(Text).Width;
		public override int SizeY => (int)renderFont.MeasureString(Text).Height;

		public TextControl(string text, BitmapFont font, Point origin, string textureID, OriginMode mode) : base(origin, textureID, mode) {
			Text = text;
			renderFont = font;
		}

		public override void Update(GameTime time) {
			base.Update(time);
			rotation = (float)Math.Sin(angle) * 0.2f;
			angle += 0.02f;
			angle %= 2 * (float)Math.PI;
		}

		public override void Draw(GameTime time, SpriteBatch batch) {
			Size2 measure = renderFont.MeasureString(Text);
			batch.DrawString(renderFont, Text, PositionV2, Color.Red, rotation, measure / 2, 1, SpriteEffects.None, 0.5f);
		}
	}
}
