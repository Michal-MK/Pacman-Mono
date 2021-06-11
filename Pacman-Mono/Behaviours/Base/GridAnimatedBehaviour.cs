using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Structures;
using MonoGame.World;

namespace MonoGame.Behaviours.Base {
	public abstract class GridAnimatedBehaviour : Behaviour {

		private const float ANIMATION_SPEED = 0.04f;

		private Graph Grid { get; }

		private Vector2 position;
		public override Vector2 Position { get => position; protected set => position = value; }

		#region Private animation

		private bool animate;
		private Point target;
		private Point previousTarget;
		private Vector2 targetPosition;
		private Vector2 initialPosition;
		private float animationProgress = 0;

		#endregion

		protected GridAnimatedBehaviour(char[,] world, Point start) {
			Grid = WorldHelper.GenerateGraphOfOpenSpaces(world, start);
		}

		public override void Update(GameTime time) {
			if (!animate) {
				Point[] targets = Grid.GetEmptyNeighborsIgnoreVisited(GameWorld.Instance.GridCoordinates(Position));
				if (targets.Length == 1 && targets[0] == previousTarget) {
					SelectTarget(previousTarget);
				}
				else if (targets.Length >= 2) {
					targets = targets.Where(t => t != previousTarget).ToArray();
				}
				previousTarget = target;

				SelectTarget(targets[Game.Random.Next(0, targets.Length)]);
				Animate();
			}
			else {
				Animate();
			}
		}

		private void SelectTarget(Point selected) {
			target = selected;
			initialPosition = Position;
			Vector2 diff = (GameWorld.Instance.WorldCoordinates(target) + Size * 0.5f) - initialPosition;
			targetPosition = initialPosition + diff;

			animate = true;
			animationProgress = 0;
		}

		private void Animate() {
			animationProgress += ANIMATION_SPEED;
			if (animationProgress >= 1) {
				animationProgress = 1;
				animate = false;
			}
			Vector2.Lerp(ref initialPosition, ref targetPosition, animationProgress, out position);
		}
	}
}
