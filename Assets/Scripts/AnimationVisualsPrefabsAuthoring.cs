using UnityEngine;
using Unity.Entities;

namespace DOTS2D
{
	public class AnimationVisualsPrefabsAuthoring : MonoBehaviour
	{
		[SerializeField] private GameObject playerPrefab;
		[SerializeField] private GameObject enemySkeletonPrefab;

		private class AnimationVisualsPrefabsBaker : Baker<AnimationVisualsPrefabsAuthoring>
		{
			public override void Bake(AnimationVisualsPrefabsAuthoring authoring)
			{
				Entity authoringEntity = GetEntity(TransformUsageFlags.None);

				AddComponentObject(authoringEntity, new AnimationVisualsPrefabs
				{
					player = authoring.playerPrefab,
					enemySkeleton = authoring.enemySkeletonPrefab
				});
			}
        }
	}
}
