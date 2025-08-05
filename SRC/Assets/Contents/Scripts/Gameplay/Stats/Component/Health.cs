using Unity.Entities;

namespace Gameplay.Stats.Component
{
	public struct Health : IComponentData
	{
		public float Curr;
		public float Max;
		public float Perc;
	}
}
