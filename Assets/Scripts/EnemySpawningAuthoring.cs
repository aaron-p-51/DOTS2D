using UnityEngine;
using Unity.Entities;
using System.Collections.Generic;
using Unity.Mathematics;

namespace DOTS2D
{
	public class EnemySpawningAuthoring : MonoBehaviour
	{
		public float spawnCoolDown = 1f;
        public Vector2 cameraSize;
		public List<EnemySO> enemySO;

		public class EnemySpawnerBaker : Baker<EnemySpawningAuthoring>
		{
            public override void Bake(EnemySpawningAuthoring authoring)
            {
                Entity enemySpawningAuthoring = GetEntity(TransformUsageFlags.None);

                AddComponent(enemySpawningAuthoring, new EnemySpawnerComponent
                {
                    spawnCooldown = authoring.spawnCoolDown,
                    cameraSize = authoring.cameraSize,
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

        public void OnDrawGizmosSelected()
        {
            Vector2 center = Camera.main.transform.position;
            DrawDebugBox(center, cameraSize / 2f, Color.red);
        }

        private void DrawDebugBox(Vector2 center, Vector2 halfSize, Color color)
        {
            // Find four corners
            Vector2 topLeft = new Vector2(center.x - halfSize.x, center.y + halfSize.y);
            Vector2 topRight = new Vector2(center.x + halfSize.x, center.y + halfSize.y);
            Vector2 bottomLeft = new Vector2(center.x - halfSize.x, center.y - halfSize.y);
            Vector2 bottomRight = new Vector2(center.x + halfSize.x, center.y - halfSize.y);

            // Draw four sides
            Debug.DrawLine(topLeft, topRight, color);
            Debug.DrawLine(topRight, bottomRight, color);
            Debug.DrawLine(bottomRight, bottomLeft, color);
            Debug.DrawLine(bottomLeft, topLeft, color); 
        }


    }
}
