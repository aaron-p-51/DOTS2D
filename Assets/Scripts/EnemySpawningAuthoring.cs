using UnityEngine;
using Unity.Entities;
using System.Collections.Generic;
using Unity.Mathematics;

namespace DOTS2D
{
	public class EnemySpawningAuthoring : MonoBehaviour
	{
		public float spawnCoolDown = 1f;
		public List<EnemySO> enemySO;

		public class EnemySpawnerBaker : Baker<EnemySpawningAuthoring>
		{
            public override void Bake(EnemySpawningAuthoring authoring)
            {
                Entity enemySpawningAuthoring = GetEntity(TransformUsageFlags.None);

                AddComponent(enemySpawningAuthoring, new EnemySpawnerComponent
                {
                    spawnCooldown = authoring.spawnCoolDown
                });

                List<EnemyData> enemyData = new List<EnemyData>();
                foreach (EnemySO enemySO in authoring.enemySO)
                {
                    enemyData.Add(new EnemyData
                    {
                        damage = enemySO.damage,
                        health = enemySO.health,
                        level = enemySO.level,
                        moveSpeed = enemySO.moveSpeed,
                        prefab = GetEntity(enemySO.prefab, TransformUsageFlags.None)
                    });
                }

                // We have to use AddComponentObject because EnemyDataContainer is a class, not a struct.
                AddComponentObject(enemySpawningAuthoring, new EnemyDataContainer { enemies = enemyData });
            }
        }
	}
}
