using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            FindFirstObjectByType<WeaponsController>().GetAmmo();

            Destroy(gameObject);


            AudioManager.instance.PlaySFX(4);
        }
    }
}
