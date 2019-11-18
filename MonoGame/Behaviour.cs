﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame {
	public abstract class Behaviour {

		public abstract Vector2 Position { get; set; }

		public abstract void Update(GameTime time);

		public abstract void Draw(GameTime time, SpriteBatch batch);
	}
}
