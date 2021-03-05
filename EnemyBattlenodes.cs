using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattlenodes : BattlenodeBaseClass
{

    ///<summary>
    /// Interactions - A list of interactable items that the player will have to interact with
    /// Current Interactable - Current Interactable Selected
    ///</summary>
    [Header("Enemy Interactions")]
    public List<EnemyClass> Interactions;
    int currentEnemy;

    ///<summary>
    /// Spawn Points - A list of Spawn Points where enemeies can spawn from
    /// Continue Interaction - Siginal the next battlenode to move onto
    ///</summary>
    [Header("Enemy Spawn Points")]
    public List<GameObject> SpawnPoints;

    ///<summary>
    /// Which enemeies will spawn on at this node
    ///</summary>
    [Header("Battlenode Spawn Parameters")]
    public uint NumberOfZombies;
    public uint NumberOfZombieDogs;
    public uint NumberOfForestDemons;
    public uint NumberOfWingedDemons;
    public uint NumberOfSpiderCreatures;
    public uint NumberOfArmouredZombies;
    uint totalNumberofEnemies;

    ///<summary>
    /// Setting the time that it takes for certian enemies to spawn on the current node.
    ///</summary>
    [Header("Enemy Spawn Times")]
    public float ZombieSpawnTime;
    public float ZombieDogSpawnTime;
    public float ForestDemonSpawnTime;
    public float WingedDemonSpawnTime;
    public float SpiderCreatureSpawnTime;
    public float ArmouredZombieSpawnTime;

    ///<summary>
    /// Hidden floats which are actually used for the countdown timer
    ///</summary>
    float zombieCountdown;
    float zombieDogCountdown;
    float forestDemonCountdown;
    float wingedDemonCountdown;
    float spiderCountdown;
    float armouredZombieCountdown;

    ///<summary>
    /// Holds the enemeies Prefabs
    /// Need for Spawning Enemies
    ///</summary>
    [Header("Enemy Prefabs")]
    public GameObject Zombie;
    public GameObject ZombieDog;
    public GameObject ForestDemon;
    public GameObject WingedDemon;
    public GameObject SpiderCreature;
    public GameObject ArmouredZombie;


    private void Awake()
    {
        //Setting int to first Position in the List
        currentEnemy = 0;

        //Setting total number of enemies that will spawn
        AddingUpEnemies();

        //Finding The Player
        Player = GameObject.Find("Main Camera");
        playerScript = Player.GetComponent<PlayerScript>();
        weaponScript = playerScript.GetWeaponScript();

        //Setting Spawn Times
        zombieCountdown = 0;
        zombieDogCountdown = 0;
        forestDemonCountdown = 0;
        wingedDemonCountdown = 0;
        spiderCountdown = 0;
        armouredZombieCountdown = 0;

        //Continue Interaction - Used for moving onto next node
        ContinueInteraction = this.gameObject.transform.GetChild(0).gameObject;
    }

    ///<summary>
    /// Adding up Total Number of enemies to Spawn
    ///</summary>
    void AddingUpEnemies()
    {
        totalNumberofEnemies = NumberOfZombies +
                       NumberOfZombieDogs +
                       NumberOfForestDemons +
                       NumberOfForestDemons +
                       NumberOfWingedDemons +
                       NumberOfSpiderCreatures +
                       NumberOfArmouredZombies;
    }

    ///<summary>
    /// Adding objects to interactions list
    ///</summary>
    public override void AddToInteractions(GameObject obj)
    {
        Interactions.Add(obj.GetComponent<EnemyClass>());
    }

    ///<summary>
    /// Removing objects from the interactions list
    ///</summary>
    public override void RemoveInteractions(GameObject obj)
    {
        if (CurrentlyFocusedObj) CurrentlyFocusedObj.GetComponent<TypingMechanicUPDATED>().OnWordCompletion();
        Interactions.Remove(obj.GetComponent<EnemyClass>());

        if (Interactions.Count > 1) PreviousInteraction();
    }

    ///<summary>
    /// Going onto the next enemy
    ///</summary>
    public override void NextInteraction()
    {
        if (currentEnemy == Interactions.Count - 1)
        {
            currentEnemy = 0;
        }
        else if (currentEnemy < Interactions.Count - 1)
        {
            currentEnemy++;
        }

        activateDecativeTyping.Deactivate();
        SetCurrentlyFocused(Interactions[currentEnemy].enemyObj);
        activateDecativeTyping.Activate();
    }

    ///<summary>
    /// Setting Focused Obj to the same index as in the interactions list
    ///</summary>
    protected override void SetCurrentlyFocused(GameObject focusObj)
    {
        if (focusObj)
        {
            CurrentlyFocusedObj = focusObj;
            TypingMechanic = CurrentlyFocusedObj.GetComponent<TypingMechanicUPDATED>();
            activateDecativeTyping = CurrentlyFocusedObj.GetComponent<ActivateDecativeTyping>();
            TypingMechanic.battlenode = this;

            if (TypingMechanic.interactionType == GlobalEnums.Type_of_Interaction.Zombie)
            {
                Debug.Log("Focusing on a Zombie");

                playerScript.SetLookAtObj(CurrentlyFocusedObj.transform.GetChild(3).gameObject);
                weaponScript.SetCurrentEnemy(CurrentlyFocusedObj.transform.gameObject);
            }
            else if (TypingMechanic.interactionType == GlobalEnums.Type_of_Interaction.ZombieDog)
            {
                Debug.Log("Focusing on a Zombie Dog");

                playerScript.SetLookAtObj(CurrentlyFocusedObj.transform.GetChild(3).gameObject);
                weaponScript.SetCurrentEnemy(CurrentlyFocusedObj.transform.gameObject);
            }
            else if(TypingMechanic.interactionType == GlobalEnums.Type_of_Interaction.ForestDemon)
            {
                Debug.Log("Focusing on a Forest Demon");

                playerScript.SetLookAtObj(CurrentlyFocusedObj.transform.GetChild(3).gameObject);
                weaponScript.SetCurrentEnemy(CurrentlyFocusedObj.transform.gameObject);
            }
            else if(TypingMechanic.interactionType == GlobalEnums.Type_of_Interaction.WingedDemon)
            {
                Debug.Log("Focusing on a Winged Demon");

                playerScript.SetLookAtObj(CurrentlyFocusedObj.transform.GetChild(3).gameObject);
                weaponScript.SetCurrentEnemy(CurrentlyFocusedObj.transform.gameObject);
            }
            else if(TypingMechanic.interactionType == GlobalEnums.Type_of_Interaction.SpiderlikeCreature)
            {
                Debug.Log("Focusing on a Spiderlike Creature");

                playerScript.SetLookAtObj(CurrentlyFocusedObj.transform.GetChild(3).gameObject);
                weaponScript.SetCurrentEnemy(CurrentlyFocusedObj.transform.gameObject);
            }
            else if(TypingMechanic.interactionType == GlobalEnums.Type_of_Interaction.ArmouredZombie)
            {
                Debug.Log("Focusing on a Armoured Zombie");

                playerScript.SetLookAtObj(CurrentlyFocusedObj.transform.GetChild(3).gameObject);
                weaponScript.SetCurrentEnemy(CurrentlyFocusedObj.transform.gameObject);
            }
            else
            {
                playerScript.SetLookAtObj(CurrentlyFocusedObj.transform.gameObject);
            }
        }
    }

    ///<summary>
    /// Spawning Enemies at set spawn points
    ///</summary>
    void SpawnEnemy()
    {
        //Spawn Location for enemy
        GameObject SpawnLocation;

        SpawnLocation = SpawnPoints[Random.Range(0, SpawnPoints.Count - 1)];

        #region Zombie Spawning

        zombieCountdown -= Time.deltaTime;

        if (zombieCountdown < 0 && NumberOfZombies != 0)
        {
            GameObject newZombie = Instantiate(Zombie, SpawnLocation.transform.position, SpawnLocation.transform.rotation);
            AddToInteractions(newZombie);

            NumberOfZombies--;
            zombieCountdown = ZombieSpawnTime;
        }

        #endregion

        #region Zombie Dog Spawning

        zombieDogCountdown -= Time.deltaTime;

        if (zombieDogCountdown < 0 && NumberOfZombieDogs != 0)
        {
            GameObject newZombieDog = Instantiate(ZombieDog, SpawnLocation.transform.position, SpawnLocation.transform.rotation);
            AddToInteractions(newZombieDog);

            NumberOfZombieDogs--;
            zombieDogCountdown = ZombieDogSpawnTime;
        }

        #endregion

        #region Forest Demon Spawning

        forestDemonCountdown -= Time.deltaTime;

        if (forestDemonCountdown < 0 && NumberOfForestDemons != 0)
        {
            GameObject newForestDemon = Instantiate(ForestDemon, SpawnLocation.transform.position, SpawnLocation.transform.rotation);
            AddToInteractions(newForestDemon);

            NumberOfForestDemons--;
            forestDemonCountdown = ForestDemonSpawnTime;
        }

        #endregion

        #region Winged Demon Spawning

        wingedDemonCountdown -= Time.deltaTime;

        if (wingedDemonCountdown < 0 && NumberOfWingedDemons != 0)
        {
            GameObject newWingedDemon = Instantiate(WingedDemon, SpawnLocation.transform.position, SpawnLocation.transform.rotation);
            AddToInteractions(newWingedDemon);

            NumberOfWingedDemons--;
            wingedDemonCountdown = WingedDemonSpawnTime;
        }

        #endregion

        #region Spider Creature Spawning

        spiderCountdown -= Time.deltaTime;

        if (spiderCountdown < 0 && NumberOfSpiderCreatures != 0)
        {
            GameObject newSpiderCreature = Instantiate(SpiderCreature, SpawnLocation.transform.position, SpawnLocation.transform.rotation);
            AddToInteractions(newSpiderCreature);

            NumberOfSpiderCreatures--;
            spiderCountdown = SpiderCreatureSpawnTime;
        }


        #endregion

        #region Armoured Zombie Spawning

        armouredZombieCountdown -= Time.deltaTime;

        if (armouredZombieCountdown < 0 && NumberOfArmouredZombies != 0)
        {
            GameObject newArmouredZombie = Instantiate(ArmouredZombie, SpawnLocation.transform.position, SpawnLocation.transform.rotation);
            AddToInteractions(newArmouredZombie);

            NumberOfArmouredZombies--;
            armouredZombieCountdown = ArmouredZombieSpawnTime;
        }

        #endregion

        AddingUpEnemies();
    }

    ///<summary>
    /// Update is called once per frame
    ///</summary>
    void Update()
    {
        //Allowing the Player to cycle through new interactions
        if (Interactions.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) PreviousInteraction();
            if (Input.GetKeyDown(KeyCode.RightArrow)) NextInteraction();
        }
        //Moving onto the next node
        else if (Interactions.Count == 0 && totalNumberofEnemies == 0)
        {
            ContinueInteraction.SetActive(true);

            if (TypingMechanic)
            {

                if (TypingMechanic.interactionType == GlobalEnums.Type_of_Interaction.Zombie)
                {
                    waitTime -= Time.deltaTime;

                    if (waitTime < 0)
                    {
                        SetCurrentlyFocused(ContinueInteraction);
                        ContinueInteraction.GetComponent<TypingMechanicUPDATED>().SetRequiredUIText();
                        waitTime = 2.0f;
                    }
                }
            }
            else
            {
                SetCurrentlyFocused(ContinueInteraction);
                ContinueInteraction.GetComponent<TypingMechanicUPDATED>().SetRequiredUIText();
            }
        }

        //Spawn Enemies Into the World
        if (totalNumberofEnemies > 0)
        {
            SpawnEnemy();
        }

        //If there is only one element within the list, set currently focused to it
        if (Interactions.Count == 1)
        {
            currentEnemy = 0;
            SetCurrentlyFocused(Interactions[currentEnemy].enemyObj);
            activateDecativeTyping.Activate();
        }

        weaponScript = playerScript.GetWeaponScript();
    }

    ///<summary>
    /// Functions that are only called in the editor
    ///</summary>
    #region Editor Functions

    ///<summary>
    /// Finding all of the spawn points in the spawn point holder object. 
    ///</summary>
    public void FindSpawnPoints()
    {
        GameObject SpawnHolder = this.transform.GetChild(1).gameObject;

        SpawnPoints.Clear();

        for (int i = 0; i < SpawnHolder.transform.childCount; i++)
        {
            SpawnPoints.Add(SpawnHolder.transform.GetChild(i).gameObject);
        }
    }
    #endregion
}
