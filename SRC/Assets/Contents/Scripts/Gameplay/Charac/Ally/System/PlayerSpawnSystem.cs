using Gameplay.Cam.Component;
using Gameplay.Charac.Ally.Authoring;
using Gameplay.Charac.Ally.Component;
using Gameplay.Common.Component;
using Gameplay.GM.Sys;
using Gameplay.View.Component;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;

namespace Gameplay.Charac.Ally.System
{
	[RequireMatchingQueriesForUpdate()]
	[UpdateAfter(typeof(StartGameSystem))]
	public partial struct PlayerSpawnSystem : ISystem
	{
		public void OnUpdate(ref SystemState state)
		{
			var playerConfigEntity = SystemAPI.GetSingletonEntity<PlayerConfig>();
			var playerConfig = SystemAPI.GetComponent<PlayerConfig>(playerConfigEntity);
			var inputConfig = state.EntityManager
				.GetComponentObject<PlayerInputSettings>(playerConfigEntity);
			var ecb = new EntityCommandBuffer(Allocator.Temp);

			foreach (var request in SystemAPI.Query<PlayerSpawnRequest>())
			{
				var newPlayerEntity = ecb.CreateEntity();
				var playerViewEntity = ecb.Instantiate(playerConfig.Prefab);
				ecb.AddComponent(newPlayerEntity, new Player());
				ecb.AddComponent(newPlayerEntity, request.Pos);
				ecb.AddComponent(newPlayerEntity, new Direction());
				ecb.AddComponent(newPlayerEntity, new Speed()
				{
					Val = 1f
				});

				ecb.AddComponent(newPlayerEntity, new CameraTarget()
				{
					Weight = 100,
				});

				ecb.AddComponent(newPlayerEntity, new ViewEnable()
				{
					TargetEntity = ecb.Instantiate(playerViewEntity)
				});

				ecb.AddComponent(newPlayerEntity, new PhysicsVelocity());
				ecb.AddComponent(newPlayerEntity, new PhysicsDamping());
				ecb.AddComponent(newPlayerEntity, new PhysicsCollider());
				ecb.AddComponent(newPlayerEntity, new PhysicsMass());
				ecb.AddSharedComponent(newPlayerEntity, new PhysicsWorldIndex());

				ecb.AddComponent(newPlayerEntity, inputConfig);
			}

			ecb.Playback(state.EntityManager);
			ecb.Dispose();
		}
	}
}