using UnityEngine;
using System.Collections;

public class Hen : MonoBehaviour
{
    private bool isFirstGen;
    private float spawnTime;

    public void Initialize(bool firstGen)
    {
        isFirstGen = firstGen;
        spawnTime = Time.time;
        StartCoroutine(Lifecycle());
    }

    IEnumerator Lifecycle()
    {
        // Egg laying timing
        if (isFirstGen)
        {
            // First hen waits until 30 seconds game time
            while (!GameManager.Instance.IsFirstHenLayTime())
            {
                yield return null;
            }
        }
        else
        {
            // Regular hens wait 30 seconds after maturing
            yield return new WaitForSeconds(30f);
        }

        LayEggs();

        // Perish 10 seconds after laying eggs (40s total for first gen)
        yield return new WaitForSeconds(10f);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdateCounter("Hen", -1);
        }
        Destroy(gameObject);
    }

    void LayEggs()
    {
        int eggCount = Random.Range(2, 11); // Lay 2-10 eggs
        for (int i = 0; i < eggCount; i++)
        {
            Vector3 spawnPos = transform.position +
                new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));
            GameManager.Instance.SpawnEntity(GameManager.Instance.eggPrefab, spawnPos, false);
        }
    }

}