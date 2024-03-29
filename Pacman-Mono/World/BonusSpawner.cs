﻿using System;
using Microsoft.Xna.Framework;
using Pacman.Behaviours;

namespace Pacman.World {
	public class BonusSpawner {
		private const int MIN_SPAWN_THRESHOLD = 500;
		private const int MAX_SPAWN_THRESHOLD = 2000;

		private int counter;
		private int spawnTrigger;

		public bool BonusSpawned { get; private set; }

		private Bonus bonus;

		public BonusSpawner() {
			spawnTrigger = Main.Random.Next(MIN_SPAWN_THRESHOLD, MAX_SPAWN_THRESHOLD);
		}

		public void Update(GameTime _) {
			if (!BonusSpawned) {
				counter++;
			}
			if (counter != spawnTrigger) return;

			SpawnBonus();
			counter = 0;
			spawnTrigger = Main.Random.Next(MIN_SPAWN_THRESHOLD, MAX_SPAWN_THRESHOLD);
		}

		private void SpawnBonus() {
			BonusSpawned = true;
			bonus = GameWorld.Instance.SpawnBonus(GameWorld.Instance.WorldCoordinates(GameWorld.Instance.GetRandomOpenSpot()));
			bonus.OnCollected += OnBonusCollected;
		}

		private void OnBonusCollected(object sender, EventArgs e) {
			BonusSpawned = false;
			((Bonus)sender).OnCollected -= OnBonusCollected;
		}
	}
}
