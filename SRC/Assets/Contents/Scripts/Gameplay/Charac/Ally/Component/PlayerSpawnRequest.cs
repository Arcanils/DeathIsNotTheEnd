using Gameplay.Common.Component;
using Unity.Entities;

namespace Gameplay.Charac.Ally.Component
{
	public struct PlayerSpawnRequest : IComponentData
	{
		public Position Pos;
	}
}