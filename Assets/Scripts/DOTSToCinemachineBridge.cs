using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace DOTS2D
{
    public class DOTSToCinemachineBridge : MonoBehaviour
    {
        private Entity TargetEntity;
        private EntityManager entityManager;

        // Start is called before the first frame update
        void Start()
        {
            entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        }

        // Update is called once per frame
        void Update()
        {
            if (entityManager == null)
            {
                entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
                return;
            }

            //entityManager
        }

       
    }
}