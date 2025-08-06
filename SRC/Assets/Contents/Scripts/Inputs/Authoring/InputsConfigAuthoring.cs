using System;
using Unity.Entities;
using UnityEngine;

namespace Inputs.Authoring
{
	public class InputsConfigAuthoring : MonoBehaviour
	{
		public InputsConfigSettings Config;

		private class Baker : Baker<InputsConfigAuthoring>
		{
			public override void Bake(InputsConfigAuthoring authoring)
			{
				var entity = GetEntity(TransformUsageFlags.None);
				AddComponentObject(entity, authoring.Config);
			}
		}
	}

	[Serializable]
	public class InputsConfigSettings
	{

	}
}