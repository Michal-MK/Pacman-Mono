using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pacman.UI.Controls.Base;
using Pacman.UI.Enums;

namespace Pacman.UI.Controls {
	public class ListView : SizedElement {
		private List<SizedElement> items = new();

		public override int SizeX => items.Aggregate(0, (a, b) => a + b.SizeX);
		public override int SizeY => items.Aggregate(0, (a, b) => a + b.SizeY);
		
		public int ItemSpacing { get; set; } 

		public ListView(Point origin, string textureID, OriginMode mode) : base(origin, textureID, mode) { }

		public void AddElements(IEnumerable<SizedElement> elements) {
			items.AddRange(elements);
		}

		public override void Update(GameTime time) {
			foreach (SizedElement uiElement in items) {
				uiElement.Update(time);
			}
		}

		public override void Draw(GameTime time, SpriteBatch batch) {
			int yOffset = 0;
			foreach (SizedElement uiElement in items) {
				uiElement.Position = new Point(Position.X, Position.Y + yOffset);
				yOffset += uiElement.SizeY + ItemSpacing;
				uiElement.Draw(time, batch);
			}
		}
	}
}