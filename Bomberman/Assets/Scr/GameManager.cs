using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;
    
    
    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    private void Start()
    {
        ReturnToMenu();
    }

    private void OnDestroy()
    {

    }
    
    public void ReturnToMenu()
    {
        GameEvents.OnStartScreenEvent?.Invoke();
    }
    
    public void StartGame()
    {
        GameEvents.OnStartGameEvent?.Invoke();
    }

    public void GameOver()
    {
        GameEvents.OnGameOverEvent?.Invoke();

    }
    
}
