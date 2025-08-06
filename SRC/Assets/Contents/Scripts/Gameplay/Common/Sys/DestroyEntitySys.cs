using Gameplay.Common.Component;
using Unity.Collections;
using Unity.Entities;

namespace Gameplay.Common.Sys
{
	[UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
	public partial struct DestroyEntitySys : ISystem
	{
		public void OnUpdate(ref SystemState state)
		{
			var ecb = new EntityCommandBuffer(Allocator.Temp);
			foreach (var (_, entity) in 
				SystemAPI.Query<DestroyEntity>().WithEntityAccess())
			{
				ecb.DestroyEntity(entity);
			}

			ecb.Playback(state.EntityManager);
			ecb.Dispose();
		}
	}
}