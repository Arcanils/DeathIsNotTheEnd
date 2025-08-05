using Gameplay.Common.Component;
using Unity.Entities;

namespace Gameplay.Charac.Ally.Component
{
	public struct PlayerSpawnRequestElement : IBufferElementData
	{
		public Position Pos;
	}
}