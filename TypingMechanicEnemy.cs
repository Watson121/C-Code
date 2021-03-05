using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypingMechanicEnemy : TypingMechanicUPDATED
{

    [Header("Enemy")]
    public EnemyClass enemyClassAI;

    ///<summary>
    /// Start is called before the first frame update
    /// Finding all of the variables in the level, that are needed for this mechainc to function
    ///</summary>
    void Start()
    {
        TypingMechanicSetUp();
        GettingPhrase();
        BreakdownRequiredWord();
    }

    protected override void GettingPhrase()
    {
        Debug.Log("Typing Mechanic Enemy Phrase override function called");

        enemyClassAI = GetComponent<EnemyClass>();

        switch (interactionType)
        {
            case GlobalEnums.Type_of_Interaction.Zombie:
                requiredWord = wordPool.Zombie_Easy_Words[Random.Range(0, wordPool.Zombie_Easy_Words.Length)];
                break;
            case GlobalEnums.Type_of_Interaction.ZombieDog:
                requiredWord = wordPool.ZombieDog_Easy_Words[Random.Range(0, wordPool.ZombieDog_Easy_Words.Length)];
                break;
            case GlobalEnums.Type_of_Interaction.ForestDemon:
                requiredWord = wordPool.ForestDemon_Easy_Words[Random.Range(0, wordPool.ForestDemon_Easy_Words.Length)];
                break;
            case GlobalEnums.Type_of_Interaction.WingedDemon:
                requiredWord = wordPool.WingedDemon_Easy_Words[Random.Range(0, wordPool.WingedDemon_Easy_Words.Length)];
                break;
            case GlobalEnums.Type_of_Interaction.SpiderlikeCreature:
                requiredWord = wordPool.Spiderlike_Easy_Words[Random.Range(0, wordPool.Spiderlike_Easy_Words.Length)];
                break;
            case GlobalEnums.Type_of_Interaction.ArmouredZombie:
                requiredWord = wordPool.ArmouredZombie_Easy_Words[Random.Range(0, wordPool.ArmouredZombie_Easy_Words.Length)];
                break;
        }
    }

    protected override void InteractionBreakdown()
    {
        if (enemyClassAI)
        {
            weaponScript.WeaponFire();
            weaponScript.PlayGunshot();

            if (enemyClassAI.enemyHealth == 0)
            {
                if (interactionType == GlobalEnums.Type_of_Interaction.Zombie)
                {
                    OnWordCompletionDeath();
                    enemyClassAI.Death();
                    battlenode.RemoveInteractions(this.gameObject);
                }
            }
            else
            {
                if (interactionType == GlobalEnums.Type_of_Interaction.Zombie)
                {

                    OnWordCompletion();

                    if (enemyClassAI.enemyHealth == 0)
                    {
                        InteractionBreakdown();
                    }
                    else
                    {
                        GettingPhrase();
                        BreakdownRequiredWord();
                    }
                }
            }
        }
    }

    ///<summary>
    /// Update is called once per frame
    /// This checks what type of interaction is going on, and if the enemy should be killed or not.
    ///</summary>
    void Update()
    {
        //Set up Required UI Text, only if Required UI Text is empty
        if (requiredWordText.text == "") SetRequiredUIText();

        //If Interatction Type is continue, then move onto next node
        //if (interactionType == Type_of_Interaction.Continue)
        //{
        //    //If Player is at Final Node
        //    if (gameDirector.battlenodeIndex == gameDirector.Battlenodes.Count - 1)
        //    {
        //        battlenodeWaitTime -= Time.deltaTime;

        //        if (battlenodeWaitTime < 0)
        //        {
        //            gameDirector.NextBattlenode();
        //            playerScript.moving = true;
        //            battlenode.ContinueInteraction.SetActive(false);
        //            ClearPlayerInput();
        //            ClearRequiredText();
        //            this.enabled = false;
        //        }
        //    }
        //    //Other Battlenodes
        //    else if (gameDirector.battlenodeIndex < gameDirector.Battlenodes.Count - 1)
        //    {
        //        gameDirector.NextBattlenode();
        //        playerScript.moving = true;
        //        battlenode.ContinueInteraction.SetActive(false);
        //        ClearPlayerInput();
        //        ClearRequiredText();
        //        this.enabled = false;
        //    }
        //}

        if (playerInputBreakdown.Count == wordBreakdown.Count)
        {
            InteractionBreakdown();
        }

        weaponScript = playerScript.GetWeaponScript();
    }
}
