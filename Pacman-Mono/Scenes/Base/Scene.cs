using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Scenes.Enum;

namespace MonoGame.Scenes.Base {
	public abstract class Scene {

		public abstract ActiveScene SceneName { get; }

		public abstract void Update(GameTime time);

		public abstract void Draw(GameTime time, SpriteBatch batch);
	}
}
