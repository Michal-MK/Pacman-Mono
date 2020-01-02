using System;

namespace MonoGame {
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
					throw new NotImplementedException("Post game does not exist yet!");
				}
			}
		}

		public void SwitchToPostGame(PostGameData data) {
			Game.Instance.CurrentScene = new PostGameScene(data);
		}
	}
}
