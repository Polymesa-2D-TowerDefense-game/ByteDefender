using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    TextMesh hitPointsText;
    public int Health { get; set; }
    public float MoveSpeed { get; set; }
    public int Coins { get; set; }

    public PlayerHealth PlayerHealthManager { get; set; }
    public PlayerWalletManager PlayerWallet { get; set; }

    // Path related variables
    protected List<Vector2> _path;
    protected int _pathIndex;

    public void SetPath(List<Vector2> path, int startingPathIndex)
    {
        _pathIndex = startingPathIndex;
        _path = path;
        hitPointsText.text = Health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        FollowPath();
    }

    private void FollowPath()
    {
        if (_pathIndex < _path.Count)
        {
            transform.position = Vector2.MoveTowards(transform.position, _path[_pathIndex], MoveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, _path[_pathIndex]) < 0.1f)
                _pathIndex++;
        }
        else
        {
            PlayerHealthManager.TakeDamage(1);
            Destroy(gameObject);
        }
            
    }

    protected void LooseHealth(int damage)
    {
        Health -= damage;
        hitPointsText.text = Health.ToString();
        GetComponent<AudioSource>().Play();
        if (Health <= 0)
        {
            PlayerWallet.AddCoins(Coins);
            Destroy(this.gameObject);
        }
            
    }    
}
