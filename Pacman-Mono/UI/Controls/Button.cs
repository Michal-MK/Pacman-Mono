using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.UI.Enums;

namespace MonoGame.UI.Controls {
	public class Button {

		public const string TEXTURE_ID = "button";


		public event EventHandler OnClick;

		public string ButtonText { get => _buttonText; set { _buttonText = value; textHalfSize = Game.Font.MeasureString(value) * 0.5f; } }

		private string _buttonText;
		private Vector2 textHalfSize;
		private Point buttonPos;
		private Vector2 buttonOriginOffset;
		private readonly Texture2D buttonTexture;
		private OriginMode originMode;

		private ButtonState activeState;

		private bool isMouseOver;

		public Button(Point origin, string text, OriginMode mode) {
			buttonTexture = Game.Sprites[TEXTURE_ID];
			buttonPos = origin;
			ButtonText = text;
			textHalfSize = Game.Font.MeasureString(text) * 0.5f;
			if (mode == OriginMode.Center) {
				buttonOriginOffset = buttonTexture.Bounds.Size.ToVector2() * 0.5f;
			}
			else {
				buttonOriginOffset = Vector2.Zero;
			}
		}

		public void Update(GameTime time) {
			MouseState state = Mouse.GetState();
			Vector2 renderPos = buttonPos.ToVector2() - buttonOriginOffset;

			if (state.X >= renderPos.X && state.Y >= renderPos.Y &&
			   state.X <= renderPos.X + buttonTexture.Width &&
			   state.Y <= renderPos.Y + buttonTexture.Height) {
				if (state.LeftButton == ButtonState.Pressed) {
					activeState = ButtonState.Pressed;
				}
				if (state.LeftButton == ButtonState.Released && activeState == ButtonState.Pressed) {
					OnClick?.Invoke(this, System.EventArgs.Empty);
					activeState = ButtonState.Released;
				}
				isMouseOver = true;
			}
			else {
				isMouseOver = false;
				activeState = ButtonState.Released;
			}
		}

		public void Draw(GameTime time, SpriteBatch batch) {
			batch.Draw(buttonTexture, buttonPos.ToVector2() - buttonOriginOffset,
					   buttonTexture.Bounds, isMouseOver ? Color.Green : Color.White, 0,
					   buttonTexture.Bounds.Location.ToVector2(), 1, SpriteEffects.None, 0);

			batch.DrawString(Game.Font, ButtonText, buttonPos.ToVector2() - buttonOriginOffset +
							 buttonTexture.Bounds.Size.ToVector2() * 0.5f - textHalfSize,
							 Color.DarkRed);
		}
	}
}
