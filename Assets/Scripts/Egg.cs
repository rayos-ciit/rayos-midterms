using UnityEngine;
using System.Collections;

public class Egg : MonoBehaviour
{
    private bool isFirstGen;
    private float spawnTime;

    public void Initialize(bool firstGen)
    {
        isFirstGen = firstGen;
        spawnTime = Time.time;
        StartCoroutine(Hatch());
    }

    IEnumerator Hatch()
    {
        // Egg hatches after 10 seconds
        yield return new WaitForSeconds(10f);

        // Update counter before transforming
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdateCounter("Egg", -1);
        }

        // Cache position before destruction
        Vector3 position = transform.position;
        Destroy(gameObject);

        // Spawn chick in same position
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SpawnEntity(
                GameManager.Instance.chickPrefab,
                position,
                isFirstGen  // Pass along first-gen status
            );
        }
    }

}