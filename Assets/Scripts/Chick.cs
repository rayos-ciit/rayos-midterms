using UnityEngine;
using System.Collections;

public class Chick : MonoBehaviour
{
    private bool isFirstGen;
    private float spawnTime;

    public void Initialize(bool firstGen)
    {
        isFirstGen = firstGen;
        spawnTime = Time.time;
        StartCoroutine(Mature());
    }

    IEnumerator Mature()
    {
        // Chick matures after 10 seconds
        yield return new WaitForSeconds(10f);

        // Update counter before transforming
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdateCounter("Chick", -1);
        }

        // Cache position before destruction
        Vector3 position = transform.position;
        Destroy(gameObject);

        // Determine next life stage
        if (isFirstGen)
        {
            // First-gen chick always becomes a hen
            GameManager.Instance.SpawnEntity(
                GameManager.Instance.henPrefab,
                position,
                true  // Pass along first-gen status
            );
        }
        else
        {
            // Regular chicks have 50% chance to become hen or rooster
            bool becomesHen = Random.value > 0.5f;
            GameObject nextPrefab = becomesHen ?
                GameManager.Instance.henPrefab :
                GameManager.Instance.roosterPrefab;

            GameManager.Instance.SpawnEntity(
                nextPrefab,
                position,
                false  // Never first-gen after this point
            );
        }
    }

}