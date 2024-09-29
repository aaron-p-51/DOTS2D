using DOTS2D;
using Unity.Collections;
using Unity.Entities;
using UnityEngine.Rendering.Universal;

namespace DOTS
{
    public partial class PoolManager : SystemBase
    {
        private NativeQueue<Entity> inactiveEntities;
        private EntityManager entityManager;

        protected override void OnCreate()
        {
            entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            inactiveEntities = new NativeQueue<Entity>(Allocator.Persistent);
        }

        protected override void OnDestroy() 
        {
            inactiveEntities.Dispose();
        }

        protected override void OnUpdate() 
        {
            
        }

        public void AddToPool(Entity entity)
        {
            entityManager.SetComponentData(entity, new Poolable
            {
                IsActive = false
            });

            inactiveEntities.Enqueue(entity);
        }

        public Entity GetFromPool(Entity prefab)
        {
            if (inactiveEntities.Count > 0)
            {
                var entity = inactiveEntities.Dequeue();
                entityManager.SetComponentData(entity, new Poolable
                {
                    IsActive = true
                });

                return entity;
            }
            else
            {
                var entity = entityManager.Instantiate(prefab);
                entityManager.AddComponentData(entity, new Poolable
                {
                    IsActive = true
                });

                return entity;
            }
        }
    }
}
