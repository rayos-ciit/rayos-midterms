using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Prefabs")]
    public GameObject eggPrefab;
    public GameObject chickPrefab;
    public GameObject henPrefab;
    public GameObject roosterPrefab;
    public Transform spawnPoint;

    [Header("UI")]
    public TMP_Text eggCountText;
    public TMP_Text chickCountText;
    public TMP_Text henCountText;
    public TMP_Text roosterCountText;

    [Header("Counters")]
    public int eggs;
    public int chicks;
    public int hens;
    public int roosters;

    private float gameStartTime;

    void Awake()
    {
        // Singleton pattern with early return
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        gameStartTime = Time.time;
        SpawnFirstEgg();
    }

    void SpawnFirstEgg()
    {
        SpawnEntity(eggPrefab, spawnPoint.position, true);
    }

    // Unified spawn method
    public void SpawnEntity(GameObject prefab, Vector3 position, bool isFirstGen = false)
    {
        var obj = Instantiate(prefab, position, Quaternion.identity);

        switch (prefab.tag)
        {
            case "Egg":
                obj.GetComponent<Egg>().Initialize(isFirstGen);
                eggs++;
                break;
            case "Chick":
                obj.GetComponent<Chick>().Initialize(isFirstGen);
                chicks++;
                break;
            case "Hen":
                obj.GetComponent<Hen>().Initialize(isFirstGen);
                hens++;
                break;
            case "Rooster":
                obj.GetComponent<Rooster>().Initialize();
                roosters++;
                break;
        }

        UpdateUI();
    }

    public void UpdateCounter(string type, int change)
    {
        switch (type)
        {
            case "Egg": eggs += change; break;
            case "Chick": chicks += change; break;
            case "Hen": hens += change; break;
            case "Rooster": roosters += change; break;
        }
        UpdateUI();
    }

    void UpdateUI()
    {
        if (eggCountText) eggCountText.text = $"Eggs: {eggs}";
        if (chickCountText) chickCountText.text = $"Chicks: {chicks}";
        if (henCountText) henCountText.text = $"Hens: {hens}";
        if (roosterCountText) roosterCountText.text = $"Roosters: {roosters}";
    }

    public bool IsFirstHenLayTime()
    {
        return Time.time - gameStartTime >= 30f;
    }
}