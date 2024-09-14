using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DOTS2D
{
    public partial class InputSystem : SystemBase
    {
        private Controls controls;

        protected override void OnCreate()
        {
            if (!SystemAPI.TryGetSingleton<InputComponent>(out InputComponent input))
            {
                EntityManager.CreateEntity(typeof(InputComponent));
            }

            controls = new Controls();
            controls.Enable();
        }

        protected override void OnUpdate()
        {
            if (controls == null) return;

            Vector2 moveVector = controls.ActionMap.Movement.ReadValue<Vector2>();
            Vector2 mousePosition = controls.ActionMap.MousePosition.ReadValue<Vector2>();
            bool isPressingLMB = controls.ActionMap.Shoot.ReadValue<float>() == 1f ? true : false;

            SystemAPI.SetSingleton(new InputComponent
            {
                movement = moveVector,
                mousePos = mousePosition,
                pressingLMB = isPressingLMB
            });
        }
    }
}
