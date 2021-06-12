using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pacman.Scenes.Base;
using Pacman.Scenes.Enum;
using Pacman.World;

namespace Pacman.Scenes {
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
