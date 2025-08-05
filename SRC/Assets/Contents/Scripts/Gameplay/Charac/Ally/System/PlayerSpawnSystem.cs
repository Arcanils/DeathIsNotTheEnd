using Gameplay.Cam.Component;
using Gameplay.Charac.Ally.Component;
using Gameplay.Common.Component;
using Gameplay.View.Component;
using Unity.Collections;
using Unity.Entities;

namespace Gameplay.Charac.Ally.System
{
	public partial struct PlayerSpawnSystem : ISystem
	{
		public void OnCreate(ref SystemState state)
		{
			var newEntity = state.EntityManager.CreateEntity();
			var buffer = state.EntityManager
				.AddBuffer<PlayerSpawnRequestElement>(newEntity);
		}

		public void OnUpdate(ref SystemState state)
		{
			if (SystemAPI.HasSingleton<Player>())
				return;

			var playerConfig = SystemAPI.GetSingleton<PlayerConfig>();

			foreach (var requests in 
				SystemAPI.Query<DynamicBuffer<PlayerSpawnRequestElement>>())
			{
				var ecb = new EntityCommandBuffer(Allocator.Temp);

				foreach (var request in requests)
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

					break;
				}

				requests.Clear();

				ecb.Playback(state.EntityManager);
				ecb.Dispose();
			}
		}
	}
}