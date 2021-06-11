using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Scenes.Base;
using MonoGame.Scenes.Enum;
using MonoGame.Structures;
using MonoGame.UI.Controls;
using MonoGame.UI.Enums;

namespace MonoGame.Scenes {
	public class PostGameScene : Scene {
		public override ActiveScene SceneName => ActiveScene.PostGame;

		private readonly Button menuBtn;
		private readonly Button restartGameBtn;

		public PostGameData Data { get; }

		public PostGameScene(PostGameData data) {
			Data = data;

			menuBtn = new Button(new Point(Game.WINDOW_SIZE_X / 2, Game.WINDOW_SIZE_Y / 2), "Go to Menu!", OriginMode.Center);
			menuBtn.OnClick += (s, e) => { Game.Instance.SceneManager.SwitchSceneEmpty(ActiveScene.Menu); };

			restartGameBtn = new Button(new Point(Game.WINDOW_SIZE_X / 2, Game.WINDOW_SIZE_Y / 2 + 100), "Restart!", OriginMode.Center);
			restartGameBtn.OnClick += (s, e) => { Game.Instance.SceneManager.SwitchSceneEmpty(ActiveScene.Game); };
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
