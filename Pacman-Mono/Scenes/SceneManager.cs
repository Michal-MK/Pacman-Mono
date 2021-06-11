using System;
using MonoGame.Scenes.Enum;
using MonoGame.Structures;

namespace MonoGame.Scenes {
	public class SceneManager {
		public ActiveScene ActiveScene { get; set; }

		public void SwitchSceneEmpty(ActiveScene scene) {
			switch (scene) {
				case ActiveScene.Menu: {
					Game.Instance.CurrentScene = new MenuScene();
					return;
				}
				case ActiveScene.Game: {
					Game.Instance.CurrentScene = new GameScene();
					return;
				}
				case ActiveScene.PostGame: {
					throw new NotImplementedException($"Post game needs additional data," +
													  $" use '{nameof(SwitchToPostGame)}({nameof(PostGameData)} data)' function.");
				}
			}
			ActiveScene = scene;
		}

		public void SwitchToPostGame(PostGameData data) {
			Game.Instance.CurrentScene = new PostGameScene(data);
			ActiveScene = ActiveScene.PostGame;
		}
	}
}
