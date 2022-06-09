using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemySpawner : MonoBehaviour
{
    [SerializeField] int enemyHealth;
    [SerializeField] float enemyMoveSpeed;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject cryptedEnemyPrefab;
    [SerializeField] GameObject zippedEnemyPrefab;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] PlayerWalletManager playerWallet;

    private List<Vector2> _path;

    // Start is called before the first frame update
    void Start()
    {
        _path = GameObject.FindObjectOfType<EnemyPathManager>().EnemyPath;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            
            newEnemy.GetComponent<Enemy>().Health = enemyHealth;
            newEnemy.GetComponent<Enemy>().MoveSpeed = enemyMoveSpeed;
            newEnemy.GetComponent<Enemy>().Coins = enemyHealth;
            newEnemy.GetComponent<Enemy>().PlayerHealthManager = playerHealth;
            newEnemy.GetComponent<Enemy>().PlayerWallet = playerWallet;
            newEnemy.GetComponent<Enemy>().SetPath(_path, 0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameObject newEnemy = Instantiate(cryptedEnemyPrefab, transform.position, Quaternion.identity);
            
            newEnemy.GetComponent<Enemy>().Health = enemyHealth;
            newEnemy.GetComponent<Enemy>().MoveSpeed = enemyMoveSpeed;
            newEnemy.GetComponent<Enemy>().Coins = enemyHealth;
            newEnemy.GetComponent<Enemy>().PlayerHealthManager = playerHealth;
            newEnemy.GetComponent<Enemy>().PlayerWallet = playerWallet;
            newEnemy.GetComponent<Enemy>().SetPath(_path, 0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameObject newEnemy = Instantiate(zippedEnemyPrefab, transform.position, Quaternion.identity);
            
            newEnemy.GetComponent<Enemy>().Health = enemyHealth;
            newEnemy.GetComponent<Enemy>().MoveSpeed = enemyMoveSpeed;
            newEnemy.GetComponent<Enemy>().Coins = enemyHealth;
            newEnemy.GetComponent<Enemy>().PlayerHealthManager = playerHealth;
            newEnemy.GetComponent<Enemy>().PlayerWallet = playerWallet;
            newEnemy.GetComponent<Enemy>().SetPath(_path, 0);
        }
    }
}
