using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pacman.Scenes.Enum;

namespace Pacman.Scenes.Base {
	public abstract class Scene {

		public abstract ActiveScene SceneName { get; }

		public abstract void Update(GameTime time);

		public abstract void Draw(GameTime time, SpriteBatch batch);
	}
}
