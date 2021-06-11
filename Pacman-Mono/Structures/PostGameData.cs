using System;

namespace MonoGame {
	public struct PostGameData {
		public PostGameData(Player player, GameResult win) {
			Result = win;
			GhostsEaten = player.GhostsEaten;
			FruitsCollected = player.FruitsCollected;
			FoodCollected = player.FoodCollected;
			StartTime = player.Start;
			EndTime = DateTime.Now;
		}

		public GameResult Result { get; set; }

		public int GhostsEaten { get; set; }

		public int FruitsCollected { get; set; }

		public int FoodCollected { get; set; }

		public DateTime StartTime { get; set; }

		public DateTime EndTime { get; set; }

		public TimeSpan PlayTime => EndTime - StartTime;
	}
}
