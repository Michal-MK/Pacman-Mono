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

		public static Random Random { get; } = new Random();

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
			Sprites.Add(Player.TEXTURE_ID + "1", Content.Load<Texture2D>(Player.TEXTURE_ID + "1"));
			Sprites.Add(Player.TEXTURE_ID + "2", Content.Load<Texture2D>(Player.TEXTURE_ID + "2"));
			Sprites.Add(Player.TEXTURE_ID + "3", Content.Load<Texture2D>(Player.TEXTURE_ID + "3"));
			Sprites.Add(Player.TEXTURE_ID + "4", Content.Load<Texture2D>(Player.TEXTURE_ID + "4"));
			Sprites.Add(Ghost.TEXTURE_ID_SHAPE, Content.Load<Texture2D>(Ghost.TEXTURE_ID_SHAPE));
			Sprites.Add(Ghost.TEXTURE_ID_EYES, Content.Load<Texture2D>(Ghost.TEXTURE_ID_EYES));
			Sprites.Add(Wall.TEXTURE_ID, Content.Load<Texture2D>(Wall.TEXTURE_ID));
			Sprites.Add(Energizer.TEXTURE_ID, Content.Load<Texture2D>(Energizer.TEXTURE_ID));
			Sprites.Add(Food.TEXTURE_ID, Content.Load<Texture2D>(Food.TEXTURE_ID));
			Sprites.Add(Creep.TEXTURE_ID, Content.Load<Texture2D>(Creep.TEXTURE_ID));
			Sprites.Add(CreepSpawn.TEXTURE_ID, Content.Load<Texture2D>(CreepSpawn.TEXTURE_ID));
			Font = Content.Load<SpriteFont>("font");
			OnLoad();
		}

		private void OnLoad() {
			world = new World(WorldDefinitions.LARGE_WORLD_19x19);
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
