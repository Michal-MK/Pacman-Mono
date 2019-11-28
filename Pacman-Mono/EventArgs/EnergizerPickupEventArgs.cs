using System;

namespace MonoGame {
	public class EnergizerPickupEventArgs : EventArgs {

		public int TotalCollected { get; set; }

		public Energizer Energizer { get; set; }


		public EnergizerPickupEventArgs(Energizer energ, int totalCollected) {
			TotalCollected = totalCollected;
			Energizer = energ;
		}
	}
}
