using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pacman.Scenes.Base;
using Pacman.Scenes.Enum;
using Pacman.UI.Controls;
using Pacman.UI.Enums;

namespace Pacman.Scenes {
	public class MenuScene : Scene {
		public override ActiveScene SceneName => ActiveScene.Menu;

		private readonly Button startGameBtn;
		private readonly Button quitGameBtn;
		private readonly TextControl title;

		public MenuScene() {
			startGameBtn = new Button(new Point(Main.WINDOW_SIZE_X / 2, Main.WINDOW_SIZE_Y / 2), "Start game!", OriginMode.Center);
			startGameBtn.OnClick += (s, e) => { Main.Instance.SceneManager.SwitchSceneEmpty(ActiveScene.Game); };

			quitGameBtn = new Button(new Point(Main.WINDOW_SIZE_X / 2, Main.WINDOW_SIZE_Y / 2 + 100), "Quit!", OriginMode.Center);
			quitGameBtn.OnClick += (s, e) => { Main.Instance.Exit(); };

			title = new TextControl("Pac-Man", Main.NewFont, new Point(Main.WINDOW_SIZE_X / 2, 100), null, OriginMode.Center);
		}

		public override void Update(GameTime time) {
			startGameBtn.Update(time);
			quitGameBtn.Update(time);
			title.Update(time);
		}

		public override void Draw(GameTime time, SpriteBatch batch) {
			startGameBtn.Draw(time, batch);
			quitGameBtn.Draw(time, batch);
			title.Draw(time, batch);
		}
	}
}