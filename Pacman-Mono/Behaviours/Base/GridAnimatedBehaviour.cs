using System;
using Microsoft.Xna.Framework;
using Pacman.AI.Base;
using Pacman.Structures;
using Pacman.World;

namespace Pacman.Behaviours.Base {
	public abstract class GridAnimatedBehaviour : Behaviour {
		public const float ANIMATION_SPEED = 0.04f;

		public Graph Grid { get; }
		public override Vector2 Position { get; set; }
		protected IGridAI AI { get; private set; }

		protected GridAnimatedBehaviour(char[,] world, Point start) {
			Grid = WorldHelper.GenerateGraphOfOpenSpaces(world, start);
		}

		protected void AddAI(Type aiType) {
			AI = (IGridAI)Activator.CreateInstance(aiType);
		}

		public override void Update(GameTime time) {
			AI.UpdateAgent(time);
		}
	}
}
