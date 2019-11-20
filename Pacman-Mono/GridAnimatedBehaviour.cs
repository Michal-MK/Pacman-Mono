using System;
using System.Linq;
using Microsoft.Xna.Framework;

namespace MonoGame {
	public abstract class GridAnimatedBehaviour : Behaviour {

		protected Graph Grid { get; set; }

		private Vector2 _position;
		public override Vector2 Position { get => _position; set => _position = value; }

		public float AnimationSpeed { get; set; } = 0.04f;

		#region Private animation
		private readonly Random r = new Random();
		private bool animate;
		private Point target;
		private Point previousTarget;
		private Vector2 targetPosition;
		private Vector2 initialPosition;
		private float animationProgress = 0;

		#endregion

		public GridAnimatedBehaviour(char[,] world, Point start) {
			Grid = WorldHelper.GenerateGraphOfOpenSpaces(world, start);
		}

		public override void Update(GameTime time) {
			if (!animate) {
				Point[] targets = Grid.GetEmptyNeighborsIgnoreVisited(World.Instance.GridCoordinates(Position));
				if (targets.Length == 1 && targets[0] == previousTarget) {
					SelectTarget(previousTarget);
				}
				else if (targets.Length >= 2) {
					targets = targets.Where(t => t != previousTarget).ToArray();
				}
				previousTarget = target;

				SelectTarget(targets[r.Next(0, targets.Length)]);
				Animate();
			}
			else {
				Animate();
			}
		}

		private void SelectTarget(Point selected) {
			target = selected;
			initialPosition = Position;
			Vector2 diff = (World.Instance.WorldCoordinates(target) + Size * 0.5f) - initialPosition;
			targetPosition = initialPosition + diff;

			animate = true;
			animationProgress = 0;
		}

		private void Animate() {
			animationProgress += AnimationSpeed;
			if (animationProgress >= 1) {
				animationProgress = 1;
				animate = false;
			}
			Vector2.Lerp(ref initialPosition, ref targetPosition, animationProgress, out _position);
		}
	}
}
