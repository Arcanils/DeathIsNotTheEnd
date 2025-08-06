using Gameplay.Cam.Component;
using Gameplay.Charac.Ally.Authoring;
using Gameplay.Charac.Ally.Component;
using Gameplay.Common.Component;
using Gameplay.GM.Sys;
using Gameplay.View.Component;
using System.Numerics;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;

namespace Gameplay.Charac.Ally.System
{
	[RequireMatchingQueriesForUpdate()]
	public partial struct PlayerInputSys : ISystem
	{
		public void OnUpdate(ref SystemState state)
		{
			var ecb = new EntityCommandBuffer(Allocator.Temp);

			foreach (var (dir, playerEntity) in SystemAPI.Query<RefRW<Direction>>()
				.WithEntityAccess())
			{
				var inputSettings = state.EntityManager
					.GetComponentObject<PlayerInputSettings>(playerEntity);

				var moveAction = inputSettings.MoveActionRef.action;
				dir.ValueRW.Val = moveAction.ReadValue<float2>();
			}

			ecb.Playback(state.EntityManager);
			ecb.Dispose();
		}
	}
}