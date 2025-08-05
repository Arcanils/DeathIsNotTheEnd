using Unity.Entities;

namespace Gameplay.Charac.Ally.Component
{
	public struct PlayerConfig : IComponentData
	{
		public Entity Prefab;
	}
}