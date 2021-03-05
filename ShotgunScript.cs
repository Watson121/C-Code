using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///Subclass of the Weapon Script Updated Class.
///Shotgun Script is responsible for functioanlity of the shotgun.
///</summary>
public class ShotgunScript : WeaponScriptUpdated
{

    ///<summary>
    ///Audio Clips that the Shotgun will use.
    ///</summary>
    [Header("Shotgun Audio")]
    [SerializeField] AudioClip shotgunShot;
    [SerializeField] AudioClip shotgunReloading;
    [SerializeField] AudioClip shotgunClick;

    ///<summary>
    ///Particle Effects for the shotgun.
    ///</summary>
    [Header("Shotgun Particle Effects")]
    [SerializeField] ParticleSystem shotgunMuzzle;
    [SerializeField] ParticleSystem shotgunPellets;
    [SerializeField] ParticleSystem shotgunSmoke;

    protected override void Update()
    {
        //Weapon has been fired. Play animation for firing.
        if (fired == true)
        {
            animationWaitTime -= Time.deltaTime;

            if (animationWaitTime < 0)
            {
                weaponAnimator.SetBool("Shotgun Fire", false);
                animationWaitTime = 1.0f;
                fired = false;
                currentRange = Vector3DMaths.Distance(transform, currentEnemy.transform);
                Debug.Log("Damage Dealt = " + DamageDealt());
            }
        }
    }

    ///<summary>
    ///Functions that are unique to the shotgun class.
    ///This Region also contains any overrides of base functions from the parent class.
    ///</summary>
    #region Shotgun Functions

    //<summary>
    //Shotgun override of the Weapon Fire Function found in the WeaponScriptUpdated Class
    //</summary>
    public override void WeaponFire()
    {
        Debug.Log("Shotgun Fired");

        if (weaponAnimator)
        {
            weaponAnimator.SetBool("Shotgun Fire", true);
            Debug.Log("Firing Shotgun Animation Played");

            shotgunMuzzle.Play();
            shotgunPellets.Play();
            shotgunSmoke.Play();

            numberOfBulletsLeft--;
            fired = true;

            enemyClass.DecreaseEnemyHealth(DamageDealt());
        }
    }

    public override void WeaponRun(bool state)
    {
        if (weaponAnimator)
        {
            weaponAnimator.SetBool("Shotgun Running", state);
        }
    }

    #endregion

    ///<summary>
    ///Methods & Functions that are responsible for playing the different shotgun audio clips
    ///</summary>
    #region Shotgun Audio

    public override void PlayGunshot()
    {
        weaponAudio.PlayOneShot(shotgunShot, 1.0f);
        Debug.Log("Playing " + shotgunShot.name);
    }

    public override void PlayReloading()
    {
        weaponAudio.PlayOneShot(shotgunReloading, 1.0f);
        Debug.Log("Playing " + shotgunReloading.name);
    }

    public override void PlayClick()
    {
        weaponAudio.PlayOneShot(shotgunClick, 1.0f);
        Debug.Log("Playing " + shotgunClick.name);
    }

    #endregion

    

}
