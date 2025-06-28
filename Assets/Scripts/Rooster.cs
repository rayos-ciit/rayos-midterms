using UnityEngine;
using System.Collections;

public class Rooster : MonoBehaviour
{
    private float spawnTime;

    public void Initialize()
    {
        spawnTime = Time.time;
        StartCoroutine(Lifecycle());
    }

    IEnumerator Lifecycle()
    {
        // Roosters live for exactly 40 seconds
        yield return new WaitForSeconds(40f);

        // Update counter and destroy
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdateCounter("Rooster", -1);
        }
        Destroy(gameObject);
    }

}
