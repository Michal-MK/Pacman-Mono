﻿using System;
using Microsoft.Xna.Framework;
using MonoGame.AI.Base;
using MonoGame.Structures;
using MonoGame.World;

namespace MonoGame.Behaviours.Base {
	public abstract class GridAnimatedBehaviour : Behaviour {
		public const float ANIMATION_SPEED = 0.04f;

		public Graph Grid { get; }
		public override Vector2 Position { get; set; }
		protected IGridAI AI { get; private set; }

		protected GridAnimatedBehaviour(char[,] world, Point start) {
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
