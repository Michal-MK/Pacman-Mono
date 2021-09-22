using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pacman.UI.Controls.Base;
using Pacman.UI.Enums;

namespace Pacman.UI.Controls {
	public class Button : SizedElement {

		public const string TEXTURE_ID = "button";

		public event EventHandler OnClick;

		public override int SizeY => MainTexture.Bounds.Height;
		public override int SizeX => MainTexture.Bounds.Width;

		private string buttonText;
		public string ButtonText { get => buttonText; set { buttonText = value; textHalfSize = Main.Font.MeasureString(value) * 0.5f; } }

		private Vector2 textHalfSize;

		private ButtonState activeState;

		public Button(Point origin, string text, OriginMode mode) : base(origin, TEXTURE_ID, mode) {
			ButtonText = text;
			MouseOver += OnMouseOver;
		}

		private void OnMouseOver(object sender, MouseState e) {
			switch (e.LeftButton) {
				case ButtonState.Pressed:
					activeState = ButtonState.Pressed;
					break;
				case ButtonState.Released when activeState == ButtonState.Pressed:
					OnClick?.Invoke(this, EventArgs.Empty);
					activeState = ButtonState.Released;
					break;
				default:
					activeState = ButtonState.Released;
					break;
			}
		}

		public override void Draw(GameTime time, SpriteBatch batch) {
			batch.Draw(MainTexture, PositionV2 - originOffset,
				MainTexture.Bounds, IsMouseOver ? Color.Green : Color.White, 0,
				MainTexture.Bounds.Location.ToVector2(), 1, SpriteEffects.None, 0);

			batch.DrawString(Main.Font, ButtonText, Position.ToVector2() - textHalfSize, Color.DarkRed);
		}
	}
}
