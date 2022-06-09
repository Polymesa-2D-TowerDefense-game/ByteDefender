using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    int startingHealth;
    [SerializeField]
    TextMeshProUGUI healthText;
    [SerializeField]
    GameObject GameOverPanel;

    int _currentHealth;

    private void Start()
    {
        _currentHealth = startingHealth;
    }


    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        healthText.text = _currentHealth.ToString();
        GetComponent<AudioSource>().Play();
        if (_currentHealth < 0)
        {
            GameOverPanel.SetActive(true);
        }
    }
}
