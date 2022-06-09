using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MapSelectionCard : MonoBehaviour
{
    [SerializeField] string levelSceneName;
    [SerializeField] TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = PlayerPrefs.GetInt(levelSceneName).ToString();
    }
}
