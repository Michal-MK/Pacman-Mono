﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using Pacman.Behaviours;
using Pacman.Scenes;
using Pacman.Scenes.Enum;
using Pacman.UI.Controls;

namespace Pacman {
	public class Main : Game {
		private GraphicsDeviceManager GDManager { get; }
		private SpriteBatch Renderer { get; set; }

		public const int WINDOW_SIZE_X = 1000;
		public const int WINDOW_SIZE_Y = 1000;

		public static Random Random { get; } = new();

		public static Dictionary<string, Texture2D> Sprites { get; } = new();
		public static Dictionary<string, Effect> Shaders { get; } = new();
		public static SpriteFont Font { get; private set; }
		public static BitmapFont NewFont { get; set; }

		public static Main Instance { get; private set; }
		public SceneManager SceneManager { get; private set; }

		public Main() {
			GDManager = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			Instance = this;
		}

		protected override void Initialize() {
			IsMouseVisible = true;
			GDManager.SynchronizeWithVerticalRetrace = true;
			GDManager.PreferredBackBufferWidth = WINDOW_SIZE_X;
			GDManager.PreferredBackBufferHeight = WINDOW_SIZE_Y;

			IsFixedTimeStep = true;
			GDManager.ApplyChanges();
			base.Initialize();
		}

		protected override void LoadContent() {
			Renderer = new SpriteBatch(GraphicsDevice);
			Load<Texture2D>(Player.TEXTURE_ID + "1");
			Load<Texture2D>(Player.TEXTURE_ID + "2");
			Load<Texture2D>(Player.TEXTURE_ID + "3");
			Load<Texture2D>(Player.TEXTURE_ID + "4");
			Load<Texture2D>(Ghost.TEXTURE_ID_SHAPE);
			Load<Texture2D>(Ghost.TEXTURE_ID_EYES);
			Load<Texture2D>(Wall.TEXTURE_ID);
			Load<Texture2D>(Energizer.TEXTURE_ID);
			Load<Texture2D>(Food.TEXTURE_ID);
			Load<Texture2D>(Creep.TEXTURE_ID);
			Load<Texture2D>(Bonus.TEXTURE_ID);
			Load<Texture2D>(CreepSpawn.TEXTURE_ID);

			Load<Texture2D>(Button.TEXTURE_ID);

			Load<Effect>(Player.POWERUP_SHADER_ID);
			Font = Content.Load<SpriteFont>("font");
			NewFont = Content.Load<BitmapFont>("fonts/new_font");

			void Load<T>(string path) {
				if (typeof(T) == typeof(Texture2D)) Sprites.Add(path, Content.Load<Texture2D>(path));
				if (typeof(T) == typeof(Effect)) Shaders.Add(path, Content.Load<Effect>(path));
			}

			OnLoad();
		}

		private void OnLoad() {
			SceneManager = new SceneManager();
			SceneManager.SwitchSceneEmpty(ActiveScene.Menu);
		}

		protected override void Update(GameTime gameTime) {
			SceneManager.CurrentScene.Update(gameTime);
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime) {
			GraphicsDevice.Clear(Color.Black);
			Renderer.Begin();

			SceneManager.CurrentScene.Draw(gameTime, Renderer);
			base.Draw(gameTime);

			Renderer.End();
		}
	}
}