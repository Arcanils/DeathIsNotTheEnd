using Unity.Entities;
using UnityEngine;

namespace Gameplay.View.Component
{
	public struct ViewEnable : IEnableableComponent, IComponentData
	{
		public Entity TargetEntity;
	}
}