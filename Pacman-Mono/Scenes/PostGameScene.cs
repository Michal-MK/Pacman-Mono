using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.UI;

namespace MonoGame {
	public class PostGameScene : Scene {
		public override ActiveScene SceneName => ActiveScene.PostGame;

		public Button MenuBtn { get; }
		public Button RestartGameBtn { get; set; }

		public PostGameData Data { get; }

		public PostGameScene(PostGameData data) {
			Data = data;

			MenuBtn = new Button(new Point(Game.WINDOW_SIZE_X / 2, Game.WINDOW_SIZE_Y / 2), "Go to Menu!", OriginMode.Center);
			MenuBtn.OnClick += (s, e) => { Game.Instance.SceneManager.SwitchSceneEmpty(ActiveScene.Menu); };

			RestartGameBtn = new Button(new Point(Game.WINDOW_SIZE_X / 2, Game.WINDOW_SIZE_Y / 2 + 100), "Restart!", OriginMode.Center);
			RestartGameBtn.OnClick += (s, e) => { Game.Instance.SceneManager.SwitchSceneEmpty(ActiveScene.Game); };
		}

		public override void Update(GameTime time) {
			MenuBtn.Update(time);
			RestartGameBtn.Update(time);
		}

		public override void Draw(GameTime time, SpriteBatch batch) {
			MenuBtn.Draw(time, batch);
			RestartGameBtn.Draw(time, batch);
		}
	}
}
