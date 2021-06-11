using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Scenes.Base;
using MonoGame.Scenes.Enum;
using MonoGame.World;

namespace MonoGame.Scenes {
	public class GameScene : Scene {
		public override ActiveScene SceneName => ActiveScene.Game;

		private readonly GameWorld world;

		public GameScene() {
			world = new GameWorld(WorldDefinitions.LARGE_WORLD_19x19);
		}

		public override void Update(GameTime time) {
			world.Update(time);
		}

		public override void Draw(GameTime time, SpriteBatch batch) {
			world.Draw(time, batch);
		}
	}
}
