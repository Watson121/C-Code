using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypingMechanicDoor : TypingMechanicUPDATED
{

    [Header("Door")]
    public DoorScript door;


    private void OnEnable()
    {
        TypingMechanicSetUp();
        GettingPhrase();
        BreakdownRequiredWord();
    }

    protected override void GettingPhrase()
    {
        Debug.Log("Getting Phrase Override Function for Door has been called");

        if(interactionType == GlobalEnums.Type_of_Interaction.Door)
        {
            requiredWord = wordPool.Door_Word_Pool[Random.Range(0, wordPool.Door_Word_Pool.Length)];
        }
    }

    protected override void InteractionBreakdown()
    {
        if (door)
        {

            door.OpenDoor();
            OnWordCompletionDeath();

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (requiredWordText.text == "") SetRequiredUIText();

        if(playerInputBreakdown.Count == wordBreakdown.Count)
        {
            InteractionBreakdown();
        }

        weaponScript = playerScript.GetWeaponScript();
    }
}
