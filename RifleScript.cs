using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleScript : WeaponScriptUpdated
{
    /// <summary>
    /// Audio Clips that the Rifle will use
    /// </summary>
    [Header("Rifle Audio")]
    [SerializeField] AudioClip rifleShot;
    [SerializeField] AudioClip rifleReload;
    [SerializeField] AudioClip rifleClick;

    /// <summary>
    /// Parictle Effects fot the Rifle
    /// </summary>
    [Header("Rifle Particle Effects")]
    [SerializeField] ParticleSystem rifleMuzzleFlash;
    [SerializeField] ParticleSystem rifleSmoke;

    protected override void Update()
    {
        //Weapon has been fired. Play animation for firing.
        if (fired == true)
        {
            animationWaitTime -= Time.deltaTime;

            if (animationWaitTime < 0)
            {
                weaponAnimator.SetBool("Rifle Fire", false);
                animationWaitTime = 1.0f;
                fired = false;
                currentRange = Vector3DMaths.Distance(transform, currentEnemy.transform);
                Debug.Log("Damage Dealt = " + DamageDealt());
            }
        }
    }

    ///<summary>
    ///Functions that are unique to the Rifle Class.
    ///This Region also contains any overrides of base functions from the parent class.
    /// </summary>
    #region Rifle Functions

    public override void WeaponFire()
    {
        Debug.Log("Rifle Fired!");

        if (weaponAnimator)
        {
            weaponAnimator.SetBool("Rifle Fire", true);
            Debug.Log(weaponAnimator.GetBool("Rifle Fire"));
            Debug.Log("Firing Rifle Animation Played");

            rifleMuzzleFlash.Play();
            rifleSmoke.Play();

            numberOfBulletsLeft--;
            fired = true;

            enemyClass.DecreaseEnemyHealth(DamageDealt());
        }
    }

    public override void WeaponRun(bool state)
    {
        if (weaponAnimator)
        {
            weaponAnimator.SetBool("Rifle Running", state);
        }
    }

    #endregion

    ///<summary>
    ///Methods & Functions that are responsible for playing the different rifle audio clips
    /// </summary>
    #region Pistol Audio

    public override void PlayGunshot()
    {
        weaponAudio.PlayOneShot(rifleShot, 1.0f);
        Debug.Log("Playing " + rifleShot.name);
    }

    public override void PlayReloading()
    {
        weaponAudio.PlayOneShot(rifleReload, 1.0f);
        Debug.Log("Playing " + rifleReload.name);
    }

    public override void PlayClick()
    {
        weaponAudio.PlayOneShot(rifleClick, 1.0f);
        Debug.Log("Playing " + rifleClick.name);
    }

    #endregion


}
