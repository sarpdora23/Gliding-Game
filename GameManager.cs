using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameStates current_state;
    private static GameManager gameManager = null;
    private void Start()
    {
        current_state = GameStates.BEFORE_GAME;
    }
    public static GameManager gameManager_Instance
    {
        get
        {
            if (gameManager == null)
            {
                gameManager = new GameObject("GameManager").GetComponent<GameManager>();
            }
            return gameManager;
        }
    }
    private void OnEnable()
    {
        gameManager = this;
    }
    public GameStates GetCurrentState()
    {
        return current_state;
    }
    public void SetCurrentState(GameStates new_state)
    {
        current_state = new_state;
    }
    public enum GameStates 
    {
        BEFORE_GAME,
        BALL_PREPARE,
        BALL_FLY,
    }
}
