using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { menu,inGame,fail,success}
public class GameManager : BaseManager
{
    public GameState currentState;
    public PlayerController playerController;
    
    void Start()
    {
        Application.targetFrameRate = 60;
        currentState = GameState.inGame;
    }
    public void ResetCharacter()
    {
        playerController.ResetCharacter();
    }
   
    void Update()
    {
        
    }
    public void StartGame()
    {
        EventManager.levelStartEvent?.Invoke();
    }
    public void CheckEndGame()//her elenen oyuncuda burayi kontrol ederek oyunun bitip bitmedigine karar veriliyor
    {
        if (ManagerHub.Get<PlayersManager>().Players.Count<=1)
        {
            EventManager.levelSuccessEvent?.Invoke();
            currentState = GameState.fail;
        }
    }

    internal void SetGameState(GameState state)
    {
        currentState = state;
    }
}
