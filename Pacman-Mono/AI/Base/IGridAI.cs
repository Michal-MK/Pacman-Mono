using Microsoft.Xna.Framework;
using MonoGame.Behaviours.Base;
using MonoGame.Structures;

namespace MonoGame.AI.Base {
	public interface IGridAI {
		Graph Grid { get; }

		void Initialize(GridAnimatedBehaviour behaviour);

		void Reset();

		void UpdateAgent(GameTime time);
	}
}
