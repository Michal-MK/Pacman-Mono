using System;
using Microsoft.Xna.Framework;
using MonoGame.Behaviours;

namespace MonoGame.World {
	public class GhostRemoverSpawner {
		private const int MIN_SPAWN_THRESHOLD = 1;
		private const int MAX_SPAWN_THRESHOLD = 2;
		private const int MAX_SPAWNED = 40;

		private int counter;
		private int spawnTrigger;

		private int numberSpawned;

		public GhostRemoverSpawner() {
			spawnTrigger = Game.Random.Next(MIN_SPAWN_THRESHOLD, MAX_SPAWN_THRESHOLD);
		}

		public void Update(GameTime _) {
			if (numberSpawned < MAX_SPAWNED) {
				counter++;
			}
			if (counter != spawnTrigger) return;

			numberSpawned++;
			GhostRemover remover = GameWorld.Instance.SpawnGhostRemover(GameWorld.Instance.WorldCoordinates(GameWorld.Instance.GetRandomOpenSpot()));
			remover.OnCollected += OnCollect;
			counter = 0;
			spawnTrigger = Game.Random.Next(MIN_SPAWN_THRESHOLD, MAX_SPAWN_THRESHOLD);
		}

		private void OnCollect(object sender, EventArgs e) {
			numberSpawned--;
			((GhostRemover) sender).OnCollected -= OnCollect;
		}
	}
}
