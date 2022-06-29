using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameController : MonoBehaviour
{
    

    [SerializeField] 
    private GameObject _startScreen;
    [SerializeField] 
    private GameObject _endScreen;
    [SerializeField]
    private GameObject _player;
    
    void Start()
    {
        GameEvents.OnStartScreenEvent += OnStartScreen;
        GameEvents.OnStartGameEvent += OnStartGame;
        GameEvents.OnGameOverEvent += OnGameOver;
    }

    private void OnDestroy()
    {
        GameEvents.OnStartScreenEvent -= OnStartScreen;
        GameEvents.OnStartGameEvent -= OnStartGame;
        GameEvents.OnGameOverEvent -= OnGameOver;
    }

    private void OnStartScreen()
    {
        _endScreen.SetActive(false);
        _startScreen.SetActive(true);
    }

    private void OnStartGame()
    {
        
        _player.SetActive(true);
        _startScreen.SetActive(false);
    }

    
    //Called from a Unity button
    public void ButtonStartGame()
    {
        GameManager.Instance.StartGame();
    }

    private void OnGameOver(){
        _startScreen.SetActive(false);
        _endScreen.SetActive(true);
    }
    
    public void ButtonBack()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void CloseGame(){
        Application.Quit();
    }
}
