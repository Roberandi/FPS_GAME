using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float range;


    public GameObject muzzleFlare;

    public float flareDisplayTime = .1f;

    public bool canAutoFire;
    public float timeBetweenShots = .1f;

    public int currentAmmo = 100;
    public int clipSize = 15;
    public int remainingAmmo = 300;

    public int pickupAmount;

    public float damageAmount = 15f;

    public int sfxIndex;
}
