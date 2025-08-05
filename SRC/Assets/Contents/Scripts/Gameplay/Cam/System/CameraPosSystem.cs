using Gameplay.Cam.Component;
using Gameplay.Common.Component;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace Gameplay.Cam.System
{
	public partial struct CameraPosSystem : ISystem
	{
		public void OnCreate(ref SystemState state)
		{
			state.RequireForUpdate<Camera>();
		}

		public void OnUpdate(ref SystemState state)
		{
			var camEntity = SystemAPI.GetSingletonEntity<Camera>();
			var endPos = float2.zero;
			var totalWeight = 0;
			foreach (var (pos, camTarget) in SystemAPI.Query<Position, CameraTarget>())
			{
				endPos += pos.Val;
				totalWeight += camTarget.Weight;
			}

			if (totalWeight > 0)
				return;

			state.EntityManager.SetComponentData(camEntity, new Position()
			{
				Val = endPos,
			});
		}
	}
}