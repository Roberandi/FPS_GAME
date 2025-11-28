using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float healAmount = 25f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerHealthController.instance.Heal(healAmount);

            Destroy(gameObject);

            AudioManager.instance.PlaySFX(5);
        }
    }
}
