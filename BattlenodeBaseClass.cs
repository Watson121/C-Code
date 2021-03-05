using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlenodeBaseClass : MonoBehaviour
{

    ///<summary>
    /// Currently Focused Object - The Current Object that this Battlenode is focusing on
    /// Typing Mechanic - The Typing Mechanic of the Currently Focused Object
    /// Activate Decative Typing - Class for activating and deactivting the typing mechanic
    ///</summary>
    [Header("Current Interactable")]
    public GameObject CurrentlyFocusedObj;
    public TypingMechanicUPDATED TypingMechanic;
    protected ActivateDecativeTyping activateDecativeTyping;

    ///<summary>
    /// Finding Player & getting the weapon that the player is holding
    ///</summary>
    [Header("Player")]
    public GameObject Player;
    public PlayerScript playerScript;
    public WeaponScriptUpdated weaponScript;

    /// <summary>
    /// Continue Interaction - Siginal the next battlenode to move onto
    /// </summary>
    public GameObject ContinueInteraction;

    ///<summary>
    /// Wait Time for moving between Battlenodes
    ///</summary>
    protected float waitTime = 2.0f;

    ///<summary>
    /// Adding objects to interactions list.
    /// Base Add To Interactions Function. Can be overriden.
    ///</summary>
    virtual public void AddToInteractions(GameObject obj)
    {
    }

    ///<summary>
    /// Removing objects from the interactions list
    /// Base Remove From Interactions Function. Can be overriden.
    ///</summary>
    virtual public void RemoveInteractions(GameObject obj)
    {
    }

    ///<summary>
    /// Going onto the next interactable object
    /// Base Next Interaction Function. Can be overriden.
    ///</summary>
    virtual public void NextInteraction()
    {
    }

    ///<summary>
    /// Going to the previous interactable object
    /// Base Previous Interaction Function. Can be overriden.
    ///</summary>
    virtual public void PreviousInteraction()
    {
    }

    ///<summary>
    /// Setting Focused Obj to the same index as in the interactions list.
    /// Base Set Currently Focused Intraction. Can be Overriden.
    ///</summary>
    virtual protected void SetCurrentlyFocused(GameObject focusObj)
    {
        if (focusObj)
        {
            CurrentlyFocusedObj = focusObj;
            TypingMechanic = CurrentlyFocusedObj.GetComponent<TypingMechanicUPDATED>();
            activateDecativeTyping = CurrentlyFocusedObj.GetComponent<ActivateDecativeTyping>();
            TypingMechanic.battlenode = this;

            if (TypingMechanic.interactionType == GlobalEnums.Type_of_Interaction.Zombie)
            {
                playerScript.SetLookAtObj(CurrentlyFocusedObj.transform.GetChild(3).gameObject);
                weaponScript.SetCurrentEnemy(CurrentlyFocusedObj.transform.gameObject);
            }
            else
            {
                playerScript.SetLookAtObj(CurrentlyFocusedObj.transform.gameObject);
            }
        }
    }
}
