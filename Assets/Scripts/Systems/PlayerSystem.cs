//using System.Numerics;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Unity.Collections;


namespace DOTS2D
{
    // Ensure the bullet prefab does not spawn at 0,0,0 before snapping to intended spawn position
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial struct PlayerSystem : ISystem
    {
        private Entity playerEntity;
        private Entity inputEntity;
        private EntityManager entityManager;
        private PlayerComponent playerComponent;
        private InputComponent inputComponent;
        private float nextShootTime;


        public void OnUpdate(ref SystemState state)
        {
            entityManager = state.EntityManager;
            playerEntity = SystemAPI.GetSingletonEntity<PlayerComponent>();
            inputEntity = SystemAPI.GetSingletonEntity<InputComponent>();
            
            playerComponent = entityManager.GetComponentData<PlayerComponent>(playerEntity);
            inputComponent = entityManager.GetComponentData<InputComponent>(inputEntity);

            Move(ref state);
            Shoot(ref state);
        }

        private void Move(ref SystemState state)
        {
            // Apply input translation
            LocalTransform playerTransform = entityManager.GetComponentData<LocalTransform>(playerEntity);
            playerTransform.Position += new float3(inputComponent.movement * playerComponent.moveSpeed * SystemAPI.Time.DeltaTime, 0f);

            Vector2 direction = (Vector2)inputComponent.mousePos - (Vector2)Camera.main.WorldToScreenPoint(playerTransform.Position);
            float angle = math.degrees(math.atan2(direction.y, direction.x));
            playerTransform.Rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            entityManager.SetComponentData(playerEntity, playerTransform);
        }
        
        private void Shoot(ref SystemState state)
        {
            if (inputComponent.pressingLMB && nextShootTime < SystemAPI.Time.ElapsedTime)
            {
                EntityCommandBuffer ECB = new EntityCommandBuffer(Allocator.Temp);

                LocalTransform playerTransform = entityManager.GetComponentData<LocalTransform>(playerEntity);

                //Entity bulletEntity = entityManager.Instantiate(playerComponent.bulletPrefab);

                //ECB.AddComponent(bulletEntity, new BulletComponent
                //{
                //    velocity = playerTransform.Right() * 10f,
                //    speed = 10f,
                //    size = 0.2f,
                //    lifespan = 1.5f
                //});

                //LocalTransform bulletTransform = entityManager.GetComponentData<LocalTransform>(bulletEntity);
                //bulletTransform.Position = playerTransform.Position + playerTransform.Right() + playerTransform.Up() * -0.35f;
                //bulletTransform.Rotation = playerTransform.Rotation;


                //ECB.SetComponent(bulletEntity, bulletTransform);
                //ECB.Playback(entityManager);
                //ECB.Dispose();

                nextShootTime = (float)SystemAPI.Time.ElapsedTime + playerComponent.shootCooldown;
            }
        }
    }
}
