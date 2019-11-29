using System.Linq;
using Microsoft.Xna.Framework;

namespace MonoGame.AI {
	public class BasicAI : IGridAI {
		public Graph Grid { get; private set; }
		public GridAnimatedBehaviour Behaviour { get; private set; }

		#region Private animation

		private Point target;
		private Point previousTarget;
		
		protected float animationProgress = 0;
		protected bool animate;
		protected Vector2 targetPosition;
		protected Vector2 initialPosition;

		#endregion

		public void Initialize(GridAnimatedBehaviour behaviour) {
			Behaviour = behaviour;
			Grid = Behaviour.Grid;
		}

		public virtual void UpdateAgent(GameTime time) {
			if (!animate) {
				Point[] targets = Grid.GetEmptyNeighborsIgnoreVisited(World.Instance.GridCoordinates(Behaviour.Position));
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
			initialPosition = Behaviour.Position;
			Vector2 diff = (World.Instance.WorldCoordinates(target) + Behaviour.Size * 0.5f) - initialPosition;
			targetPosition = initialPosition + diff;

			animate = true;
			animationProgress = 0;
		}

		protected void Animate() {
			animationProgress += Behaviour.AnimationSpeed;
			if (animationProgress >= 1) {
				animationProgress = 1;
				animate = false;
			}
			Behaviour.Position = Vector2.Lerp(initialPosition, targetPosition, animationProgress);
		}

		public void Reset() {
			animate = false;
			animationProgress = 0;
		}
	}
}
