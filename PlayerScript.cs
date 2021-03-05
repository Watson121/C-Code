using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    ///<summary>
    ///Getting the main camera that the player looks through
    ///</summary>
    [Header("Player Attributes")]
    public GameObject mainCamera;
    public GameObject player;
    public float playerHealth;
    public GameObject currentBattleNode;
    public bool attacked;
    public WeaponScriptUpdated[] Inventory;
    private int inventoryIndex;

    ///<summary>
    ///Getting the Audio Source that is responisble for playing audio
    ///</summary>
    [Header("Player Audio Source")]
    public AudioSource audioSource;

    ///<summary>
    ///List of audio clips that this component will play
    ///</summary>
    [Header("Audio Clips")]
    public AudioClip typeWriterKeyPress;
    public AudioClip typeWritingMultiplePresses;
    public AudioClip typeWriterDing;
    public AudioClip running;
    public AudioClip playerGrunt;

    ///<summary>
    ///Components that are needed to move the Player around the level
    ///Look at Object - The Object that the Player will look at
    ///</summary>
    [Header("Player Movement & Navigation")]
    public NavMeshAgent playerNavAgent;
    public bool moving;
    [SerializeField] GameObject lookAtObj;

    ///<summary>
    ///Player Weapons - the weapons that player will be able to use throughout the game
    ///</summary>
    [Header("Player Weapons")]
    public GameObject shotgun;                  
    public GameObject pistol;                   
    public GameObject rifle;                    
    public WeaponScriptUpdated weaponScript;
    private ShotgunScript shotgunScript;
    private PistolScript pistolScript;
    private RifleScript rifleScript;
    bool pistolActive;
    bool shotgunActive;
    bool rifleActive;

    ///<summary>
    ///Global Objects
    ///Current Scene - Current Level/Scene string
    ///</summary>
    GameObject globalObj;
    Director gameDirector;
    string currentScene;

    ///<summary>
    ///Compoenents Related to the Player UI - includes Health Bar
    ///<summary>
    #region Player UI

    TextMeshProUGUI playerHealthNumber;
    Slider playerHealthBar;

    #endregion

    ///<summary>
    ///Post Processing Effects - This is attached to the camera, to give the game an overall better look
    ///</summary>
    #region Post Processing
    PostProcessVolume postProcessVolume;
    Vignette playerVingette;
    float vingetteValue;
    #endregion

    void Awake()
    {
        SettingUpThePlayer();
        GettingSceneName();
    }

    ///<summary>
    ///Functions/Methods related to setting up the player
    ///</summary>
    #region Player Setup Functions/Methods

    ///<summary>
    ///Method/Function that Sets up the Player, and get's all of the needed components for this script to function
    ///</summary>
    void SettingUpThePlayer()
    {
        //Finding the Player, and setting up the current Battlenode
        player = GameObject.Find("Player");
        globalObj = GameObject.Find("GlobalObj");
        gameDirector = globalObj.GetComponent<Director>();
        currentBattleNode = gameDirector.currentBattleNode;

        //Finding the Player's Camera
        mainCamera = this.gameObject;

        //Getting Player Nav Agent, needed for controlling player movement around the level
        playerNavAgent = GameObject.Find("Player").GetComponent<NavMeshAgent>();
        moving = true; //TEMP FIX FOR GLTICH AT START OF EVERY LEVEL

        //Finding the Player's Audio Source
        audioSource = GetComponent<AudioSource>();

        //Setting up Player Health
        playerHealth = 100;
        playerHealthBar = GameObject.Find("PlayerHealthBar").GetComponent<Slider>();
        playerHealthNumber = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        playerHealthBar.value = playerHealth;

        //Finding Player Weapons
        pistol = GameObject.Find("Pistol");
        shotgun = GameObject.Find("ShotgunWeapon");
        rifle = GameObject.Find("Rifle");

        //Finding Weapon Scripts
        pistolScript = pistol.GetComponent<PistolScript>();
        shotgunScript = shotgun.GetComponent<ShotgunScript>();
        rifleScript = rifle.GetComponent<RifleScript>();

        //Post Processing Effects
        postProcessVolume = GetComponent<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings(out playerVingette);
        playerVingette.enabled.value = true;
        vingetteValue = 0.0f;

        //Inventory
        inventoryIndex = 0;
    }

    ///<summary>
    ///Getting the name of the active scene/level.
    ///This also will equip the correct weapon for certain levels.
    ///</summary>
    void GettingSceneName()
    {
        currentScene = SceneManager.GetActiveScene().name;
        Debug.Log("The current name of the scene is " + currentScene);

        if (currentScene == "Level 1 - House")
        {
            EquipPistol();
        }
        else
        {
            EquipRifle();
        }
    }
    #endregion

    ///<summary>
    ///Setting Which Object that the Player should Look at
    ///</summary>
    public void SetLookAtObj(GameObject obj)
    {
        lookAtObj = obj;
    }

    ///<summary>
    ///Transforming towards the next Battlenode
    ///</summary>
    public void TransformToNewNode()
    {
        //Setting the Current Battlenode, to the next battlenode
        currentBattleNode = gameDirector.currentBattleNode;
        lookAtObj = null;
        lookAtObj = currentBattleNode;

        playerNavAgent.destination = currentBattleNode.transform.position;
        weaponScript.WeaponRun(true);

        float distance = Vector3DMaths.Distance(currentBattleNode.transform, mainCamera.transform);

        if(!audioSource.isPlaying) PlayRunningSound();

        //Once Player is near the Battlenode, stop animations and stop the player from moving
        if (distance < 2.0)
        {
            audioSource.Stop();
            moving = false;
            weaponScript.WeaponRun(false);
        }
    }

    ///<summary>
    ///Functions/Methods related to controlling & monitoring the Player's Health
    ///</summary>
    #region PlayerHealth

    /// <summary>
    /// Decrease the Health of the Player
    /// </summary>
    public void DecreaseHealth(float Damage)
    {
        playerHealth -= Damage;
        vingetteValue = 0.4f;
        PlayerGrunt();
    }

    /// <summary>
    /// Increase the Health of the Player
    /// </summary>
    public void IncreaseHealth(float Health)
    {
        playerHealth += Health;
        if (playerHealth > 100) playerHealth = 100;
    }

    /// <summary>
    /// Checking the Player Health, and Updating the Player Health Bar
    /// </summary>
    void PlayerHealthCheck()
    {
        //Setting the Player Health Bar to the Correct Value
        if (playerHealthBar.value > playerHealth)
        {
            playerHealthBar.value -= 10 * Time.deltaTime;
        }

        if (playerHealth <= 0)
        {
            Debug.Log("End Game");
            SceneManager.LoadScene(2);
        }

    }

    #endregion

    ///<summary>
    ///Functions/Methods Related to Equiping the Player with different Weapons
    ///</summary>
    #region Equiping Different Weapons

    ///<summary>
    ///Equiping Pistol
    ///</summary>
    public void EquipPistol()
    {
        pistol.SetActive(true);
        shotgun.SetActive(false);
        rifle.SetActive(false);
        weaponScript = pistolScript;
    }

    ///<summary>
    ///Equiping Shotgun
    ///</summary>
    public void EquipShotgun()
    {
        pistol.SetActive(false);
        shotgun.SetActive(true);
        rifle.SetActive(false);
        weaponScript = shotgunScript;
    }

    ///<summary>
    ///Equiping Rifle
    ///</summary>
    public void EquipRifle()
    {
        pistol.SetActive(false);
        shotgun.SetActive(false);
        rifle.SetActive(true);
        weaponScript = rifleScript;
    }

    #endregion

    //A method that is ran everything single frame
    private void Update()
    {
        //Rotating the Player to look at the enemy
        if (lookAtObj && Vector3DMaths.Distance(player.transform, mainCamera.transform) < 3)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Vector3DMaths.GetObjLookAtAngle(mainCamera.transform, lookAtObj.transform), 2.0f * Time.deltaTime);
        }
        
        //Moving Player to Next Battlenode
        if (moving)
        {
            playerNavAgent.speed = 5.5f;
            TransformToNewNode();
        }

        //if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene(0);        //REPLACE WITH PAUSE MENU

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (pistolScript.GetInventory())
            {
                weaponScript.UnequipingWeapon();
                EquipPistol();
            }
            else
            {
                Debug.Log("Pistol not in Inventory");
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (shotgunScript.GetInventory())
            {
                weaponScript.UnequipingWeapon();
                EquipShotgun();
            }
            else
            {
                Debug.Log("Shotgun not in Inventory");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (rifleScript.GetInventory())
            {
                weaponScript.UnequipingWeapon();
                EquipRifle();
            }
            else
            {
                Debug.Log("Rifle not in Inventory");
            }
        }


        DamageVingette();
        PlayerHealthCheck();
    }


    ///<summary>
    ///Methods/Functions related to controlling the post processing effects
    ///</summary>
    #region Post Processing Methods

    //DISABLED AT THIS TIME
    void DamageVingette()
    {
        //if(attacked) playerVingette.intensity.value = Mathf.Lerp(playerVingette.intensity.value, 0.5f, 10.0f * Time.deltaTime);
        //else if(!attacked) playerVingette.intensity.value = Mathf.Lerp(playerVingette.intensity.value, 0.0f, 10.0f * Time.deltaTime);


        //if(playerVingette.intensity.value != 0.5) playerVingette.intensity.value = Mathf.Lerp(playerVingette.intensity.value, vingetteValue, 10.0f * Time.deltaTime);
        //else
        //{
        //    vingetteValue = 0;
        //    playerVingette.intensity.value = Mathf.Lerp(playerVingette.intensity.value, vingetteValue, 10.0f * Time.deltaTime);
        //}
    }
    #endregion

    ///<summary>
    ///Methods/Functions related to playing Player Audio
    ///</summary>
    #region Player Audio
    public void PlayTypeWritingKeySound()
    {
        audioSource.PlayOneShot(typeWriterKeyPress, 0.8f);
    }

    public void PlayTypewritingDing()
    {
        audioSource.PlayOneShot(typeWriterDing, 1.0f);
    }

    public void PlayTypeWriterMultipleHits()
    {
        audioSource.PlayOneShot(typeWritingMultiplePresses, 1.0f);
    }

    public void PlayRunningSound()
    {
        audioSource.PlayOneShot(running, 0.5f);
    }

    public void PlayerGrunt()
    {
        audioSource.PlayOneShot(playerGrunt, 0.5f);
    }

    #endregion

    /// <summary>
    /// Getters - Gettting private methods
    /// </summary>
    #region Getters

    /// <summary> 
    /// Returning the Weapon Script 
    /// </summary>
    public WeaponScriptUpdated GetWeaponScript()
    {
        return weaponScript;
    }
    #endregion


}
