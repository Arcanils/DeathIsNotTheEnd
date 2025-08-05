using Gameplay.Common.Component;
using Gameplay.View.Component;
using Unity.Entities;
using Unity.Transforms;

namespace Gameplay.View.System
{
	public partial struct UpdateViewSystem : ISystem
	{
		public void OnUpdate(ref SystemState state)
		{
			foreach (var (pos, view) in
				SystemAPI.Query<Position, ViewEnable>()
				.WithAny<ViewEnable>())
			{
				var targetEntity = view.TargetEntity;
				var local = SystemAPI.GetComponentRW<LocalTransform>(targetEntity);
				local.ValueRW.Position.x = pos.Val.x;
				local.ValueRW.Position.y = pos.Val.y;
			}
		}
	}
}