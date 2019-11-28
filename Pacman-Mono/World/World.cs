using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame {
	public class World {

		public static World Instance { get; set; }

		public const int FOOD_DISTANCE_THRESHOLD_SQUARED = 164;
		public const int ENERGIZER_DISTANCE_THRESHOLD_SQUARED = 1024 + 512;
		public const int BONUS_DISTANCE_THRESHOLD_SQUARED = 1024 + 512;

		private readonly List<Wall> walls = new List<Wall>();
		private readonly List<Food> food = new List<Food>();
		private readonly List<Ghost> ghosts = new List<Ghost>();
		private readonly List<Energizer> energizers = new List<Energizer>();
		private readonly List<Point> openSpaces = new List<Point>();

		private readonly BonusSpawner bonusSpawner;

		public Player Player { get; }


		public Creep Creep { get; }
		public Bonus Bonus { get; private set; }
		public int SelectedWorldSizeX { get; }
		public int SelectedWorldSizeY { get; }
		public float CellSizeX { get; }
		public float CellSizeY { get; }
		public char[,] SelectedWorld { get; }
		public int TotalFoodOnMap { get; set; }


		public World(char[,] world = null) {
			if (world == null) world = WorldDefinitions.DEFAULT_WORLD_9x9;
			Instance = this;
			SelectedWorld = world;
			SelectedWorldSizeX = world.GetLength(1);
			SelectedWorldSizeY = world.GetLength(0);
			CellSizeX = Game.WINDOW_SIZE_X / (float)SelectedWorldSizeX;
			CellSizeY = Game.WINDOW_SIZE_Y / (float)SelectedWorldSizeY;

			bonusSpawner = new BonusSpawner();

			for (int i = 0; i < SelectedWorldSizeX; i++) {
				for (int j = 0; j < SelectedWorldSizeY; j++) {
					if (world[j, i] == 'W') {
						walls.Add(new Wall(new Vector2(i * CellSizeX, j * CellSizeY)));
					}
					if (world[j, i] == 'P') {
						Player = new Player(new Vector2(i * CellSizeX, j * CellSizeY));
						Player.OnEnergizerPickup += Player_OnEnergizerPickup;
					}
					if (world[j, i] == '*') {
						food.Add(new Food(new Vector2(i * CellSizeX, j * CellSizeY)));
						TotalFoodOnMap++;
					}
					if (world[j, i] == 'C') {
						Creep = new Creep(new Vector2(i * CellSizeX, j * CellSizeY));
					}
					if (world[j, i] == 'G') {
						ghosts.Add(new Ghost(new Vector2(i * CellSizeX, j * CellSizeY)) { Tint = HSV.ColorFromHue(Game.Random.NextDouble() * 360) });
					}
					if (world[j, i] == 'E') {
						energizers.Add(new Energizer(new Vector2(i * CellSizeX, j * CellSizeY)));
					}
					if (world[j, i] == '_') {
						openSpaces.Add(new Point(i, j));
					}
				}
			}
		}

		#region Collisions

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

		public bool IsOverFood(Vector2 position, out Food found) => IsOverBehaviour(position, food, FOOD_DISTANCE_THRESHOLD_SQUARED, out found);
		public bool IsOverEnergizer(Vector2 position, out Energizer found) => IsOverBehaviour(position, energizers, ENERGIZER_DISTANCE_THRESHOLD_SQUARED, out found);
		public bool IsOverBonus(Vector2 position, out Bonus found) => IsOverBehaviour(position, Bonus, BONUS_DISTANCE_THRESHOLD_SQUARED, out found);

		public bool IsOverBehaviour<T>(Vector2 position, ICollection<T> behaviours, int threshold, out T found) where T : Behaviour {
			foreach (T behaviour in behaviours) {
				if (Vector2.DistanceSquared(behaviour.Position, position) < threshold) {
					found = behaviour;
					return true;
				}
			}
			found = null;
			return false;
		}

		public bool IsOverBehaviour<T>(Vector2 position, T behaviour, int threshold, out T found) where T : Behaviour {
			found = behaviour;
			if (behaviour == null) {
				return false;
			}
			return Vector2.DistanceSquared(behaviour.Position, position) < threshold;
		}

		public void RemoveFood(Food food) => this.food.Remove(food);
		public void RemoveEnergizer(Energizer energ) => energizers.Remove(energ);
		public void RemoveBonus(Bonus bonus) { bonus.Collect(); Bonus = null; }

		#endregion

		#region Events

		private void Player_OnEnergizerPickup(object sender, EnergizerPickupEventArgs e) {
			foreach (Ghost gh in ghosts) {
				gh.Scare(e.TotalCollected);
			}
		}

		#endregion

		public Vector2 WorldCoordinates(Point point) => new Vector2(point.X * CellSizeX, point.Y * CellSizeY);

		public Point GridCoordinates(Vector2 worldCoodrinates) => new Point((int)(worldCoodrinates.X / CellSizeX), (int)(worldCoodrinates.Y / CellSizeY));

		public Point GetRandomOpenSpot() => openSpaces[Game.Random.Next(0, openSpaces.Count)];


		public Bonus SpawnBonus(Vector2 position) {
			return Bonus = new Bonus(position);
		}

		public void Update(GameTime time) {
			Player.Update(time);
			Creep?.Update(time);
			bonusSpawner.Update(time);
			Bonus?.Update(time);
			foreach (Ghost g in ghosts) {
				g.Update(time);
			}
		}

		public void Draw(GameTime time, SpriteBatch batch) {
			foreach (Wall wall in walls) {
				wall.Draw(time, batch);
			}
			Creep?.Draw(time, batch);
			Bonus?.Draw(time, batch);
			foreach (Food _food in food) {
				_food.Draw(time, batch);
			}
			foreach (Ghost ghost in ghosts) {
				ghost.Draw(time, batch);
			}
			foreach (Energizer energ in energizers) {
				energ.Draw(time, batch);
			}
			Player.Draw(time, batch);
		}
	}
}
