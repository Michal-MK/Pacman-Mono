using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pacman.Scenes.Base;
using Pacman.Scenes.Enum;
using Pacman.Structures;
using Pacman.UI.Controls;
using Pacman.UI.Enums;

namespace Pacman.Scenes {
	public class PostGameScene : Scene {
		public override ActiveScene SceneName => ActiveScene.PostGame;

		private readonly Button menuBtn;
		private readonly Button restartGameBtn;

		public PostGameData Data { get; }

		public PostGameScene(PostGameData data) {
			Data = data;

			menuBtn = new Button(new Point(Main.WINDOW_SIZE_X / 2, Main.WINDOW_SIZE_Y / 2), "Go to Menu!", OriginMode.Center);
			menuBtn.OnClick += (s, e) => { Main.Instance.SceneManager.SwitchSceneEmpty(ActiveScene.Menu); };

			restartGameBtn = new Button(new Point(Main.WINDOW_SIZE_X / 2, Main.WINDOW_SIZE_Y / 2 + 100), "Restart!", OriginMode.Center);
			restartGameBtn.OnClick += (s, e) => { Main.Instance.SceneManager.SwitchSceneEmpty(ActiveScene.Game); };
		}

		public override void Update(GameTime time) {
			menuBtn.Update(time);
			restartGameBtn.Update(time);
		}

		public override void Draw(GameTime time, SpriteBatch batch) {
			menuBtn.Draw(time, batch);
			restartGameBtn.Draw(time, batch);
		}
	}
}
