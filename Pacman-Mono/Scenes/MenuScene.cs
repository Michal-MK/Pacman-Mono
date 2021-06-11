using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Scenes.Base;
using MonoGame.Scenes.Enum;
using MonoGame.UI.Controls;
using MonoGame.UI.Enums;

namespace MonoGame.Scenes {
	public class MenuScene : Scene {
		public override ActiveScene SceneName => ActiveScene.Menu;

		private readonly Button startGameBtn;
		private readonly Button quitGameBtn;

		public MenuScene() {
			startGameBtn = new Button(new Point(Game.WINDOW_SIZE_X / 2, Game.WINDOW_SIZE_Y / 2), "Start game!", OriginMode.Center);
			startGameBtn.OnClick += (s, e) => { Game.Instance.SceneManager.SwitchSceneEmpty(ActiveScene.Game); };

			quitGameBtn = new Button(new Point(Game.WINDOW_SIZE_X / 2, Game.WINDOW_SIZE_Y / 2 + 100), "Quit!", OriginMode.Center);
			quitGameBtn.OnClick += (s, e) => { Game.Instance.Exit(); };
		}

		public override void Update(GameTime time) {
			startGameBtn.Update(time);
			quitGameBtn.Update(time);
		}

		public override void Draw(GameTime time, SpriteBatch batch) {
			startGameBtn.Draw(time, batch);
			quitGameBtn.Draw(time, batch);
		}
	}
}
