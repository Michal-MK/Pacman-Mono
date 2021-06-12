using System;
using System.IO;
using System.Reflection;
using Pacman.Behaviours;

namespace Pacman.FileSystem {
	public static class FileManager {
		private static readonly DirectoryInfo GameDir = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory;

		private static readonly DirectoryInfo SavesDir = GameDir.CreateSubdirectory("Saves");

		public static void Save(Player playerData) {
			string fileName = Path.Combine(SavesDir.FullName, Guid.NewGuid() + ".txt");
			using StreamWriter sw = File.CreateText(fileName);
			sw.WriteLine(DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToLongTimeString());
			sw.WriteLine($"Playtime:{DateTime.Now - playerData.start}");
			sw.WriteLine("Statistics:");
			sw.WriteLine($" Food: {playerData.FoodCollected}");
			sw.WriteLine($" Ghosts: {playerData.GhostsEaten}");
			sw.WriteLine($" Fruits: {playerData.FruitsCollected}");
			sw.WriteLine($" Score: {playerData.GetScore()}");
		}
	}
}
