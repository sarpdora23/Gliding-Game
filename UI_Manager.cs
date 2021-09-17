using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public void StartButton(GameObject button)
    {
        StartCoroutine(StartDelay());
        button.SetActive(false);
    }
    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(0.3f);
        GameManager.gameManager_Instance.SetCurrentState(GameManager.GameStates.BALL_PREPARE);
    }
}
