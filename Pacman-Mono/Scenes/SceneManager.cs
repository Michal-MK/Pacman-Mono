using System;
using Pacman.Scenes.Enum;
using Pacman.Structures;

namespace Pacman.Scenes {
	public class SceneManager {
		public ActiveScene ActiveScene { get; set; }

		public void SwitchSceneEmpty(ActiveScene scene) {
			switch (scene) {
				case ActiveScene.Menu: {
					Main.Instance.CurrentScene = new MenuScene();
					return;
				}
				case ActiveScene.Game: {
					Main.Instance.CurrentScene = new GameScene();
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
			Main.Instance.CurrentScene = new PostGameScene(data);
			ActiveScene = ActiveScene.PostGame;
		}
	}
}
