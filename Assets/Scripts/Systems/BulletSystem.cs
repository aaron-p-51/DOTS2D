using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Unity.Physics;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using Unity.Jobs;

namespace DOTS2D
{
    [BurstCompile]
    public struct BulletMovementJob : IJobParallelFor
    {
        public float deltaTime;

        [ReadOnly] public NativeArray<LocalTransform> bulletTransforms;
        [ReadOnly] public NativeArray<BulletComponent> bulletComponents;

        public NativeArray<float3> updatedPositions;


        public void Execute(int index)
        {
            float3 position = bulletTransforms[index].Position;
            float3 velocity = bulletComponents[index].velocity;

            position += velocity * deltaTime;

            updatedPositions[index] = position;
        }
    }

    [BurstCompile]
    public struct UpdateLocalTransformsJob : IJobParallelFor
    {
        [ReadOnly] public NativeArray<Entity> entities;
        [ReadOnly] public NativeArray<float3> updatedPositions;
        public EntityCommandBuffer.ParallelWriter ecb;

        public void Execute(int index)
        {
            Entity entity = entities[index];


            ecb.SetComponent(index, entity, new LocalTransform
            {
                Position = updatedPositions[index],
                Rotation = quaternion.identity,
                Scale = 1f
            });
        }
    }



    [BurstCompile]
    public partial struct BulletSystem : ISystem
    {
        private Entity gameStateEntity;
        private bool isGameStateCached;

        private PhysicsWorldSingleton physicsWorld;

        private void OnCreate(ref SystemState state)
        {
            isGameStateCached = false;
            //physicsWorld = SystemAPI.GetSingleton<PhysicsWorldSingleton>();
        }

        [BurstCompile]
        private void OnUpdate(ref SystemState state) 
        {
            var bulletQuery = SystemAPI.QueryBuilder().WithAll<BulletComponent, LocalTransform, Poolable>().Build();

            // find the bullets that are active
            var IsActiveComponents = bulletQuery.ToComponentDataArray<Poolable>(Allocator.Temp);
           // var bulletTransforms = new NativeList<LocalTransform>(Allocator.TempJob);
            //var bulletComponents
            foreach (var bullet in IsActiveComponents)
            {
                if (bullet.IsActive)
                {
                    
                }
            }

            



            var bulletTransforms = bulletQuery.ToComponentDataArray<LocalTransform>(Allocator.TempJob);
            var bulletComponent = bulletQuery.ToComponentDataArray<BulletComponent>(Allocator.TempJob);
            var entities = bulletQuery.ToEntityArray(Allocator.TempJob);

            var updatedPositions = new NativeArray<float3>(bulletTransforms.Length, Allocator.TempJob);

            var bulletMovementJob = new BulletMovementJob
            {
                deltaTime = SystemAPI.Time.DeltaTime,
                bulletTransforms = bulletTransforms,
                bulletComponents = bulletComponent,
                updatedPositions = updatedPositions
            };

            JobHandle movementHandle = bulletMovementJob.Schedule(bulletTransforms.Length, 1000, state.Dependency);

            EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);
            var updateTransformJob = new UpdateLocalTransformsJob
            {
                entities = entities,
                updatedPositions = updatedPositions,
                ecb = ecb.AsParallelWriter()
            };

            JobHandle updateHandle = updateTransformJob.Schedule(entities.Length, 1000, movementHandle);

            

            state.Dependency = JobHandle.CombineDependencies(updateHandle, movementHandle);

            updateHandle.Complete();

            ecb.Playback(state.EntityManager);
            ecb.Dispose();

            //state.Dependency = JobHandle.CombineDependencies(state.Dependency, ecb.Playback(state.EntityManager));
            state.Dependency = JobHandle.CombineDependencies(state.Dependency, bulletTransforms.Dispose(state.Dependency), bulletComponent.Dispose(state.Dependency));
            state.Dependency = JobHandle.CombineDependencies(state.Dependency, updatedPositions.Dispose(state.Dependency), entities.Dispose(state.Dependency));

            //state.Dependency = JobHandle.CombineDependencies(
            //    state.Dependency,
            //    bulletTransforms.Dispose(state.Dependency),
            //    bulletComponent.Dispose(state.Dependency),
            //    updatedPositions.Dispose(state.Dependency),
            //    entities.Dispose(state.Dependency)
            //);


            //movementHandle.Complete();

            //NativeArray<Entity> entities = bulletQuery.ToEntityArray(Allocator.Temp);
            //for (int i = 0; i < entities.Length; ++i)
            //{
            //    Entity entity = entities[i];

            //    LocalTransform currentTransform = state.EntityManager.GetComponentData<LocalTransform>(entity);
            //    currentTransform.Position = updatedPositions[i];
            //    state.EntityManager.SetComponentData(entity, currentTransform);
            //}

            //entities.Dispose();
            //updatedPositions.Dispose();




            //EntityManager entityManager = state.EntityManager;



            //// We need the GameStateComponents to process any hits. Do return if not found
            //if (!isGameStateCached)
            //{
            //    if (SystemAPI.HasSingleton<GameStateComponent>())
            //    {
            //        gameStateEntity = SystemAPI.GetSingletonEntity<GameStateComponent>();
            //        isGameStateCached = true;
            //    }
            //    else
            //    {
            //        return;
            //    }
            //}

            //// At this point gameStateEntity is cached if gameStateEntity no longer exists then return, will try and re-cache next update
            //if (!entityManager.Exists(gameStateEntity))
            //{
            //    isGameStateCached = false;
            //    return;
            //}

            //RefRW<GameStateComponent> gameStateComponentRW = SystemAPI.GetComponentRW<GameStateComponent>(gameStateEntity);



            //PhysicsWorldSingleton physicsWorld = SystemAPI.GetSingleton<PhysicsWorldSingleton>();

            //NativeList<Entity> hitEntities = new NativeList<Entity>(Allocator.Temp);
            //NativeList<Entity> bulletLifeTimeExpired = new NativeList<Entity>(Allocator.Temp);


            //foreach (var (bulletComponent, bulletLocalTransform, entity) in SystemAPI.Query<RefRW<BulletComponent>, RefRW<LocalTransform>>().WithEntityAccess())
            //{

            //    bulletComponent.ValueRW.lifespan = 0f;
            //    //bulletLocalTransform.
            //    bulletComponent.ValueRW.lifespan -= SystemAPI.Time.DeltaTime;
            //    if (bulletComponent.ValueRW.lifespan < 0 )
            //    {
            //        //bulletLifeTimeExpired.Add(SystemAPI.Get)
            //    }


            //    bulletLocalTransform.ValueRW.Position += bulletComponent.ValueRO.speed * SystemAPI.Time.DeltaTime * bulletLocalTransform.ValueRW.Right();

            //    NativeList<ColliderCastHit> hits = new NativeList<ColliderCastHit>(Allocator.Temp);
            //    CollisionFilter collisionFilter = new CollisionFilter
            //    {
            //        BelongsTo = (uint)CollisionLayer.Default,
            //        CollidesWith = (uint)CollisionLayer.Enemy
            //    };

            //    if (physicsWorld.SphereCastAll(bulletLocalTransform.ValueRW.Position, bulletComponent.ValueRO.size / 2, float3.zero, 1, ref hits, collisionFilter))
            //    {
            //        foreach (ColliderCastHit hit in hits)
            //        {
            //            hitEntities.Add(hit.Entity);
            //        }
            //    }

            //    hits.Dispose();
            //}

            //if (!hitEntities.IsEmpty)
            //{
            //    gameStateComponentRW.ValueRW.score += hitEntities.Length;
               
            //    foreach (Entity entity in hitEntities)
            //    {
            //        //RefRW<EnemyComponent> enemyData = SystemAPI.GetComponentRW<EnemyComponent>(entity);


            //        entityManager.DestroyEntity(entity);
            //    }
                
                

            //}

            //hitEntities.Dispose();
            
        }

        
    }

    public enum CollisionLayer
    {
        Default = 1 << 0,
        Enemy = 1 << 7
    }


}
