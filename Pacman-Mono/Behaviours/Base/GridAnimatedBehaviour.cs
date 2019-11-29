using System;
using Microsoft.Xna.Framework;

namespace MonoGame {
	public abstract class GridAnimatedBehaviour : Behaviour {

		public Graph Grid { get; set; }

		public override Vector2 Position { get; set; }
		public float AnimationSpeed { get; set; } = 0.04f;
		public IGridAI AI { get; private set; }


		public GridAnimatedBehaviour(char[,] world, Point start) {
			Grid = WorldHelper.GenerateGraphOfOpenSpaces(world, start);
		}

		public void AddAI(Type aiType) {
			AI = (IGridAI)Activator.CreateInstance(aiType);
		}


		public override void Update(GameTime time) {
			AI.UpdateAgent(time);
		}
	}
}
