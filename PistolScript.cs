using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolScript : WeaponScriptUpdated
{
    ///<summary>
    ///Audio Clips that the Pistol will use.
    ///</summary>
    [Header("Pistol Audio")]
    [SerializeField] AudioClip pistolShot;
    [SerializeField] AudioClip pistolReloading;
    [SerializeField] AudioClip pistolClick;

    ///<summary>
    ///Particle Effects for the Pistol.
    ///</summary>
    [Header("Pistol Particle Effects")]
    [SerializeField] ParticleSystem pistolMuzzleFlash;
    [SerializeField] ParticleSystem pistolSmoke;

    protected override void Update()
    {
        //Weapon has been fired. Play animation for firing.
        if (fired == true)
        {
            animationWaitTime -= Time.deltaTime;

            if (animationWaitTime < 0)
            {
                weaponAnimator.SetBool("Pistol Fire", false);
                animationWaitTime = 1.0f;
                fired = false;
                currentRange = Vector3DMaths.Distance(transform, currentEnemy.transform);
                Debug.Log("Damage Dealt = " + DamageDealt());
            }
        }
    }

    ///<summary>
    ///Functions that are unique to the pistol class.
    ///This Region also contains any overrides of base functions from the parent class.
    ///</summary>
    #region Pistol Functions

    public override void WeaponFire()
    {
        Debug.Log("Pistol Fired");

        if (weaponAnimator)
        {
            weaponAnimator.SetBool("Pistol Fire", true);
            Debug.Log("Firing Pistol Animation Played");

            pistolMuzzleFlash.Play();
            pistolSmoke.Play();

            numberOfBulletsLeft--;
            fired = true;

            enemyClass.DecreaseEnemyHealth(DamageDealt());
        }
    }

    public override void WeaponRun(bool state)
    {
        if (weaponAnimator)
        {
            weaponAnimator.SetBool("Pistol Running", state);
        }
    }

    #endregion

    #region Courtines

    public override IEnumerator ReloadingTime()
    {
        weaponAnimator.SetBool("Pistol Reloading", true);
        yield return new WaitForSeconds(1.0f);
        weaponAnimator.SetBool("Pistol Reloading", false);
        numberOfBulletsLeft = bullets;
    }

    #endregion


    ///<summary>
    ///Methods & Functions that are responsible for playing the different pistol audio clips
    ///</summary>
    #region Pistol Audio

    public override void PlayGunshot()
    {
        weaponAudio.PlayOneShot(pistolShot, 1.0f);
        Debug.Log("Playing " + pistolShot.name);
    }

    public override void PlayReloading()
    {
        weaponAudio.PlayOneShot(pistolReloading, 1.0f);
        Debug.Log("Playing " + pistolReloading.name);
    }

    public override void PlayClick()
    {
        weaponAudio.PlayOneShot(pistolClick, 1.0f);
        Debug.Log("Playing " + pistolClick.name);
    }

    #endregion

   

}
