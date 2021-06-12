using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Pacman.World;

namespace Pacman.AI {
	public class ChaserAI : BasicAI {

		public override void UpdateAgent(GameTime time) {
			if (!animate) {
				Vector2 target = GetClosest(GameWorld.Instance.GridCoordinates(GameWorld.Instance.Player.Position));
				targetPosition = target;
				initialPosition = Behaviour.Position;
				animate = true;
				animationProgress = 0;
			}
			else {
				Animate();
			}
		}

		private Vector2 GetClosest(Point point) {
			Point me = GameWorld.Instance.GridCoordinates(Behaviour.Position);
			Point[] points = Grid.GetEmptyNeighborsIgnoreVisited(me);

			Vector2 direction = (point - me).ToVector2();
			direction.Normalize();
			int roundedX = (int)Math.Round(direction.X);
			int roundedY = (int)Math.Round(direction.Y);

			Point[] selected = points.Where(p => {
				return p == me + new Point(roundedX, roundedY) ||
					   p == me + new Point(0, roundedY) ||
					   p == me + new Point(roundedX, 0);	   
			}).ToArray();

			if (selected.Length != 0) {
				return GameWorld.Instance.WorldCoordinates(selected[Main.Random.Next(0, selected.Length)]) + Behaviour.Size * 0.5f;
			}
			return GameWorld.Instance.WorldCoordinates(points[Main.Random.Next(0, points.Length)]) + Behaviour.Size * 0.5f;
		}
	}
}
