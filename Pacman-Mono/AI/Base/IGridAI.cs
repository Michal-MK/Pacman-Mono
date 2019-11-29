using Microsoft.Xna.Framework;

namespace MonoGame {
	public interface IGridAI {
		Graph Grid { get; }

		void Initialize(GridAnimatedBehaviour behaviour);

		void Reset();

		void UpdateAgent(GameTime time);
	}
}
