using System;
using System.IO;
using System.Reflection;

namespace MonoGame.FileSystem {
	public static class FileManager {

		public static DirectoryInfo GameDir = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory;

		public static DirectoryInfo SavesDir = GameDir.CreateSubdirectory("Saves");

		public static void Save(Player playerData) {
			string fileName = Path.Combine(SavesDir.FullName, Guid.NewGuid().ToString() + ".txt");
			using (StreamWriter sw = File.CreateText(fileName)) {
				sw.WriteLine(DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToLongTimeString());
				sw.WriteLine($"Playtime:{DateTime.Now - playerData.Start}");
				sw.WriteLine("Statistics:");
				sw.WriteLine($" Food: {playerData.FoodCollected}");
				sw.WriteLine($" Ghosts: {playerData.GhostsEaten}");
				sw.WriteLine($" Fruits: {playerData.FruitsCollected}");
				sw.WriteLine($" Score: {playerData.GetScore()}");
			}
		}
	}
}
