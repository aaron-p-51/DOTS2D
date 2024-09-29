using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;



using Random = Unity.Mathematics.Random;
using UnityEngine;

namespace DOTS2D
{
    public partial class EnemySpawnerSystem : SystemBase
    {
        private EnemySpawnerComponent enemySpawnerComponent;
        private EnemyDataContainer enemyDataContainerComponent;
        private Entity enemySpawnerEntity;
        private float nextSpawnTime;
        private Random random;

        protected override void OnCreate()
        {
            uint Seed = (uint)enemySpawnerComponent.GetHashCode();
            Debug.Log($"Random Seed: {Seed}");
            random = Random.CreateFromIndex(Seed);
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

            int spawnIndex = random.NextInt(availableEnemeis.Count);
            if (spawnIndex < 0) return;

            Entity newEnemy = EntityManager.Instantiate(availableEnemeis[spawnIndex].prefab);
            EntityManager.SetComponentData(newEnemy, new LocalTransform
            {
                Position = GetPositionOutsideOfCameraView(),
                Rotation = quaternion.identity,
                Scale = 1
            });

            EntityManager.AddComponentData(newEnemy, new EnemyComponent
            {
                currentHealth = availableEnemeis[spawnIndex].health
            });

            nextSpawnTime = (float)SystemAPI.Time.ElapsedTime + enemySpawnerComponent.spawnCooldown;
        }

        private float3 GetPositionOutsideOfCameraView()
        {
            // Get random initial position twice as big as the camera size
            float3 position = new float3(random.NextFloat2(-enemySpawnerComponent.cameraSize * 2f, enemySpawnerComponent.cameraSize * 2f), 0);

            // If position is inside camera size then get a new position
            while (position.x < enemySpawnerComponent.cameraSize.x && position.x > -enemySpawnerComponent.cameraSize.x
                && position.y < enemySpawnerComponent.cameraSize.y && position.y > -enemySpawnerComponent.cameraSize.y)
            {
                position = new float3(random.NextFloat2(-enemySpawnerComponent.cameraSize * 2f, enemySpawnerComponent.cameraSize * 2f), 0);
            }

            position += new float3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0f);
            return position;
        }

    }
}
