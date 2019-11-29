using System;
using Microsoft.Xna.Framework;

namespace MonoGame {
	public class BonusSpawner {

		public const int MIN_SPAWN_THRESHOLD = 400;
		public const int MAX_SPAWN_THRESHOLD = 1600;

		private int counter;
		private int spawnTrigger;

		public bool BonusSpawned { get; private set; }
		private Bonus bonus;

		public BonusSpawner() {
			spawnTrigger = Game.Random.Next(MIN_SPAWN_THRESHOLD, MAX_SPAWN_THRESHOLD);
		}

		public void Update(GameTime time) {
			if (!BonusSpawned) {
				counter++;
			}

			if (counter == spawnTrigger) {
				SpawnBonus();
				counter = 0;
				spawnTrigger = Game.Random.Next(MIN_SPAWN_THRESHOLD, MAX_SPAWN_THRESHOLD);
			}
		}

		public void SpawnBonus() {
			BonusSpawned = true;
			bonus = World.Instance.SpawnBonus(World.Instance.WorldCoordinates(World.Instance.GetRandomOpenSpot()));
			bonus.OnCollected += OnBonusCollected;
		}

		private void OnBonusCollected(object sender, Bonus casted) {
			BonusSpawned = false;
			casted.OnCollected -= OnBonusCollected;
		}

		public void DespawnBonus() {
			if(bonus != null) {
				bonus.OnCollected -= OnBonusCollected;
			}
			BonusSpawned = false;
			World.Instance.SpawnBonus(World.Instance.WorldCoordinates(World.Instance.GetRandomOpenSpot()));
		}
	}
}
