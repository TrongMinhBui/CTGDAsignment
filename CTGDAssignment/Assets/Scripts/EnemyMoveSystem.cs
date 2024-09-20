using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public partial struct EnemyMoveSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float deltaTime = SystemAPI.Time.DeltaTime;
        foreach (var (transform, moveSpeed) in SystemAPI.Query<RefRW<LocalTransform>, EnemyAuthoring.EnemyMoveSpeed>())
        {
            transform.ValueRW.Position += -transform.ValueRO.Up() * moveSpeed.Value * deltaTime;
        }
    }
}
