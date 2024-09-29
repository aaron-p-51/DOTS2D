using UnityEngine;
using Unity.Entities;

namespace DOTS2D
{
	public class GameStateAuthoring : MonoBehaviour
	{
		private class GameStateBaker : Baker<GameStateAuthoring>
		{
            public override void Bake(GameStateAuthoring authoring)
            {
                Entity gameStateEntity = GetEntity(TransformUsageFlags.None);

				AddComponent(gameStateEntity, new GameStateComponent
				{
					score = 0
				});
            }
		}
	}
}
