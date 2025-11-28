using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    private void Awake()
    {
        instance = this;
    }

    public float maxHealth = 100f;
    private float currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;

        UIController.instance.UpdateHealthText(currentHealth);
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            // Verificamos si PlayerController existe
            if (PlayerController.instance != null)
                PlayerController.instance.isDead = true;

            // Verificamos si UIController existe
            if (UIController.instance != null)
                UIController.instance.ShowDeathScreen();

            // Verificamos si AudioManager existe antes de reproducir sonido
            if (AudioManager.instance != null)
                AudioManager.instance.PlaySFX(6);
        }
        else // El jugador sigue vivo
        {
            // PROTECCIÓN EN LA LÍNEA DEL ERROR (Línea 40 aprox)
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlaySFX(7);
            }
            else
            {
                Debug.LogWarning("¡Falta el AudioManager en la escena! No se oyó el daño.");
            }
        }

        if (UIController.instance != null)
            UIController.instance.UpdateHealthText(currentHealth);
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIController.instance.UpdateHealthText(currentHealth);
    }
}
