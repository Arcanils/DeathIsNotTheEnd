using System.Collections;
using Unity.Entities;
using UnityEngine;

namespace Gameplay.Cam.Component
{
	public struct CamConfig : IComponentData
	{
		public Entity Prefab;
	}
}