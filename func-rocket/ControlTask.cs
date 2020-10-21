namespace func_rocket
{
	public class ControlTask
	{
		public static Turn ControlRocket(Rocket rocket, Vector target)
		{
			var rocketForce = ForcesTask.GetThrustForce(5)(rocket);
			var vectorToTarget = rocket.Location + rocket.Velocity + rocketForce;
			var totalAngle = (target - rocket.Location).Angle - (vectorToTarget - rocket.Location).Angle;

			if (totalAngle < 0) return Turn.Left;
			if (totalAngle > 0) return Turn.Right;
			return Turn.None;
		}
	}
}