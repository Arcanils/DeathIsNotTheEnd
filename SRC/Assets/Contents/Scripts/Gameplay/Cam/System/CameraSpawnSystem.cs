using Gameplay.Cam.Component;
using Gameplay.Common.Component;
using Gameplay.View.Component;
using Unity.Collections;
using Unity.Entities;

namespace Gameplay.Cam.System
{
	public partial struct CameraSpawnSystem : ISystem
	{
		public void OnCreate(ref SystemState state)
		{
			var newEntity = state.EntityManager.CreateEntity();
			var buffer = state.EntityManager
				.AddBuffer<CameraSpawnRequestElement>(newEntity);
		}

		public void OnUpdate(ref SystemState state)
		{
			if (SystemAPI.HasSingleton<Camera>())
				return;

			var camConfig = SystemAPI.GetSingleton<CamConfig>();

			foreach (var requests in
				SystemAPI.Query<DynamicBuffer<CameraSpawnRequestElement>>())
			{
				var ecb = new EntityCommandBuffer(Allocator.Temp);

				foreach (var request in requests)
				{
					var newCamEntity = ecb.CreateEntity();
					var viewEntity = ecb.Instantiate(camConfig.Prefab);
					ecb.AddComponent(newCamEntity, new Camera());
					ecb.AddComponent(newCamEntity, new Position());
					ecb.AddComponent(newCamEntity, new ViewEnable()
					{
						TargetEntity = viewEntity,
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