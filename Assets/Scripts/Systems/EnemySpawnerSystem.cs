using NUnit.Framework;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

namespace DOTS2D
{
    public partial class EnemySpawnerSystem : SystemBase
    {
        private EnemySpawnerComponent enemySpawnerComponent;
        private EnemyDataContainer enemyDataContainerComponent;
        private Entity enemySpawnerEntity;
        private float nextSpawnTime;

        protected override void OnCreate()
        {
            
        }

        protected override void OnUpdate()
        {
            if (SystemAPI.Time.ElapsedTime > nextSpawnTime) 
            {
                // We should only have one entity with an EnemySpawnerComponent
                if (!SystemAPI.TryGetSingletonEntity<EnemySpawnerComponent>(out enemySpawnerEntity))
                {
                    return;
                }

                enemySpawnerComponent = EntityManager.GetComponentData<EnemySpawnerComponent>(enemySpawnerEntity);
                enemyDataContainerComponent = EntityManager.GetComponentData<EnemyDataContainer>(enemySpawnerEntity);

                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            // This is the current game level, TODO: make this dynamic
            int level = 2;
            List<EnemyData> availableEnemeis = new List<EnemyData>();

            // Add all possible enemies for each level we are on
            foreach (EnemyData enemyData in enemyDataContainerComponent.enemies)
            {
                if (enemyData.level <= level)
                {
                    availableEnemeis.Add(enemyData);
                }
            }

            int spawnIndex = 0; // TODO: make random in the future
            Entity newEnemy = EntityManager.Instantiate(availableEnemeis[spawnIndex].prefab);
            EntityManager.SetComponentData(newEnemy, new LocalTransform
            {
                Position = float3.zero,
                Rotation = quaternion.identity,
                Scale = 1
            });

            nextSpawnTime = (float)SystemAPI.Time.ElapsedTime + enemySpawnerComponent.spawnCooldown;
        }

    }
}
