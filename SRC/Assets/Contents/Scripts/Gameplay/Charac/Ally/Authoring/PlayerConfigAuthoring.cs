using Gameplay.Cam.Authoring;
using Gameplay.Cam.Component;
using Gameplay.Charac.Ally.Component;
using System;
using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Charac.Ally.Authoring
{
	public class PlayerConfigAuthoring : MonoBehaviour
	{
		public PlayerConfigSettings Config;
		private class Baker : Baker<PlayerConfigAuthoring>
		{
			public override void Bake(PlayerConfigAuthoring authoring)
			{
				var entity = GetEntity(TransformUsageFlags.None);
				AddComponent(entity, new PlayerConfig()
				{
					Prefab = GetEntity(authoring.Config.Prefab,
						TransformUsageFlags.Dynamic)
				});
				AddComponentObject(entity, authoring.Config.InputSettings);
			}
		}
	}

	[Serializable]
	public class PlayerConfigSettings
	{
		public GameObject Prefab;
		public PlayerInputSettings InputSettings;
	}

	[Serializable]
	public class PlayerInputSettings
	{
		public InputActionReference MoveActionRef;
	}

}