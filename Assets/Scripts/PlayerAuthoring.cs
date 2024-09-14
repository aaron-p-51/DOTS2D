using UnityEngine;
using Unity.Entities;

namespace DOTS2D
{
	public class PlayerAuthoring : MonoBehaviour
	{
		public float MoveSpeed = 5f;
		public float ShootCooldown = 1f;
		public GameObject bulletPrefab;

		private class PlayerBaker : Baker<PlayerAuthoring>
		{
			public override void Bake(PlayerAuthoring authoring)
			{
				Entity playerEntity = GetEntity(TransformUsageFlags.None);

				AddComponent(playerEntity, new PlayerComponent
				{
					moveSpeed = authoring.MoveSpeed,
					shootCooldown = authoring.ShootCooldown,
					bulletPrefab = GetEntity(authoring.bulletPrefab, TransformUsageFlags.None)
				});
			}
		}
	}
}
