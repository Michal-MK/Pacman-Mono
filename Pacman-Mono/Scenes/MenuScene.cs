using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.UI;

namespace MonoGame {
	public class MenuScene : Scene {
		public override ActiveScene SceneName => ActiveScene.Menu;

		public Button StartGameBtn { get; }
		public Button QuitGameBtn { get; }

		public MenuScene() {
			StartGameBtn = new Button(new Point(Game.WINDOW_SIZE_X / 2, Game.WINDOW_SIZE_Y / 2), "Start game!", OriginMode.Center);
			StartGameBtn.OnClick += (s, e) => { Game.Instance.SceneManager.SwitchSceneEmpty(ActiveScene.Game); };

			QuitGameBtn = new Button(new Point(Game.WINDOW_SIZE_X / 2, Game.WINDOW_SIZE_Y / 2 + 100), "Quit!", OriginMode.Center);
			QuitGameBtn.OnClick += (s, e) => { Game.Instance.Exit(); };
		}

		public override void Update(GameTime time) {
			StartGameBtn.Update(time);
			QuitGameBtn.Update(time);
		}

		public override void Draw(GameTime time, SpriteBatch batch) {
			StartGameBtn.Draw(time, batch);
			QuitGameBtn.Draw(time, batch);
		}
	}
}
