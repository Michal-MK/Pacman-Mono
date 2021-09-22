using Microsoft.Xna.Framework;
using Pacman.UI.Enums;

namespace Pacman.UI.Controls.Base {
	public abstract class SizedElement : UIElement {
		
		public abstract int SizeY { get; }
		
		public abstract int SizeX { get; }

		protected SizedElement(Point origin, string textureID, OriginMode mode) : base(origin, textureID, mode) { }
	}
}