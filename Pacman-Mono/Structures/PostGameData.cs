using System;
using Pacman.Behaviours;
using Pacman.Enums;

namespace Pacman.Structures {
	public struct PostGameData {
		public PostGameData(Player player, GameResult win) {
			Result = win;
			GhostsEaten = player.GhostsEaten;
			FruitsCollected = player.FruitsCollected;
			FoodCollected = player.FoodCollected;
			StartTime = player.start;
			EndTime = DateTime.Now;
		}

		public GameResult Result { get; }

		public int GhostsEaten { get; }

		public int FruitsCollected { get; }

		public int FoodCollected { get; }

		public DateTime StartTime { get; }

		public DateTime EndTime { get; }

		public TimeSpan PlayTime => EndTime - StartTime;
	}
}
