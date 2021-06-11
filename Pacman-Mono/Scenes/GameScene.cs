using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame {
	public class GameScene : Scene {
		public override ActiveScene SceneName => ActiveScene.Game;

		public World World { get; set; }

		public GameScene() {
			World = new World(WorldDefinitions.LARGE_WORLD_19x19);
		}

		public override void Update(GameTime time) {
			World.Update(time);
		}

		public override void Draw(GameTime time, SpriteBatch batch) {
			World.Draw(time, batch);
		}
	}
}
