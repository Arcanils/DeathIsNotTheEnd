using Gameplay.Common.Component;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;

namespace Gameplay.Common.Sys
{
	public partial struct MoveDirWithVeloSys : ISystem
	{
		public void OnUpdate(ref SystemState state)
		{
			foreach (var (dir, speed, velocity) in
				SystemAPI.Query<RefRO<Direction>, RefRO<Speed>,
				RefRW<PhysicsVelocity>>())
			{
				var result = dir.ValueRO.Val * speed.ValueRO.Val;
				velocity.ValueRW.Linear = new float3(result, 0f);
			}
		}
	}
}