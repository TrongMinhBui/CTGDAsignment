using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
public partial class PlayerInputSystem : SystemBase
{
    private GameInput InputActions;
    private Entity Player;

    protected override void OnCreate()
    {
        RequireForUpdate<PlayerTag>();
        RequireForUpdate<PlayerMoveInput>();
        InputActions = new GameInput();
    }

    protected override void OnStartRunning()
    {
        InputActions.Enable();
        Player = SystemAPI.GetSingletonEntity<PlayerTag>();
        
    }

    protected override void OnUpdate()
    {
        Vector2 moveInput = InputActions.Player.Movement.ReadValue<Vector2>();
        
        SystemAPI.SetSingleton(new PlayerMoveInput{ Value = moveInput });
    }

    protected override void OnStopRunning()
    {
        InputActions.Disable();
        Player = Entity.Null;
    }
}
