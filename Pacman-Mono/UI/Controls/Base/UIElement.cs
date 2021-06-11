using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.UI.Enums;

namespace MonoGame.UI.Controls.Base {
	public class UIElement {

		protected Point Position { get; }
		protected Vector2 PositionV2 => Position.ToVector2();
		protected OriginMode OriginMode { get; }

		protected Texture2D MainTexture { get; }
		protected bool IsMouseOver { get; set; }

		public event EventHandler<MouseState> MouseOver;

		protected readonly Vector2 originOffset;

		protected UIElement(Point origin, string textureID, OriginMode mode) {
			Position = origin;
			OriginMode = mode;
			MainTexture = Game.Sprites[textureID];

			if (mode == OriginMode.Center) {
				originOffset = MainTexture.Bounds.Size.ToVector2() * 0.5f;
			}
			else {
				originOffset = Vector2.Zero;
			}
		}

		public virtual void Update(GameTime time) {
			MouseState state = Mouse.GetState();
			(float x, float y) = PositionV2 - originOffset;

			if (state.X >= x && state.Y >= y &&
				state.X <= x + MainTexture.Width &&
				state.Y <= y + MainTexture.Height) {
				IsMouseOver = true;
				MouseOver?.Invoke(this, state);
			}
			else {
				IsMouseOver = false;
			}
		}

		public virtual void Draw(GameTime time, SpriteBatch batch) { }
	}
}
