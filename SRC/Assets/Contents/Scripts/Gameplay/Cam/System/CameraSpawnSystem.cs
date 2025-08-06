using Gameplay.Cam.Component;
using Gameplay.Common.Component;
using Gameplay.GM.Sys;
using Gameplay.View.Component;
using Unity.Collections;
using Unity.Entities;

namespace Gameplay.Cam.System
{
	[RequireMatchingQueriesForUpdate()]
	[UpdateAfter(typeof(StartGameSystem))]
	public partial struct CameraSpawnSystem : ISystem
	{
		public void OnUpdate(ref SystemState state)
		{
			//var camConfig = SystemAPI.GetSingleton<CamConfig>();
			var ecb = new EntityCommandBuffer(Allocator.Temp);

			foreach (var request in SystemAPI.Query<CameraSpawnRequest>())
			{
				var newCamEntity = ecb.CreateEntity();
				ecb.AddComponent(newCamEntity, new Camera());
				ecb.AddComponent(newCamEntity, new Position());
				ecb.AddComponent(newCamEntity, UnityEngine.Camera.main);
			}

			ecb.Playback(state.EntityManager);
			ecb.Dispose();
		}
	}
}