using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame {
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game : Microsoft.Xna.Framework.Game {
		private GraphicsDeviceManager GDManager { get; }
		private SpriteBatch Renderer { get; set; }

		public const int WINDOW_SIZE_X = 1000;
		public const int WINDOW_SIZE_Y = 1000;

		private World world;

		public static Dictionary<string, Texture2D> Sprites { get; } = new Dictionary<string, Texture2D>();
		public static SpriteFont Font { get; set; }

		public Game() {
			GDManager = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
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
			Sprites.Add("pacman1", Content.Load<Texture2D>("pacman1"));
			Sprites.Add("pacman2", Content.Load<Texture2D>("pacman2"));
			Sprites.Add("pacman3", Content.Load<Texture2D>("pacman3"));
			Sprites.Add("pacman4", Content.Load<Texture2D>("pacman4"));
			Sprites.Add("wall", Content.Load<Texture2D>("wall"));
			Sprites.Add("food", Content.Load<Texture2D>("food"));
			Sprites.Add("creep", Content.Load<Texture2D>("creep"));
			Font = Content.Load<SpriteFont>("font");
			OnLoad();
		}

		private void OnLoad() {
			world = new World();
		}

		protected override void Update(GameTime gameTime) {
			world.Update(gameTime);
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime) {
			GraphicsDevice.Clear(Color.Black);
			Renderer.Begin();
			world.Draw(gameTime, Renderer);
			base.Draw(gameTime);
			Renderer.End();
		}
	}
}
