using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame {
	public class Player : Behaviour {

		public override Vector2 Position { get; set; }
		Vector2 scaleVec;
		public override Vector2 Size { get; }

		float rotation = 0;

		float speed = 4;
		float correctThreshold = 4;

		private const string TEXTURE_ID = "pacman";
		int textureOffset = 1;
		int indexOffsetCounter = 0;
		bool rise = true;

		bool flip = false;

		int foodCollected = 0;

		public Player(Vector2 position) {
			Texture2D tx = Game.Sprites[TEXTURE_ID + textureOffset];
			float scaleX = World.sizeX / tx.Width;
			float scaleY = World.sizeY / tx.Height;
			scaleVec = new Vector2(scaleX, scaleY);

			Vector2 offset = tx.Bounds.Center.ToVector2() * scaleVec;
			Size = new Vector2(tx.Width, tx.Height) * scaleVec;

			Position = position + offset;
		}

		public override void Update(GameTime time) {
			KeyboardState state = Keyboard.GetState();
			if (state.IsKeyDown(Keys.Down) || state.IsKeyDown(Keys.Up) || state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.Right)) {
				indexOffsetCounter++;
				if (indexOffsetCounter % 4 == 0) {
					if (rise) {
						textureOffset += 1;
						rise = textureOffset != 4;
					}
					else {
						textureOffset -= 1;
						rise = textureOffset == 1;
					}
				}
			}

			if (World.Instance.IsOverFood(Position, out Food found)) {
				World.Instance.RemoveFood(found);
				foodCollected++;
			}

			if (state.IsKeyDown(Keys.Down)) {
				OrientDown();
				Vector2 newPos = Vector2.UnitY * speed;
				if (World.Instance.IsValidPlayerPosition(Position + newPos, Size, out Vector2 corr)) {
					Position += newPos;
				}
				else {
					float xCoord = corr.X;
					float xSpeed = (Vector2.UnitX * speed).X;
					if (Math.Abs(xCoord - xSpeed) < xSpeed * correctThreshold) {
						if (xCoord < 0) {
							OrientRight();
							Position += new Vector2(1 * speed, 0);
						}
						else {
							OrientLeft();
							Position += new Vector2(-1 * speed, 0);
						}
					}
				}
			}
			else if (state.IsKeyDown(Keys.Up)) {
				OrientUp();
				Vector2 newPos = -Vector2.UnitY * speed;
				if (World.Instance.IsValidPlayerPosition(Position + newPos, Size, out Vector2 corr)) {
					Position += newPos;
				}
				else {
					float xCoord = corr.X;
					float xSpeed = (Vector2.UnitX * speed).X;
					if (Math.Abs(xCoord - xSpeed) < xSpeed * correctThreshold) {
						if (xCoord < 0) {
							OrientRight();
							Position += new Vector2(1 * speed, 0);
						}
						else {
							OrientLeft();
							Position += new Vector2(-1 * speed, 0);
						}
					}
				}
			}
			else if (state.IsKeyDown(Keys.Left)) {
				OrientLeft();
				Vector2 newPos = -Vector2.UnitX * speed;
				if (World.Instance.IsValidPlayerPosition(Position + newPos, Size, out Vector2 corr)) {
					Position += newPos;
				}
				else {
					float yCoord = corr.Y;
					float ySpeed = (Vector2.UnitY * speed).Y;
					if (Math.Abs(yCoord - ySpeed) < ySpeed * correctThreshold) {
						if (yCoord < 0) {
							OrientDown();
							Position += new Vector2(0, 1 * speed);
						}
						else {
							OrientUp();
							Position += new Vector2(0, -1 * speed);
						}
					}
				}
			}
			else if (state.IsKeyDown(Keys.Right)) {
				OrientRight();
				Vector2 newPos = Vector2.UnitX * speed;
				if (World.Instance.IsValidPlayerPosition(Position + newPos, Size, out Vector2 corr)) {
					Position += newPos;
				}
				else {
					float yCoord = corr.Y;
					float ySpeed = (Vector2.UnitY * speed).Y;
					if (Math.Abs(yCoord - ySpeed) < ySpeed * correctThreshold) {
						if (yCoord < 0) {
							OrientDown();
							Position += new Vector2(0, 1 * speed);
						}
						else {
							OrientUp();
							Position += new Vector2(0, -1 * speed);
						}
					}
				}
			}
		}

		private void OrientRight() {
			rotation = 0;
			flip = false;
		}

		private void OrientLeft() {
			rotation = 0;
			flip = true;
		}

		private void OrientUp() {
			rotation = (float)-Math.PI / 2;
			flip = false;
		}

		private void OrientDown() {
			rotation = (float)Math.PI / 2;
			flip = false;
		}

		public override void Draw(GameTime time, SpriteBatch batch) {
			Texture2D tx = Game.Sprites[TEXTURE_ID + textureOffset];
			batch.Draw(tx, Position, tx.Bounds, Color.White, rotation, tx.Bounds.Center.ToVector2(), scaleVec, flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
			batch.DrawString(Game.Font, $"Food collected: {foodCollected}/{World.Instance.FoodOnMap}", Vector2.One * 20, Color.White);
		}
	}
}
