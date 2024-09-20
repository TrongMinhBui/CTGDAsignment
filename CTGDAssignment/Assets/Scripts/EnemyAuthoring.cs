using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class EnemyAuthoring : MonoBehaviour
{
    public float enemyMoveSpeed;
    
    public class EnemyMovementAuthoringBaker : Baker<EnemyAuthoring>
    {
        public override void Bake(EnemyAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new EnemyMoveSpeed{ Value = authoring.enemyMoveSpeed });
        }
    }


    public struct EnemyMoveSpeed : IComponentData
    {
        public float Value;
    }
}
