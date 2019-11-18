using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame {
	public class World {
		private readonly List<Wall> walls = new List<Wall>();
		private readonly List<Food> food = new List<Food>();
		public Player Player { get; }
		public Creep Creep { get; }

		public const int DEFAULT_WORLD_SIZE_X = WorldDefinitions.LARGE_WORLD_SIZE_X;
		public const int DEFAULT_WORLD_SIZE_Y = WorldDefinitions.LARGE_WORLD_SIZE_Y;

		public const int FOOD_DISTANCE_THRESHOLD_SQUARED = 164;

		public const float sizeX = Game.WINDOW_SIZE_X / (float)DEFAULT_WORLD_SIZE_X;
		public const float sizeY = Game.WINDOW_SIZE_Y / (float)DEFAULT_WORLD_SIZE_Y;

		public static World Instance { get; set; }

		public int FoodOnMap { get; set; }

		public bool IsValidPlayerPosition(Vector2 playerPos, Vector2 playerSize, out Vector2 correction) {
			foreach (Wall wall in walls) {
				if (Math.Abs(wall.Position.X - playerPos.X) < (wall.Size.X + playerSize.X) * 0.45f) {
					if (Math.Abs(wall.Position.Y - playerPos.Y) < (wall.Size.Y + playerSize.Y) * 0.45f) {
						float xError = playerPos.X - wall.Position.X;
						float yError = playerPos.Y - wall.Position.Y;

						float correctedX = Math.Abs(xError) - (wall.Size.X + playerSize.X) * 0.5f;
						float correctedY = Math.Abs(yError) - (wall.Size.Y + playerSize.Y) * 0.5f;

						correction = new Vector2(xError > 0 ? correctedX : -correctedX, yError > 0 ? correctedY : -correctedY);
						return false;
					}
				}
			}
			correction = Vector2.Zero;
			return true;
		}

		public Vector2 WorldCoordinates(Point point) {
			return new Vector2(point.X * sizeX, point.Y * sizeY);
		}

		public bool IsOverFood(Vector2 playerPos, out Food found) {
			foreach (Food _food in food) {
				if (Vector2.DistanceSquared(_food.Position, playerPos) < FOOD_DISTANCE_THRESHOLD_SQUARED) {
					found = _food;
					return true;
				}
			}
			found = null;
			return false;
		}

		public void RemoveFood(Food food) {
			this.food.Remove(food);
		}

		public World(char[,] world = null) {
			if (world == null) world = WorldDefinitions.LARGE_WORLD_19x19;
			Instance = this;

			for (int i = 0; i < DEFAULT_WORLD_SIZE_X; i++) {
				for (int j = 0; j < DEFAULT_WORLD_SIZE_Y; j++) {
					if (world[j, i] == 'W')
						walls.Add(new Wall(new Vector2(i * sizeX, j * sizeY)));
					if (world[j, i] == 'P')
						Player = new Player(new Vector2(i * sizeX, j * sizeY));
					if (world[j, i] == '*') {
						food.Add(new Food(new Vector2(i * sizeX, j * sizeY)));
						FoodOnMap++;
					}
					if (world[j, i] == 'C') {
						Creep = new Creep(new Vector2(i * sizeX, j * sizeY));
					}
				}
			}
		}

		public (int X, int Y) GridCoordinates(Vector2 worldCoodrinates) {
			return ((int)(worldCoodrinates.X / sizeX), (int)(worldCoodrinates.Y / sizeY));
		}

		public void Update(GameTime time) {
			Player.Update(time);
			Creep.Update(time);
		}

		public void Draw(GameTime time, SpriteBatch batch) {
			foreach (Wall wall in walls) {
				wall.Draw(time, batch);
			}
			Creep.Draw(time, batch);
			foreach (Food _food in food) {
				_food.Draw(time, batch);
			}
			Player.Draw(time, batch);
		}
	}
}
