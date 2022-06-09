using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    [Header("Start/ Change Game Speed Button")]
    [SerializeField]
    Image startChangeSpeedButtonImage;
    [SerializeField]
    TextMeshProUGUI startChangeSpeedText;
    [SerializeField]
    Sprite fastForwardSprite;
    [SerializeField]
    Sprite startSprite;
    [Header("Pause Menu")]
    [SerializeField]
    GameObject pauseMenuObject;
    [SerializeField]
    AudioMixer musicMixer;
    [SerializeField]
    AudioMixer sfxMixer;
    [SerializeField]
    Slider masterSlider;
    [SerializeField]
    Slider musicSlider;
    [SerializeField]
    Slider sfxSlider;
    [SerializeField]
    GameObject tutorial;
    [SerializeField]
    EnemySpawner enemySpawner;

    private float _currentSpeed = 1f;

    private void Start()
    {
        Time.timeScale = 1f;
        masterSlider.value = PlayerPrefs.GetFloat("masterVolume");
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        musicMixer.SetFloat("musicVolume", Mathf.Log10(musicSlider.value) * 20f);
        sfxMixer.SetFloat("sfxVolume", Mathf.Log10(sfxSlider.value) * 20f);
    }

    private void FastForward()
    {
        Time.timeScale = 1.5f;
        startChangeSpeedText.text = "FAST FORWARD";
        _currentSpeed = 1.5f;
    }

    private void SetSpeedToNormal()
    {
        Time.timeScale = 1f;
        startChangeSpeedText.text = "NORMAL SPEED";
        _currentSpeed = 1f;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenuObject.SetActive(true);
    }

    public void UnPauseGame()
    {
        Time.timeScale = _currentSpeed;
        pauseMenuObject.SetActive(false);
    }

    public void ToggleSpeed()
    {
        if (Time.timeScale == 0)
            return;

        if(_currentSpeed == 1f)
        {
            FastForward();
            return;
        }
        SetSpeedToNormal();
    }

    public void ChangeMasterVolume()
    {
        AudioListener.volume = masterSlider.value;
        PlayerPrefs.SetFloat("masterVolume", masterSlider.value);
    }

    public void ChangeMusicVolume()
    {
        musicMixer.SetFloat("musicVolume", Mathf.Log10(musicSlider.value) * 20f);
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
    }

    public void ChangeSfxVolume()
    {
        sfxMixer.SetFloat("sfxVolume", Mathf.Log10(sfxSlider.value) * 20f);
        PlayerPrefs.SetFloat("sfxVolume", sfxSlider.value);
    }

    public void CloseTutorial()
    {
        tutorial.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void SpawnNextWave()
    {
        enemySpawner.SpawnNextWave();
    }
}
