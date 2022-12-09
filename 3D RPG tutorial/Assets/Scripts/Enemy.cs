using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{
    PlayerManager playerManager;
    CharacterStats myStats;

    private void Start()
    {
        myStats = GetComponent<CharacterStats>();
        playerManager = PlayerManager.instance;
    }
    public override void Interact()
    {
        CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>();
        if (playerCombat != null)
        {
            playerCombat.Attack(myStats);
        }

        else if (playerCombat == null)
        {
            Debug.Log("HELP");
        }
    }
}
