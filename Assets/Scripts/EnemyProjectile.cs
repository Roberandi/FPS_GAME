using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float moveSpeed = 10f, damageAmount = 15f;

    public Rigidbody theRB;

    public GameObject impactEffect, damageEffect;

    public float lifetime = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        theRB.linearVelocity = transform.forward * moveSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Instantiate(damageEffect, transform.position, Quaternion.identity);

            //Debug.Log("Damaging Player for " + damageAmount);

            PlayerHealthController.instance.TakeDamage(damageAmount);

        } else
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
