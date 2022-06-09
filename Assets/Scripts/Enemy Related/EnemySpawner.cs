using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    [Header("Basic Spawning Configuration")]
    [SerializeField]
    TextMeshProUGUI waveIndexText;
    [SerializeField] 
    int initialEnemyPower;
    [SerializeField]
    int enemyPowerScaling;
    [SerializeField] [Range(0f, 1f)]
    float minSpawnsAcordingToPowerPercentage;
    [SerializeField] [Range(0f, 1f)]
    float maxSpawnsAcordingToPowerPercentage;

    [Header("Player Rewarding")]
    [SerializeField]
    int coinsPerEnemyHitPoint;
    [SerializeField]
    int coinsPerWave;

    [Header("Special Enemies Apeal Waves and increments")]
    [SerializeField]
    int cryptedEnemyApealWave;
    [SerializeField]
    int cryptedEnemyIncrementPerWave;
    [SerializeField]
    int zippedEnemyApealWave;
    [SerializeField]
    int zippedEnemyIncrementPerWave;
    [SerializeField]
    int doublePowerIcrementPerWave;
    

    [Header("Special Enemies Initial Values and upgrades")]
    [SerializeField]
    int initialCryptedEnemiesToSpawn;
    [SerializeField]
    int cryptedEnemiesToSpawnIncrement;
    [SerializeField]
    int initialZippedEnemiesToSpawn;
    [SerializeField]
    int zippedEnemiesToSpawnIncrement;

    [Header("Enemy Prefabs")]
    [SerializeField]
    GameObject normalEnemyPrefab;
    [SerializeField]
    GameObject cryptedEnemyPrefab;
    [SerializeField]
    GameObject zippedEnemyPrefab;

    [Header("Spawning Rate and Enemy Speed")]
    [SerializeField]
    float initialEnemyMoveSpeed;
    [SerializeField]
    float enemyMoveSpeedScaling;
    [SerializeField]
    float initialSpawnSpeed;
    [SerializeField]
    float spawnSpeedScaling;

    // Private Variables
    private int _enemyPower;
    private int _waveCount = 0;
    private int _cryptedEnemiesToSpawn = 0;
    private int _zippedEnemiesToSpawn = 0;

    private float _enemySpeed;
    private float _spawnRate;

    //Components
    private List<Vector2> _enemyPath;
    private PlayerWalletManager _playerWallet;
    private PlayerHealth _playerHealth;

    // Monitoring
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    void Start()
    {
        // Get Components
        _enemyPath = FindObjectOfType<EnemyPathManager>().EnemyPath;
        _playerWallet = FindObjectOfType<PlayerWalletManager>();
        _playerHealth = FindObjectOfType<PlayerHealth>();
        // Initialise Values
        _enemyPower = initialEnemyPower;
        _enemySpeed = initialEnemyMoveSpeed;
        _spawnRate = initialSpawnSpeed;
    }

    public bool IsWaveRunning()
    {
        int remainingEnemies = 0;
        foreach (var enemy in spawnedEnemies)
            if (enemy)
                remainingEnemies++;

        return remainingEnemies > 0;
    }

    public void SpawnNextWave()
    {
        if (IsWaveRunning())
            return;

        ClearAllProjectiles();

        spawnedEnemies.Clear();
        _playerWallet.AddCoins(_waveCount * coinsPerWave);

        int[] enemyHealthPointsArray = PowerToSpawns();
        GameObject[] enemyPrefabsArray = DeclareEnemies(enemyHealthPointsArray.Length);

        StartCoroutine(SpawnWave(enemyHealthPointsArray, enemyPrefabsArray));

        WaveUpgrades();
    }

    private void ClearAllProjectiles()
    {
        var projectiles = FindObjectsOfType<Projectile>();
        foreach (var projectile in projectiles)
            Destroy(projectile.gameObject);
    }

    private void WaveUpgrades()
    {
        _enemyPower += enemyPowerScaling;
        if (_waveCount == cryptedEnemyApealWave)
            _cryptedEnemiesToSpawn++;
        if (_waveCount == zippedEnemyApealWave)
            _zippedEnemiesToSpawn++;

        if (_waveCount % cryptedEnemyIncrementPerWave == 0 && _waveCount >= cryptedEnemyApealWave)
            _cryptedEnemiesToSpawn += cryptedEnemiesToSpawnIncrement;
        if (_waveCount % zippedEnemyIncrementPerWave == 0 && _waveCount >= zippedEnemyApealWave)
            _zippedEnemiesToSpawn += zippedEnemiesToSpawnIncrement;
        if (_waveCount % doublePowerIcrementPerWave == 0 && _waveCount >= doublePowerIcrementPerWave)
            enemyPowerScaling = enemyPowerScaling * 2;

        _enemySpeed += enemyMoveSpeedScaling;
        _spawnRate += spawnSpeedScaling;
        _waveCount++;
        waveIndexText.text = "WAVE: " + _waveCount.ToString();
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, _waveCount);
    }

    private int[] PowerToSpawns()
    {
        int minSpawns = (int)(minSpawnsAcordingToPowerPercentage * _enemyPower);
        int maxSpawns = (int)(maxSpawnsAcordingToPowerPercentage * _enemyPower);

        int spawns = Random.Range(minSpawns, maxSpawns);

        int remainingPower = _enemyPower - spawns;

        int[] enemyHealthPointsArray = new int[spawns];

        for (int i = 0; i < enemyHealthPointsArray.Length; i++)
            enemyHealthPointsArray[i] = 1;

        while(remainingPower > 0)
        {
            int randomIndex = Random.Range(0, enemyHealthPointsArray.Length);
            enemyHealthPointsArray[randomIndex]++;
            remainingPower--;
        }

        return enemyHealthPointsArray;
    }

    private GameObject[] DeclareEnemies(int enemiesToSpawn)
    {

        List<GameObject> enemyPrefabList = new List<GameObject>();
        for (int i = 0; i < _cryptedEnemiesToSpawn; i++)
            enemyPrefabList.Add(cryptedEnemyPrefab);

        for (int i = 0; i < _zippedEnemiesToSpawn; i++)
            enemyPrefabList.Add(zippedEnemyPrefab);

        int remainingEnemies = enemiesToSpawn - enemyPrefabList.Count;

        for (int i = 0; i < remainingEnemies; i++)
            enemyPrefabList.Add(normalEnemyPrefab);

        System.Random rng = new System.Random();
        enemyPrefabList = enemyPrefabList.OrderBy(a => rng.Next()).ToList();

        return enemyPrefabList.ToArray();
    }

    IEnumerator SpawnWave(int[] enemyHealthPointsArray, GameObject[] enemyPrefabsArray)
    {
        for(int i = 0; i < enemyHealthPointsArray.Length; i++)
        {
            GameObject enemy = Instantiate(enemyPrefabsArray[i], transform.position, Quaternion.identity);
            var enemyComponent = enemy.GetComponent<Enemy>();
            //Initialise Components
            enemyComponent.PlayerWallet = _playerWallet;
            enemyComponent.PlayerHealthManager = _playerHealth;
            // Initialise Values
            enemyComponent.Health = enemyHealthPointsArray[i];
            enemyComponent.MoveSpeed = _enemySpeed;
            enemyComponent.SetPath(_enemyPath, 0);
            enemyComponent.Coins = enemyComponent.Health * coinsPerEnemyHitPoint;
            spawnedEnemies.Add(enemy);

            yield return new WaitForSeconds(_spawnRate);
        }
    }
}
