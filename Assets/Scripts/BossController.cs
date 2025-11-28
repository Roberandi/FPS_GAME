using UnityEngine;

public class BossController : MonoBehaviour
{
    public Transform[] ammoPoints;

    public GameObject ammoPickup;

    public float ammoSpawnTime;
    private float ammoCounter;

    public float checkInterval;
    private float checkCounter;

    public GameObject levelExit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ammoCounter = ammoSpawnTime;

        checkCounter = checkInterval;

        AudioManager.instance.PlayBossMusic();
    }

    // Update is called once per frame
    void Update()
    {
        checkCounter -= Time.deltaTime;

        if (checkCounter <= 0)
        {
            checkCounter = checkInterval;

            if(FindFirstObjectByType<EnemyController>() == null)
            {
                Debug.Log("Boss Is Defeated");

                gameObject.SetActive(false);

                levelExit.SetActive(true);

                AudioManager.instance.PlayLevelMusic();
            }
        }


        ammoCounter -= Time.deltaTime;
        if(ammoCounter <= 0)
        {
            ammoCounter = ammoSpawnTime;

            Instantiate(ammoPickup, ammoPoints[Random.Range(0, ammoPoints.Length)].position, Quaternion.identity);
        }
    }
}
