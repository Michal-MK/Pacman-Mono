using Pacman.Behaviours;

namespace Pacman.EventArgData {
	public class EnergizerPickupEventArgs : System.EventArgs {

		public int TotalCollected { get; }

		public Energizer Energizer { get; }


		public EnergizerPickupEventArgs(Energizer energizer, int totalCollected) {
			TotalCollected = totalCollected;
			Energizer = energizer;
		}
	}
}
