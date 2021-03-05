using System.Collections;
using System.Collections.Generic;
using UnityEngine;


///<summary>
///Base Weapon Script Class. Holds all of the methods, variables, events and functions that are resposnible
///for controlling the weapon. This includes: Firing & Reloading 
///The Different Types of weapon class will be: Pistol, Shotgun & Rifle. More to come later on.
///</summary>

public class WeaponScriptUpdated : MonoBehaviour
{
    /// <summary>
    /// These are the main weapon attributes of the weapon. They determine the damage, range of weapon and if it's been fired or not.
    /// Base Damage - Base Amount of damage that this weapon deals
    /// Max Weapon Range - Maxinium Range of this Weapon (Measured in Units)
    /// Distance Modifer - A Modifer used to modify damage over a distance. Closer the enemy, higher the damage. Further the enemy, the damage wil be lower
    /// In Inventory - Is this weapon in the player's inventory. If TRUE then the player can equip it.
    ///                                                          If FALSE then the player cannot equip it.
    /// Fired - Weapon has been fired or not.
    /// </summary>
    [Header("Weapon Attributes")]
    public float baseDamage;
    public float maxWeaponRange;
    protected float distanceModifer;
    [SerializeField] protected float damagePointIncrease;
    protected float randomnessModifer;
    [SerializeField] protected float currentRange;
    [SerializeField] protected bool inInventory;
    [SerializeField] protected bool fired;

    /// <summary>
    /// Keeping track of the amount of ammo that the player currently has left.
    /// </summary>
    [Header("Ammo System")]
    public int bullets;
    protected int numberOfBulletsLeft;

    /// <summary>
    /// Keeping track of the current enemy that player is targetting.
    /// </summary>
    [Header("Enemy")]
    public GameObject currentEnemy;
    protected EnemyClass enemyClass;

    /// <summary>
    /// The many different components that are needed for many different aspects of this mechaincs.
    /// This includes audio and animation
    /// </summary>
    [SerializeField] protected Animator weaponAnimator;
    protected AudioSource weaponAudio;
    [SerializeField] protected float animationWaitTime;

    // Start is called before the first frame update
    void Awake()
    {
        //Finding Weapon Animator
        weaponAnimator = GetComponent<Animator>();
        //Finding Weapon Audio Source
        weaponAudio = GetComponent<AudioSource>();
        //Setting up Ammo System
        numberOfBulletsLeft = bullets;
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        //Weapon has been fired. Play animation for firing.
        if (fired == true)
        {
            animationWaitTime -= Time.deltaTime;

            if(animationWaitTime < 0)
            {
                weaponAnimator.SetBool("Fire", false);
                animationWaitTime = 1.0f;
                fired = false;
                currentRange = Vector3DMaths.Distance(transform, currentEnemy.transform);
                Debug.Log("Damage Dealt = " + DamageDealt());
            }
        }
    }

    ///<summary>
    ///Getting the Current Enemy that the player is attacking
    ///Base Function
    ///</summary>
    virtual public void SetCurrentEnemy(GameObject obj)
    {
        currentEnemy = obj;
        enemyClass = currentEnemy.GetComponent<EnemyClass>();
    }

    ///<summary>
    ///Checking the Enemy is in range of the weapon or not.
    ///Examples: Enemy is behind Wall = Blocked
    ///          Enemy is out in open = Not Blocked
    ///</summary>
    virtual protected void InRange()
    {
        if (Physics.Linecast(transform.position, currentEnemy.transform.position))
        {
            Debug.Log("Blocked");
            enemyClass.inRange = false;
        }
        else
        {
            Debug.Log("Not Blocked");
            enemyClass.inRange = true;
        }
    }

    ///<summary>
    ///Working out the weapon damage.
    ///This function works out the amount of damage that this weapon will deal.
    ///The main calculation for working out weapon damage is: DAMAGE = (BASE DAMAGE) * (DISTNACE MODIFER + RANDOMNESS MODIFER)
    ///
    ///Definitions:
    ///Base Damage - Base Damage of weapon that the player has been equiped. This should have already been declared before start.
    ///Distance Modifer - A Float that represents how distance will affect the damage of the weapon
    ///Randomness Modifer - A Float that fives a slight randomness to damage out of weapons.
    ///Max Weapon Range - Max Range of the Weapon currently equiped. Should have already been decleared before start.
    ///Current Range - Curretn Ranage the player is away from the enemy
    ///</summary>
    protected float DamageDealt()
    {
        float damage = 0;
        float distanceModifer = DamageModifer();
        float randomnessModifer = RandomnessModifer(distanceModifer);

        Debug.Log("The Distance Mod is " + distanceModifer);
        Debug.Log("The Randomness Mod is " + randomnessModifer);

        damage = (baseDamage) * (distanceModifer);

        return damage;
    }

    ///<summary>
    ///Calculating the Damage Modifer.
    ///Calculation: (((Max Weapon Range - Current Range) / Max Range) + damagePointIncrease)
    ///</summary>
    protected float DamageModifer()
    {
        float damage = (((maxWeaponRange - currentRange) / maxWeaponRange) + damagePointIncrease);
        return Mathf.Clamp(damage, 0.5f, 1.5f);
    }

    ///<summary>
    ///Calcualating the Randomness Modifer
    ///Calculation - Range(Distance Modifer - 0.3, Distance Mod)
    ///              This will be clamped between 1.2 and 0.8
    ///</summary>
    protected float RandomnessModifer(float disMod)
    {
        return Mathf.Clamp((Random.Range(disMod - 0.3f, disMod)), 0.8f, 1.2f);
    }

    ///<summary>
    /// Region that holds the main functionality of the weapon.
    /// Conatins all of the base functions that can be later overriden.
    ///</summary>
    #region Weapon Functions
    
    /// <summary>
    /// When a Weapon is fired. This function will be called to play animation and deal damage to the enemy.
    /// This is a base function, which will be overriden in the subclasses.
    /// </summary>
    virtual public void WeaponFire()
    {
        Debug.Log("FIRE");
        //Play animation for when a weapon is fired

        if (weaponAnimator)
        {
            weaponAnimator.SetBool("Fire", true);
            Debug.Log("Animation Played");

            numberOfBulletsLeft--;
            fired = true;
        }
    }

    /// <summary>
    /// Playing Run animation for when player is moving between two battlenodes
    /// </summary>
    virtual public void WeaponRun(bool state)
    {

        //Play animation for when the player is running between points

        if (weaponAnimator)
        {
            weaponAnimator.SetBool("Running", state);
        }
    }

    public void UnequipingWeapon()
    {
        weaponAnimator.SetBool("Exit", true);
    }

    #endregion

    #region Coroutines

    /// <summary>
    /// Playing & Stopping Reloading Animation
    /// </summary>
    virtual public IEnumerator ReloadingTime()
    {
        weaponAnimator.SetBool("Reloading", true);
        yield return new WaitForSeconds(1.0f);
        weaponAnimator.SetBool("Reloading", false);
        numberOfBulletsLeft = bullets;

 
    }

    #endregion

    #region Weapon Audio

    //Playing Gunshot Audio
    virtual public void PlayGunshot()
    {

    }

    //Playing Weapon Click Audio
    virtual public void PlayClick()
    {

    }

    //Playing Reloading Audio
    virtual public void PlayReloading()
    {
    }

    #endregion

    #region Getters

    /// <summary>
    /// Getting Number of Bullets Left
    /// </summary>
    public int GetNumberBullets()
    {
        return numberOfBulletsLeft;
    }

    /// <summary>
    /// Returning the inInventory Boolean.
    /// </summary>
    public bool GetInventory()
    {
        return inInventory;
    }

    #endregion


}
