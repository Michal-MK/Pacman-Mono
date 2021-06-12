using System;
using Pacman.Scenes.Base;
using Pacman.Scenes.Enum;
using Pacman.Structures;

namespace Pacman.Scenes {
	public class SceneManager {

		public Scene CurrentScene { get; private set; }

		public void SwitchSceneEmpty(ActiveScene scene) {
			switch (scene) {
				case ActiveScene.Menu: {
					CurrentScene = new MenuScene();
					return;
				}
				case ActiveScene.Game: {
					CurrentScene = new GameScene();
					return;
				}
				case ActiveScene.PostGame: {
					throw new NotImplementedException($"Post game needs additional data," +
													  $" use '{nameof(SwitchToPostGame)}({nameof(PostGameData)} data)' function.");
				}
			}
		}

		public void SwitchToPostGame(PostGameData data) {
			CurrentScene = new PostGameScene(data);
		}
	}
}
