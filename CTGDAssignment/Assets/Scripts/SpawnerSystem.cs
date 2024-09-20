using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = Unity.Mathematics.Random;

//partial means not a complete implementation, needed for the compilation 

public partial struct SpawnerSystem : ISystem
{
    private Random _random;

    public void OnCreate(ref SystemState state)
    {
        _random = new Random((uint)UnityEngine.Random.Range(1, 100000));
    }
    
    public void OnDestroy (ref SystemState state) {}

    public void OnUpdate(ref SystemState state)
    {
        foreach (RefRW<Spawner> spawner in SystemAPI.Query<RefRW<Spawner>>())
        {
            if (spawner.ValueRO.NextSpawnTime < SystemAPI.Time.ElapsedTime)
            {
                float randomX = _random.NextFloat(-6f, 6f);
                float randomY = _random.NextFloat(4f,4f);
                Entity newEntity = state.EntityManager.Instantiate(spawner.ValueRO.Prefab);
                float3 pos = new float3(randomX, randomY, 0);
                state.EntityManager.SetComponentData(newEntity, LocalTransform.FromPosition(pos));
                spawner.ValueRW.NextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.SpawnRate;
            }
        }
    }
    
}
