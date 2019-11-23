﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame {
	public static class WorldDefinitions {

		public static readonly char[,] DEFAULT_WORLD_9x9 = new char[9, 9] {
			{ 'W','W','W','W','W','W','W','W','W'},
			{ 'W','_','*','_','*','_','_','C','W'},
			{ 'W','G','W','_','_','_','_','W','W'},
			{ 'W','_','_','_','_','W','_','_','W'},
			{ 'W','_','_','W','P','_','_','*','W'},
			{ 'W','*','_','_','*','_','_','_','W'},
			{ 'W','_','W','_','_','_','W','_','W'},
			{ 'W','_','W','*','_','_','*','G','W'},
			{ 'W','W','W','W','W','W','W','W','W'},
		};

		public static readonly char[,] SQUISHED_WORLD_7x9 = new char[7, 9] {
			{ 'W','W','W','W','W','W','W','W','W'},
			{ 'W','_','*','_','*','_','_','C','W'},
			{ 'W','G','W','_','_','_','_','W','W'},
			{ 'W','_','_','_','_','W','_','_','W'},
			{ 'W','_','_','W','P','_','_','*','W'},
			{ 'W','_','W','*','_','_','*','G','W'},
			{ 'W','W','W','W','W','W','W','W','W'},
		};

		public static readonly char[,] LARGE_WORLD_19x19 = new char[19, 19] {
			{ 'W','W','W','W','W','W','W','W','W','W','W','W','W','W','W','W','W','W','W'},
			{ 'W','_','*','_','*','_','_','C','W','W','_','*','_','*','G','_','_','_','W'},
			{ 'W','_','W','_','_','_','W','W','_','_','_','W','_','_','_','_','W','_','W'},
			{ 'W','_','_','_','_','W','_','_','_','W','_','_','_','_','W','_','_','_','W'},
			{ 'W','_','_','_','_','_','_','*','_','_','_','_','W','_','_','_','*','_','W'},
			{ 'W','*','W','_','*','_','_','_','_','_','*','_','_','*','_','_','_','W','W'},
			{ 'W','_','W','_','_','_','W','_','_','_','_','W','_','_','_','W','_','_','W'},
			{ 'W','_','W','*','_','_','*','_','_','W','_','W','*','_','_','*','_','_','W'},
			{ 'W','_','W','W','_','_','W','_','_','W','_','_','W','W','_','_','W','_','W'},
			{ 'W','G','W','_','_','W','W','W','_','W','_','_','_','W','W','E','W','_','W'},
			{ 'W','_','*','_','*','_','_','_','_','_','_','*','_','*','_','_','_','_','W'},
			{ 'W','_','W','_','_','_','_','W','_','_','_','W','_','_','_','_','W','_','W'},
			{ 'W','E','_','_','_','W','_','_','_','_','_','_','_','_','W','_','_','_','W'},
			{ 'W','_','_','W','_','_','_','*','W','_','_','_','W','P','_','_','*','_','W'},
			{ 'W','*','_','_','*','_','_','_','W','_','*','_','_','*','_','_','_','_','W'},
			{ 'W','W','W','_','_','_','W','_','W','W','_','W','_','_','_','W','_','G','W'},
			{ 'W','*','W','*','_','_','*','_','W','_','_','W','*','_','_','*','_','_','W'},
			{ 'W','_','G','_','_','_','W','_','_','_','_','_','_','_','W','_','_','_','W'},
			{ 'W','W','W','W','W','W','W','W','W','W','W','W','W','W','W','W','W','W','W'}
		};
	}
}
