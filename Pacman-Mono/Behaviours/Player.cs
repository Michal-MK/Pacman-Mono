using System;
using System.Diagnostics;
using MonoGame.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.FileSystem;
using MonoGame.Behaviours.Base;

namespace MonoGame.Behaviours {
	public class Player : Behaviour {

		public event EventHandler<EnergizerPickupEventArgs> OnEnergizerPickup;

		public override Vector2 Position { get; protected set; }
		protected override Vector2 Scale { get; set; }
		public override Vector2 Size { get; protected set; }

		private float rotation = 0;
		private int textureOffset = 1;
		private int textureOffsetCounter = 0;
		private bool counterRises = true;
		private bool flipTexture;

		private int energizersPickedUp = 0;

		private const float CORRECTION_THRESHOLD = 4;
		public const string TEXTURE_ID = "pacman";
		public const string POWERUP_SHADER_ID = "rainbow";

		public const int FOOD_VALUE = 10;
		public const int FRUIT_VALUE = 200;
		public const int GHOST_VALUE = 500;
		public float Speed { get; set; } = 4;
		public int FoodCollected { get; private set; } = 0;
		public int FruitsCollected { get; private set; } = 0;
		public int GhostsEaten { get; private set; } = 0;

		public readonly DateTime Start;

		public Player(Vector2 position) {
			Setup(position, TEXTURE_ID + textureOffset);
			Start = DateTime.Now;
		}

		public override void Update(GameTime time) {
			KeyboardState state = Keyboard.GetState();
			if (state.IsKeyDown(Keys.Down) || state.IsKeyDown(Keys.Up) || state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.Right)) {
				textureOffsetCounter++;
				if (textureOffsetCounter % 4 == 0) {
					if (counterRises) {
						textureOffset += 1;
						counterRises = textureOffset != 4;
					}
					else {
						textureOffset -= 1;
						counterRises = textureOffset == 1;
					}
				}
			}

			PowerupAmount--;

			if (GameWorld.Instance.IsOverFood(Position, out Food foundF)) {
				GameWorld.Instance.RemoveFood(foundF);
				FoodCollected++;
				if (FoodCollected == World.Instance.TotalFoodOnMap) {
					FileManager.Save(this);
					Game.Instance.SceneManager.SwitchToPostGame(new PostGameData(this, GameResult.Win));
					//TODO Win
				}
			}

			if (World.Instance.IsOverEnergizer(Position, out Energizer foundE)) {
				World.Instance.RemoveEnergizer(foundE);
				energizersPickedUp++;
				OnEnergizerPickup?.Invoke(this, new EnergizerPickupEventArgs(foundE, energizersPickedUp));
				Speed += 1;
			}

			if (GameWorld.Instance.IsOverGhostRemover(Position, out GhostRemover foundR)) {
				GameWorld.Instance.RemoveGhostRemover(foundR);
				CanEatGhosts = true;
				PowerupAmount = 300;
			}

			if (GameWorld.Instance.IsOverBonus(Position, out Bonus foundB)) {
				GameWorld.Instance.RemoveBonus(foundB);
				FruitsCollected++;
			}

			if (World.Instance.IsOverGhost(Position, out Ghost foundG)) {
				if (foundG.IsAfraid) {
					GhostsEaten++;
					foundG.Respawn();
				}
				else {
					//TODO Game over
					Game.Instance.SceneManager.SwitchToPostGame(new PostGameData(this, GameResult.Loss));
				}
			}

			if (state.IsKeyDown(Keys.Down)) {
				OrientDown();
				Vector2 newPos = Vector2.UnitY * Speed;
				if (GameWorld.Instance.IsValidPlayerPosition(Position + newPos, Size, out Vector2 corr)) {
					Position += newPos;
				}
				else {
					float xCoord = corr.X;
					float xSpeed = (Vector2.UnitX * Speed).X;
					if (Math.Abs(xCoord - xSpeed) < xSpeed * CORRECTION_THRESHOLD) {
						if (xCoord < 0) {
							OrientRight();
							Position += new Vector2(1 * Speed, 0);
						}
						else {
							OrientLeft();
							Position += new Vector2(-1 * Speed, 0);
						}
					}
				}
			}
			else if (state.IsKeyDown(Keys.Up)) {
				OrientUp();
				Vector2 newPos = -Vector2.UnitY * Speed;
				if (GameWorld.Instance.IsValidPlayerPosition(Position + newPos, Size, out Vector2 corr)) {
					Position += newPos;
				}
				else {
					float xCoord = corr.X;
					float xSpeed = (Vector2.UnitX * Speed).X;
					if (Math.Abs(xCoord - xSpeed) < xSpeed * CORRECTION_THRESHOLD) {
						if (xCoord < 0) {
							OrientRight();
							Position += new Vector2(1 * Speed, 0);
						}
						else {
							OrientLeft();
							Position += new Vector2(-1 * Speed, 0);
						}
					}
				}
			}
			else if (state.IsKeyDown(Keys.Left)) {
				OrientLeft();
				Vector2 newPos = -Vector2.UnitX * Speed;
				if (GameWorld.Instance.IsValidPlayerPosition(Position + newPos, Size, out Vector2 corr)) {
					Position += newPos;
				}
				else {
					float yCoord = corr.Y;
					float ySpeed = (Vector2.UnitY * Speed).Y;
					if (Math.Abs(yCoord - ySpeed) < ySpeed * CORRECTION_THRESHOLD) {
						if (yCoord < 0) {
							OrientDown();
							Position += new Vector2(0, 1 * Speed);
						}
						else {
							OrientUp();
							Position += new Vector2(0, -1 * Speed);
						}
					}
				}
			}
			else if (state.IsKeyDown(Keys.Right)) {
				OrientRight();
				Vector2 newPos = Vector2.UnitX * Speed;
				if (GameWorld.Instance.IsValidPlayerPosition(Position + newPos, Size, out Vector2 corr)) {
					Position += newPos;
				}
				else {
					float yCoord = corr.Y;
					float ySpeed = (Vector2.UnitY * Speed).Y;
					if (Math.Abs(yCoord - ySpeed) < ySpeed * CORRECTION_THRESHOLD) {
						if (yCoord < 0) {
							OrientDown();
							Position += new Vector2(0, 1 * Speed);
						}
						else {
							OrientUp();
							Position += new Vector2(0, -1 * Speed);
						}
					}
				}
			}
		}

		private void OrientRight() {
			rotation = 0;
			flipTexture = false;
		}

		private void OrientLeft() {
			rotation = 0;
			flipTexture = true;
		}

		private void OrientUp() {
			rotation = (float)-Math.PI / 2;
			flipTexture = false;
		}

		private void OrientDown() {
			rotation = (float)Math.PI / 2;
			flipTexture = false;
		}

		public override void Draw(GameTime time, SpriteBatch batch) {
			if (PowerupAmount > 0) {
				batch.End();
				Game.Shaders[POWERUP_SHADER_ID].Parameters["time"].SetValue((float)time.TotalGameTime.TotalMilliseconds);
				batch.Begin(SpriteSortMode.Texture, effect: Game.Shaders[POWERUP_SHADER_ID]);
			}
			Texture2D tx = Game.Sprites[TEXTURE_ID + textureOffset];
			batch.Draw(tx, Position, tx.Bounds, Color.White, rotation, tx.Bounds.Center.ToVector2(), Scale, flipTexture ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
			if (PowerupAmount > 0) {
				batch.End();
				batch.Begin();
			}
			batch.DrawString(Game.Font, $"Food collected: {FoodCollected}/{GameWorld.Instance.TotalFoodOnMap}", Vector2.One * 20, Color.White);
			batch.DrawString(Game.Font, $"Fruits Collected: {FruitsCollected}", Vector2.One * 20 + Vector2.UnitY * 16, Color.White);
			batch.DrawString(Game.Font, $"Total Points: {FoodCollected * 10 + FruitsCollected * 200}", Vector2.One * 20 + Vector2.UnitY * 32, Color.White);
			batch.DrawString(Game.Font, $"Total Points: {GetScore()}", Vector2.One * 20 + Vector2.UnitY * 48, Color.White);
		}

		public int GetScore() {
			return FoodCollected * FOOD_VALUE + FruitsCollected * FRUIT_VALUE + GhostsEaten * GHOST_VALUE;
		}
	}
}
