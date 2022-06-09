using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCoin : MonoBehaviour
{
    [SerializeField] private float travelSpeed;
    public GameObject UICoinGameObject { get; set; }
    public PlayerWalletManager WalletManager { get; set; }
    public int CoinValue { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(UICoinGameObject)
        {
            if (Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(UICoinGameObject.transform.position)) <= 0.1f)
            {
                WalletManager.AddCoins(CoinValue);
                Destroy(gameObject);
            }

            transform.position = Vector2.Lerp(transform.position, Camera.main.ScreenToWorldPoint(UICoinGameObject.transform.position), travelSpeed * Time.deltaTime);
        }
        

    }

    public void InitializeCoin(int coinValue, PlayerWalletManager walletManager, GameObject uiCoinGameObject)
    {
        WalletManager = walletManager;
        CoinValue = coinValue;
        UICoinGameObject = uiCoinGameObject;
    }
}
