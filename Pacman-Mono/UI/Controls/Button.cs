using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.BitmapFonts;
using MonoGame.UI.Controls.Base;
using MonoGame.UI.Enums;

namespace MonoGame.UI.Controls {
	public class Button : UIElement {

		public const string TEXTURE_ID = "button";

		public event EventHandler OnClick;

		private string buttonText;
		public string ButtonText { get => buttonText; set { buttonText = value; textHalfSize = Game.Font.MeasureString(value) * 0.5f; } }

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

			batch.DrawString(Game.Font, ButtonText, Position.ToVector2() - textHalfSize, Color.DarkRed);
		}
	}
}
