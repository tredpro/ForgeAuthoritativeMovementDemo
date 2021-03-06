﻿using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : GameStateManagerBehavior
{
    protected override void NetworkStart()
    {
        base.NetworkStart();

        if (networkObject.IsServer)
        {
            //Add player spawning code.  
            //When a player joins, a cube is spawned with that player's ID assigned to the prefab
            NetworkManager.Instance.Networker.playerAccepted += (player) =>
            {
                MainThreadManager.Run(() =>
                {
                    var go = NetworkManager.Instance.InstantiateGuyWithMovement(position: new Vector3(0, 0.1f, 0));
                    var guy = go.GetComponent<GuyWithMovement>();
                    guy.networkObject.inputOwnerId = player.NetworkId;
                });
            };
        }

        else
        {
            NetworkManager.Instance.InstantiateInputListener();
        }
    }
}