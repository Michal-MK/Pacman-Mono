﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using Pacman.Behaviours.Base;
using Pacman.Enums;
using Pacman.EventArgData;
using Pacman.FileSystem;
using Pacman.Structures;
using Pacman.World;

namespace Pacman.Behaviours {
	public class Player : Behaviour {
		public event EventHandler<EnergizerPickupEventArgs> OnEnergizerPickup;

		public override Vector2 Position { get; set; }
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

		private const int FOOD_VALUE = 10;
		private const int FRUIT_VALUE = 200;
		private const int GHOST_VALUE = 500;

		private const float IMMUNITY_POINTS_MAX = 2000f;

		private float Speed { get; set; } = 4;
		public int FoodCollected { get; private set; } = 0;
		public int FruitsCollected { get; private set; } = 0;
		public int GhostsEaten { get; private set; } = 0;
		public int ImmunityPoints { get; private set; } = (int)IMMUNITY_POINTS_MAX;

		public readonly DateTime start;

		private int PowerupAmount { get; set; }

		public Player(Vector2 position) {
			Setup(position, TEXTURE_ID + textureOffset);
			start = DateTime.Now;
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

			PowerupAmount = Math.Max(0, PowerupAmount - 1);

			if (GameWorld.Instance.IsOverFood(Position, out Food foundF)) {
				GameWorld.Instance.RemoveFood(foundF);
				FoodCollected++;
				if (FoodCollected == GameWorld.Instance.TotalFoodOnMap) {
					FileManager.Save(this);
					Main.Instance.SceneManager.SwitchToPostGame(new PostGameData(this, GameResult.Win));

					//TODO Win
				}
			}

			if (GameWorld.Instance.IsOverEnergizer(Position, out Energizer foundE)) {
				energizersPickedUp++;
				OnEnergizerPickup?.Invoke(this, new EnergizerPickupEventArgs(foundE, energizersPickedUp));
				PowerupAmount = 400 + energizersPickedUp * 200;
			}

			float actualSpeed = Speed + (PowerupAmount != 0 ? 2 : 0);

			if (GameWorld.Instance.IsOverBonus(Position, out Bonus foundB)) {
				GameWorld.Instance.RemoveBonus(foundB);
				FruitsCollected++;
			}

			if (GameWorld.Instance.IsOverGhost(Position, out Ghost foundG)) {
				if (foundG.IsAfraid) {
					GhostsEaten++;
					foundG.Respawn();
				}
				else {
					//TODO Game over
					Main.Instance.SceneManager.SwitchToPostGame(new PostGameData(this, GameResult.Loss));
				}
			}

			if (GameWorld.Instance.IsOverCreep(Position, out CreepSpawn _)) {
				ImmunityPoints = Math.Max(0, ImmunityPoints - 1);
				if (ImmunityPoints == 0) {
					// TODO Game Over
				}
			}
			else if (ImmunityPoints < IMMUNITY_POINTS_MAX) {
				ImmunityPoints = (int)Math.Min(IMMUNITY_POINTS_MAX, ImmunityPoints + 1);
			}

			if (state.IsKeyDown(Keys.Down)) {
				OrientDown();
				Vector2 newPos = Vector2.UnitY * actualSpeed;
				if (GameWorld.Instance.IsValidPlayerPosition(Position + newPos, Size, out Vector2 corr)) {
					Position += newPos;
				}
				else {
					float xCoord = corr.X;
					float xSpeed = (Vector2.UnitX * actualSpeed).X;
					if (Math.Abs(xCoord - xSpeed) < xSpeed * CORRECTION_THRESHOLD) {
						if (xCoord < 0) {
							OrientRight();
							Position += new Vector2(1 * actualSpeed, 0);
						}
						else {
							OrientLeft();
							Position += new Vector2(-1 * actualSpeed, 0);
						}
					}
				}
			}
			else if (state.IsKeyDown(Keys.Up)) {
				OrientUp();
				Vector2 newPos = -Vector2.UnitY * actualSpeed;
				if (GameWorld.Instance.IsValidPlayerPosition(Position + newPos, Size, out Vector2 corr)) {
					Position += newPos;
				}
				else {
					float xCoord = corr.X;
					float xSpeed = (Vector2.UnitX * actualSpeed).X;
					if (Math.Abs(xCoord - xSpeed) < xSpeed * CORRECTION_THRESHOLD) {
						if (xCoord < 0) {
							OrientRight();
							Position += new Vector2(1 * actualSpeed, 0);
						}
						else {
							OrientLeft();
							Position += new Vector2(-1 * actualSpeed, 0);
						}
					}
				}
			}
			else if (state.IsKeyDown(Keys.Left)) {
				OrientLeft();
				Vector2 newPos = -Vector2.UnitX * actualSpeed;
				if (GameWorld.Instance.IsValidPlayerPosition(Position + newPos, Size, out Vector2 corr)) {
					Position += newPos;
				}
				else {
					float yCoord = corr.Y;
					float ySpeed = (Vector2.UnitY * actualSpeed).Y;
					if (Math.Abs(yCoord - ySpeed) < ySpeed * CORRECTION_THRESHOLD) {
						if (yCoord < 0) {
							OrientDown();
							Position += new Vector2(0, 1 * actualSpeed);
						}
						else {
							OrientUp();
							Position += new Vector2(0, -1 * actualSpeed);
						}
					}
				}
			}
			else if (state.IsKeyDown(Keys.Right)) {
				OrientRight();
				Vector2 newPos = Vector2.UnitX * actualSpeed;
				if (GameWorld.Instance.IsValidPlayerPosition(Position + newPos, Size, out Vector2 corr)) {
					Position += newPos;
				}
				else {
					float yCoord = corr.Y;
					float ySpeed = (Vector2.UnitY * actualSpeed).Y;
					if (Math.Abs(yCoord - ySpeed) < ySpeed * CORRECTION_THRESHOLD) {
						if (yCoord < 0) {
							OrientDown();
							Position += new Vector2(0, 1 * actualSpeed);
						}
						else {
							OrientUp();
							Position += new Vector2(0, -1 * actualSpeed);
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
				Main.Shaders[POWERUP_SHADER_ID].Parameters["time"].SetValue((float)time.TotalGameTime.TotalMilliseconds);
				batch.Begin(SpriteSortMode.Texture, effect: Main.Shaders[POWERUP_SHADER_ID]);
			}
			Texture2D tx = Main.Sprites[TEXTURE_ID + textureOffset];
			batch.Draw(tx, Position, tx.Bounds, Color.White, rotation, tx.Bounds.Center.ToVector2(), Scale, flipTexture ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
			if (PowerupAmount > 0) {
				batch.End();
				batch.Begin();
			}
			if (ImmunityPoints < IMMUNITY_POINTS_MAX) {
				Vector2 translated = Position.Translate(-32, -38);
				batch.FillRectangle(translated, new Size2(64, 6), Color.Red);
				batch.FillRectangle(translated, new Size2(64 - 64 * (1 - ImmunityPoints / IMMUNITY_POINTS_MAX), 6), Color.Green);
			}

			batch.DrawString(Main.Font, $"Food collected: {FoodCollected}/{GameWorld.Instance.TotalFoodOnMap}", Vector2.One * 20, Color.White);
			batch.DrawString(Main.Font, $"Fruits Collected: {FruitsCollected}", Vector2.One * 20 + Vector2.UnitY * 16, Color.White);
			batch.DrawString(Main.Font, $"Total Points: {FoodCollected * 10 + FruitsCollected * 200}", Vector2.One * 20 + Vector2.UnitY * 32, Color.White);
			batch.DrawString(Main.Font, $"Total Points: {GetScore()}", Vector2.One * 20 + Vector2.UnitY * 48, Color.White);
		}

		public int GetScore() {
			return FoodCollected * FOOD_VALUE + FruitsCollected * FRUIT_VALUE + GhostsEaten * GHOST_VALUE;
		}
	}
}