using System;
using System.Collections.Generic;

namespace func_rocket
{
	public class LevelsTask
	{
		static readonly Physics standardPhysics = new Physics();
		static readonly Dictionary<string, Level> levels = new Dictionary<string, Level>
		{
			{ "Zero", CreateZeroLvl() },
			{ "Heavy", CreateHeavyLvl() },
			{ "Up", CreateUpLvl() },
			{ "WhiteHole", CreateWhiteHoleLvl() },
			{ "BlackHole", CreateBlackHoleLvl() },
			{ "BlackAndWhite",  CreateBlackAndWhiteLvl() },
		};

		private static Level CreateZeroLvl()
		{
			return new Level("Zero",
				new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
				new Vector(600, 200),
				(size, v) => Vector.Zero, standardPhysics);
		}

		private static Level CreateHeavyLvl()
		{
			return new Level("Heavy",
				new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
				new Vector(600, 200),
				(size, v) => new Vector(0, 0.9), standardPhysics);
		}

		private static Level CreateUpLvl()
		{
			return new Level("Up",
				new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
				new Vector(700, 500),
				(size, v) => new Vector(0, -300 / (size.Height - v.Y + 300)), standardPhysics);
		}

		private static Level CreateWhiteHoleLvl()
		{
			return new Level("WhiteHole",
				new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
				new Vector(600, 200),
				(size, v) =>
				{
					var vectorToWH = new Vector(v.X - 600, v.Y - 200);
					var distanceToWH = vectorToWH.Length;
					return vectorToWH.Normalize() *
					140 * distanceToWH / (distanceToWH * distanceToWH + 1);
				},
				standardPhysics);
		}

		private static Level CreateBlackHoleLvl()
		{
			return new Level("BlackHole",
				new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
				new Vector(600, 200),
				(size, v) =>
				{
					var positionBH = new Vector(200 + 600, 500 + 200) / 2;
					var vectorToBH = positionBH - v;
					var distanceToBH = vectorToBH.Length;
					return vectorToBH.Normalize() *
					300 * distanceToBH / (distanceToBH * distanceToBH + 1);
				},
				standardPhysics);
		}

		private static Level CreateBlackAndWhiteLvl()
		{
			return new Level("BlackAndWhite",
				new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
				new Vector(600, 200),
				(size, v) =>
				{
					var vectorToWH = new Vector(v.X - 600, v.Y - 200);
					var distanceToWH = vectorToWH.Length;
					var positionBH = new Vector(200 + 600, 500 + 200) / 2;
					var vectorToBH = positionBH - v;
					var distanceToBH = vectorToBH.Length;
					return (vectorToWH.Normalize() *
					140 * distanceToWH / (distanceToWH * distanceToWH + 1) +
					vectorToBH.Normalize() *
					300 * distanceToBH / (distanceToBH * distanceToBH + 1)) / 2;
				},
				standardPhysics);
		}

		public static IEnumerable<Level> CreateLevels()
		{
			foreach (var level in levels.Keys)
				yield return levels[level];
		}
	}
}