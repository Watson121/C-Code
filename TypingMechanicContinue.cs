using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypingMechanicContinue : TypingMechanicUPDATED
{
    // Start is called before the first frame update
    void Start()
    {
        TypingMechanicSetUp();

        interactionType = GlobalEnums.Type_of_Interaction.Continue;
    }

    // Update is called once per frame
    void Update()
    {
        //If Interatction Type is continue, then move onto next node
        if (interactionType == GlobalEnums.Type_of_Interaction.Continue)
        {
            //If Player is at Final Node
            if (gameDirector.battlenodeIndex == gameDirector.Battlenodes.Count - 1)
            {
                battlenodeWaitTime -= Time.deltaTime;

                if (battlenodeWaitTime < 0)
                {
                    gameDirector.NextBattlenode();
                    playerScript.moving = true;
                    battlenode.ContinueInteraction.SetActive(false);
                    ClearPlayerInput();
                    ClearRequiredText();
                    this.enabled = false;
                }
            }
            //Other Battlenodes
            else if (gameDirector.battlenodeIndex < gameDirector.Battlenodes.Count - 1)
            {
                gameDirector.NextBattlenode();
                playerScript.moving = true;
                battlenode.ContinueInteraction.SetActive(false);
                ClearPlayerInput();
                ClearRequiredText();
                this.enabled = false;
            }
        }
    }
}
