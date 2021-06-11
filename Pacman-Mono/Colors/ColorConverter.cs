﻿using System;
using Microsoft.Xna.Framework;

namespace MonoGame.Colors {
	public static class ColorConverter {

		public static Color ColorFromHue(double h) {
			HsvToRgb(h, 1, 1, out int r, out int g, out int b);
			return new Color(r, g, b);
		}

		public static void HsvToRgb(double h, double s, double v, out int r, out int g, out int b) {
			double H = h % 360;
			double R, G, B;
			if (v <= 0) { R = G = B = 0; }
			else if (s <= 0) {
				R = G = B = v;
			}
			else {
				double hf = H / 60.0;
				int i = (int)Math.Floor(hf);
				double f = hf - i;
				double pv = v * (1 - s);
				double qv = v * (1 - s * f);
				double tv = v * (1 - s * (1 - f));
				switch (i) {
					// Red is the dominant color
					case 0:
						R = v;
						G = tv;
						B = pv;
						break;
					// Green is the dominant color
					case 1:
						R = qv;
						G = v;
						B = pv;
						break;
					case 2:
						R = pv;
						G = v;
						B = tv;
						break;
					// Blue is the dominant color
					case 3:
						R = pv;
						G = qv;
						B = v;
						break;
					case 4:
						R = tv;
						G = pv;
						B = v;
						break;
					// Red is the dominant color
					case 5:
						R = v;
						G = pv;
						B = qv;
						break;
					// Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.
					case 6:
						R = v;
						G = tv;
						B = pv;
						break;
					case -1:
						R = v;
						G = pv;
						B = qv;
						break;
					default:
						R = G = B = v; // Just pretend its black/white
						break;
				}
			}
			r = Clamp((int)(R * 255.0));
			g = Clamp((int)(G * 255.0));
			b = Clamp((int)(B * 255.0));
		}

		/// <summary>
		/// Clamp a value to 0-255
		/// </summary>
		private static int Clamp(int i) {
			if (i < 0) { return 0; }
			return i > 255 ? 255 : i;
		}
	}
}
