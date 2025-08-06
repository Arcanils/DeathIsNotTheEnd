using Gameplay.Cam.Component;
using Gameplay.Charac.Ally.Component;
using Gameplay.Common.Component;
using Gameplay.GM.Component;
using Unity.Collections;
using Unity.Entities;

namespace Gameplay.GM.Sys
{
	[RequireMatchingQueriesForUpdate()]
	public partial struct StartGameSystem : ISystem
	{
		public void OnUpdate(ref SystemState state)
		{
			var ecb = new EntityCommandBuffer(Allocator.Temp);

			foreach (var request in SystemAPI.Query<StartGameRequest>())
			{
				var playerRequestEntity = ecb.CreateEntity();
				ecb.AddComponent(playerRequestEntity, new PlayerSpawnRequest()
				{
					Pos = default,
				});
				ecb.AddComponent(playerRequestEntity, new DestroyEntity());

				var camRequestEntity = ecb.CreateEntity();
				ecb.AddComponent(camRequestEntity, new CameraSpawnRequest());
				ecb.AddComponent(camRequestEntity, new DestroyEntity());
			}

			ecb.Playback(state.EntityManager);
			ecb.Dispose();
		}
	}
}