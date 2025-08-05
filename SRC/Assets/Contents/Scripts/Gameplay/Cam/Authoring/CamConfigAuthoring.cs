using Gameplay.Cam.Component;
using System;
using Unity.Entities;
using UnityEngine;

namespace Gameplay.Cam.Authoring
{
	public class CamConfigAuthoring : MonoBehaviour
	{
		public CamConfigSettings Config;

		private class Baker : Baker<CamConfigAuthoring>
		{
			public override void Bake(CamConfigAuthoring authoring)
			{
				var entity = GetEntity(TransformUsageFlags.None);
				AddComponent(entity, new CamConfig()
				{
					Prefab = GetEntity(authoring.Config.Prefab,
						TransformUsageFlags.Dynamic)
				});
			}
		}
	}

	[Serializable]
	public class CamConfigSettings
	{
		public GameObject Prefab;
	}
}