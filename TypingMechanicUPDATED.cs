using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypingMechanicUPDATED : MonoBehaviour
{
    ///<summary>
    ///Interaction type - what will the player being interacting with.
    ///</summary>
    //public enum Type_of_Interaction
    //{
    //    Zombie,
    //    ZombieDog,
    //    ForestDemon,
    //    WingedDemon,
    //    SpiderlikeCreature,
    //    ArmouredZombie,
    //    MeatDemon,
    //    Continue,
    //    Reloading
    //};

    ///<summary>
    /// Interaction Type - current type of interaction that this typing mechainc is.
    ///</summary>
    public GlobalEnums.Type_of_Interaction interactionType;

    ///<summary>
    /// Global Items that are needed for many of the major aspects of this mechanic to work
    /// Word Pool - Contains all of the phrases and words that the player will have to enter
    /// Battlenode - Battlenode that this enemy is related to.
    /// Director - The Game Director
    /// Battlenode Wait Time - A count down timer for moving between the different nodes
    ///</summary>
    [Header("Global Items")]
    public WordPool wordPool;
    public BattlenodeBaseClass battlenode;
    public Director gameDirector;
    public float battlenodeWaitTime = 2.0f;

    ///<summary>
    /// Variables that are responisble for getting, and breaking down the required word.
    /// Required Word - The Word Required Word that player has to enter to interact.
    /// Word Breakdown - Will be used to breakdown the string of characters. 
    /// These charaters will then be compared alongside player input to see if there is a match
    ///</summary>
    [Header("Required Word")]
    [SerializeField] protected string requiredWord;
    [SerializeField] protected List<char> wordBreakdown;

    ///<summary>
    /// Variables that responsible for getting, displaying and breaking down the player's input
    /// Player Input String - Saving the player's text input.
    /// Player Input Breakdown - Breaking down the Player Input so it can be compared to see if there is a match.
    /// Index To Check - The index of the two character lists that will be compared
    ///</summary>
    [Header("Player Inputs")]
    [SerializeField] protected string playerInputString;
    [SerializeField] protected List<char> playerInputBreakdown;
    [SerializeField] protected int indexToCheck;

    ///<summary>
    /// Getting the GUI Elements that are responsible for showing off the required word, and player input to the player.
    /// Required Word Text - GUI Text that holds the Required Word
    /// Player Input Word Text - GUI Text that holds the player input word text
    ///</summary>
    [Header("GUI Elements")]
    [SerializeField] protected TextMeshProUGUI requiredWordText;
    [SerializeField] protected TextMeshProUGUI playerInputWordText;

    ///<summary>
    /// Variables that are needed for interacting with the player, and the current weapon being held by the player.
    /// Player - Getting the Player Game Object
    /// Player Script - Getting the Player Script
    /// Weapon Script - Getting the current weapon script
    ///</summary>
    #region Player - Getting Player Info
    [Header("Player Items")]
    [SerializeField] protected GameObject player;
    [SerializeField] protected PlayerScript playerScript;
    [SerializeField] protected WeaponScriptUpdated weaponScript;
    #endregion


    protected void TypingMechanicSetUp()
    {
        //Finding Player Object & Player Script
        player = GameObject.Find("Main Camera");
        playerScript = player.GetComponent<PlayerScript>();

        //Finding Weapon Script
        weaponScript = playerScript.GetWeaponScript();

        //Finding Word Pool
        wordPool = GameObject.Find("GlobalObj").GetComponent<WordPool>();

        //Finding Battlenode & Game Director
        gameDirector = GameObject.Find("GlobalObj").GetComponent<Director>();
        battlenode = gameDirector.currentBattleNode.GetComponent<BattlenodeBaseClass>();

        //Finding GUI Elements
        requiredWordText = GameObject.Find("PlayerTextPanel").transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        playerInputWordText = GameObject.Find("PlayerTextPanel").transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        playerInputString = "";
    }

    ///<summary>
    /// Getting the correct word pool. Word pool depends on the interaction type
    /// Base Getting Phrase Function
    ///</summary>
    virtual protected void GettingPhrase()
    {

        Debug.Log("Getting Phrase Base Function Called");

        switch (interactionType)
        {
            case GlobalEnums.Type_of_Interaction.Zombie:


                //Getting Zombie Phrases

                //TO DO:
                //ADD IN DIFFICULTY LEVELS

                requiredWord = wordPool.Zombie_Easy_Words[Random.Range(0, wordPool.Zombie_Easy_Words.Length)];
                break;
            case GlobalEnums.Type_of_Interaction.ZombieDog:

                //Getting Zombie Dog Phrases

                //TO DO:
                //ADD IN DIFFICULTY LEVELS

                requiredWord = wordPool.ZombieDog_Easy_Words[Random.Range(0, wordPool.ZombieDog_Easy_Words.Length)];
                break;
            case GlobalEnums.Type_of_Interaction.ForestDemon:

                //Getting Forest Demon Phrases

                //TO DO:
                //ADD IN DIFFICULTY LEVELS

                requiredWord = wordPool.ForestDemon_Easy_Words[Random.Range(0, wordPool.ForestDemon_Easy_Words.Length)];
                break;
            case GlobalEnums.Type_of_Interaction.WingedDemon:

                //Getting Winged Demon Phrases

                //TO DO:
                //ADD IN DIFFICULTY LEVELS

                requiredWord = wordPool.WingedDemon_Easy_Words[Random.Range(0, wordPool.WingedDemon_Easy_Words.Length)];
                break;
            case GlobalEnums.Type_of_Interaction.SpiderlikeCreature:

                //Getting Winged Demon Phrases

                //TO DO:
                //ADD IN DIFFICULTY LEVELS

                requiredWord = wordPool.Spiderlike_Easy_Words[Random.Range(0, wordPool.Spiderlike_Easy_Words.Length)];
                break;
            case GlobalEnums.Type_of_Interaction.ArmouredZombie:

                //Getting Winged Demon Phrases

                //TO DO:
                //ADD IN DIFFICULTY LEVELS

                requiredWord = wordPool.ArmouredZombie_Easy_Words[Random.Range(0, wordPool.ArmouredZombie_Easy_Words.Length)];
                break;
            case GlobalEnums.Type_of_Interaction.MeatDemon:
                requiredWord = wordPool.MeatDemon_Words[Random.Range(0, wordPool.MeatDemon_Words.Length)];
                break;
        }
    }

    ///<summary>
    /// Breaking down the Required word, and adding each character to the Word Breakdown character list
    ///</summary>
    protected void BreakdownRequiredWord()
    {
        for (int i = 0; i <= requiredWord.Length - 1; i++)
        {
            wordBreakdown.Add(requiredWord[i]);
        }
    }

    ///<summary>
    /// Controls - 
    /// Getting Player Input from the Keyboard
    ///</summary>
    private void OnGUI()
    {

        Event e = Event.current; //Current Key that has been pressed

        if (e.isKey)             //Checking if a key has been pressed
        {
            //Uppercase Input

            if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && Input.GetKeyDown(e.keyCode))
            {
                switch (e.keyCode)
                {
                    case KeyCode.A:
                        AddToPlayerInput('A');      //A
                        break;
                    case KeyCode.B:
                        AddToPlayerInput('B');      //B
                        break;
                    case KeyCode.C:
                        AddToPlayerInput('C');      //C
                        break;
                    case KeyCode.D:
                        AddToPlayerInput('D');      //D
                        break;
                    case KeyCode.E:
                        AddToPlayerInput('E');      //E
                        break;
                    case KeyCode.F:
                        AddToPlayerInput('F');      //F
                        break;
                    case KeyCode.G:
                        AddToPlayerInput('G');      //G
                        break;
                    case KeyCode.H:
                        AddToPlayerInput('H');      //H
                        break;
                    case KeyCode.I:
                        AddToPlayerInput('I');      //I
                        break;
                    case KeyCode.J:
                        AddToPlayerInput('J');      //J
                        break;
                    case KeyCode.K:
                        AddToPlayerInput('K');      //K
                        break;
                    case KeyCode.L:
                        AddToPlayerInput('L');      //L
                        break;
                    case KeyCode.M:
                        AddToPlayerInput('M');      //M
                        break;
                    case KeyCode.N:
                        AddToPlayerInput('N');      //N
                        break;
                    case KeyCode.O:
                        AddToPlayerInput('O');      //O
                        break;
                    case KeyCode.P:
                        AddToPlayerInput('P');      //P
                        break;
                    case KeyCode.Q:
                        AddToPlayerInput('Q');      //Q
                        break;
                    case KeyCode.R:
                        AddToPlayerInput('R');      //R
                        break;
                    case KeyCode.S:
                        AddToPlayerInput('S');      //S
                        break;
                    case KeyCode.T:
                        AddToPlayerInput('T');      //T
                        break;
                    case KeyCode.U:
                        AddToPlayerInput('U');      //U
                        break;
                    case KeyCode.V:
                        AddToPlayerInput('V');      //V
                        break;
                    case KeyCode.W:
                        AddToPlayerInput('W');      //W
                        break;
                    case KeyCode.X:
                        AddToPlayerInput('X');      //X
                        break;
                    case KeyCode.Y:
                        AddToPlayerInput('Y');      //Y
                        break;
                    case KeyCode.Z:
                        AddToPlayerInput('Z');      //Z
                        break;
                    case KeyCode.Alpha1:
                        AddToPlayerInput('!');      //!
                        break;
                    case KeyCode.Space:
                        AddToPlayerInput('_');      //Spacebar
                        break;
                    case KeyCode.Underscore:
                        AddToPlayerInput('_');      //Underscore
                        break;
                }
            }

            //Lowercase Input

            else if (Input.GetKeyDown(e.keyCode))
            {
                switch (e.keyCode)
                {
                    case KeyCode.A:
                        AddToPlayerInput('a');      //a
                        break;
                    case KeyCode.B:
                        AddToPlayerInput('b');      //b
                        break;
                    case KeyCode.C:
                        AddToPlayerInput('c');      //c
                        break;
                    case KeyCode.D:
                        AddToPlayerInput('d');      //d
                        break;
                    case KeyCode.E:
                        AddToPlayerInput('e');      //e
                        break;
                    case KeyCode.F:
                        AddToPlayerInput('f');      //f
                        break;
                    case KeyCode.G:
                        AddToPlayerInput('g');      //g
                        break;
                    case KeyCode.H:
                        AddToPlayerInput('h');      //h
                        break;
                    case KeyCode.I:
                        AddToPlayerInput('i');      //i
                        break;
                    case KeyCode.J:
                        AddToPlayerInput('j');      //j
                        break;
                    case KeyCode.K:
                        AddToPlayerInput('k');      //k
                        break;
                    case KeyCode.L:
                        AddToPlayerInput('l');      //l
                        break;
                    case KeyCode.M:
                        AddToPlayerInput('m');      //m
                        break;
                    case KeyCode.N:
                        AddToPlayerInput('n');      //n
                        break;
                    case KeyCode.O:
                        AddToPlayerInput('o');      //o
                        break;
                    case KeyCode.P:
                        AddToPlayerInput('p');      //p
                        break;
                    case KeyCode.Q:
                        AddToPlayerInput('q');      //q
                        break;
                    case KeyCode.R:
                        AddToPlayerInput('r');      //r
                        break;
                    case KeyCode.S:
                        AddToPlayerInput('s');      //s
                        break;
                    case KeyCode.T:
                        AddToPlayerInput('t');      //t
                        break;
                    case KeyCode.U:
                        AddToPlayerInput('u');      //u
                        break;
                    case KeyCode.V:
                        AddToPlayerInput('v');      //v
                        break;
                    case KeyCode.W:
                        AddToPlayerInput('w');      //w
                        break;
                    case KeyCode.X:
                        AddToPlayerInput('x');      //x
                        break;
                    case KeyCode.Y:
                        AddToPlayerInput('y');      //y
                        break;
                    case KeyCode.Z:
                        AddToPlayerInput('z');      //z
                        break;
                    case KeyCode.Space:
                        AddToPlayerInput('_');      //Space
                        break;
                }
            }
        }
    }

    ///<summary>
    /// Adding characters to the PlayerInput Char List
    ///</summary>
    private void AddToPlayerInput(char character)
    {
        playerScript.PlayTypeWritingKeySound();

        //Adding character to player input breakdown
        playerInputBreakdown.Add(character);

        //Getting the correct index
        if (playerInputBreakdown.Count != 0) indexToCheck = playerInputBreakdown.Count - 1;

        //Comparing the Required Text and Player Input Text
        if ((wordBreakdown.Count != 0) && (playerInputBreakdown.Count != 0)
            && !(indexToCheck > playerInputBreakdown.Count - 1)
            && !(indexToCheck > wordBreakdown.Count - 1))
        {

            //If there is not a match, clear the Player Input so that the player has to start again
            if (playerInputBreakdown[indexToCheck] != wordBreakdown[indexToCheck])
            {
                ClearPlayerInput();
            }

            //If there is a match, add the letter to the player input string. 
            //This displays to the player that they are correct
            else if (playerInputBreakdown[indexToCheck] == wordBreakdown[indexToCheck])
            {
                playerInputString += character;
                //Setting the Player Input GUI Text
                playerInputWordText.SetText(playerInputString);
            }
        }
    }

    ///<summary>
    /// Clearing Player Input, and setting Player Input string to empty. Also clears Player Input Char List
    ///</summary>
    public void ClearPlayerInput()
    {
        playerInputString = "";
        playerInputBreakdown.Clear();
        playerInputWordText.SetText(playerInputString);
    }

    ///<summary>
    /// When a phrase is completed, this method is called to clear up and move onto the next enemy.
    ///</summary>
    public void OnWordCompletion()
    {
        //Clearing up Player Input and Required Text
        ClearPlayerInput();
        ClearRequiredText();
    }

    ///<summary>
    /// On World Completion Death - Kills the enemy instead.
    ///</summary>
    public void OnWordCompletionDeath()
    {
        //Clearing up Player Input and Required Text
        ClearPlayerInput();
        ClearRequiredText();

        //Destroying this component. This is only for enemies
        Destroy(this);
    }

    ///<summary>
    /// Setting the Required UI Text to the Required Word
    ///</summary>
    public void SetRequiredUIText()
    {
        if (requiredWordText) requiredWordText.SetText(requiredWord);
    }

    ///<summary>
    /// Clearing the Required UI Text
    ///</summary>
    public void ClearRequiredText()
    {
        requiredWordText.SetText("");
        wordBreakdown.Clear();
    }

    ///<summary>
    /// Breaking down the different interactions that this 
    ///</summary>
    virtual protected void InteractionBreakdown()
    {
        //if (meleeEnemyAI)
        //{
        //    weaponScript.WeaponFire();
        //    weaponScript.PlayGunshot();

        //    if (meleeEnemyAI.enemyHealth == 0)
        //    {
        //        if (interactionType == Type_of_Interaction.Zombie)
        //        {
        //            OnWordCompletionDeath();
        //            meleeEnemyAI.Death();
        //            battlenode.RemoveInteractions(this.gameObject);
        //        }
        //    }
        //    else
        //    {
        //        if (interactionType == Type_of_Interaction.Zombie)
        //        {

        //            OnWordCompletion();

        //            if (meleeEnemyAI.enemyHealth == 0)
        //            {
        //                InteractionBreakdown();
        //            }
        //            else
        //            {
        //                GettingPhrase();
        //                BreakdownRequiredWord();
        //            }
        //        }
        //    }
        //}
    }
}
