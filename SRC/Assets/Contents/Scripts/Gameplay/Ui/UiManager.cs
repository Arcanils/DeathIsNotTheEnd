using Gameplay.Common.Component;
using Gameplay.GM.Component;
using Unity.Entities;
using UnityEngine;

namespace Assets.Contents.Scripts.Gameplay.Ui
{
	public class UiManager : MonoBehaviour
	{
		public static UiManager Instance { get; private set; }

		public void Awake()
		{
			Instance = this;
		}

		public void Start()
		{
			StartGame();
		}

		public void StartGame()
		{
			var em = World.DefaultGameObjectInjectionWorld.EntityManager;
			var newEntity = em.CreateEntity();
			em.AddComponent(newEntity, typeof(StartGameRequest));
			em.AddComponent(newEntity, typeof(DestroyEntity));
		}
	}
}