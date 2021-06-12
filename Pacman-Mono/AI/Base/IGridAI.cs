using Microsoft.Xna.Framework;
using Pacman.Behaviours.Base;
using Pacman.Structures;

namespace Pacman.AI.Base {
	public interface IGridAI {
		Graph Grid { get; }

		void Initialize(GridAnimatedBehaviour behaviour);

		void Reset();

		void UpdateAgent(GameTime time);
	}
}
