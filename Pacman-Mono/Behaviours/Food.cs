﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Behaviours.Base;

namespace MonoGame.Behaviours {
	public class Food : Behaviour {

		public const string TEXTURE_ID = "food";

		public override Vector2 Position { get; protected set; }

		protected override Vector2 Scale { get; set; }

		public Food(Vector2 position) {
			Setup(position, TEXTURE_ID);
		}

		public override void Draw(GameTime time, SpriteBatch batch) => SimpleDraw(time, batch);
	}
}
