using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Unity.Collections;
using Unity.Mathematics;

namespace DOTS2D
{
    public partial struct PlayerAnimationSystem : ISystem
    {
        private EntityManager entityManager;

        private void OnUpdate(ref SystemState state)
        {
            if (!SystemAPI.ManagedAPI.TryGetSingleton(out AnimationVisualsPrefabs animationVisualsPrefabs))
            {
                return;
            }

            entityManager = state.EntityManager;

            EntityCommandBuffer ECB = new EntityCommandBuffer(Allocator.Temp);

            foreach (var (transform, playerComponent, entity) in SystemAPI.Query<LocalTransform, PlayerComponent>().WithEntityAccess())
            {
                if (!entityManager.HasComponent<VisualsReferenceComponent>(entity))
                {
                    GameObject playerVisuals = Object.Instantiate(animationVisualsPrefabs.player);
                    ECB.AddComponent(entity, new VisualsReferenceComponent
                    {
                        gameObject = playerVisuals
                    });
                }
                else
                {
                    VisualsReferenceComponent playerVisualsReference = entityManager.GetComponentData<VisualsReferenceComponent>(entity);

                    playerVisualsReference.gameObject.transform.position = transform.Position;
                    playerVisualsReference.gameObject.transform.rotation = transform.Rotation;

                    InputComponent inputComponent = SystemAPI.GetSingleton<InputComponent>();
                    playerVisualsReference.gameObject.GetComponent<Animator>().SetBool("IsWalking", math.lengthsq(inputComponent.movement) > 0f);
                }
            }

            ECB.Playback(entityManager);
            ECB.Dispose();
        }
    }
}
