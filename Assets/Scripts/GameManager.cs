using System;
using TMPro;
using UnityEngine;

public enum GameState
{
    NotStarted,
    GameIsOn,
    GameIsOver
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject foreground = null;
    [SerializeField] private TMP_Text text = null;

    public GameState state { get; private set; } = GameState.NotStarted;

    private string startMessage = "Tap to start";
    private string winnerMessage;

    public Action NeedReload;
    public Action NeedSave;

    private void Update()
    {
        if (state == GameState.NotStarted && 
            (Input.touchCount > 0 || Input.GetMouseButtonDown(0)))
        {
            SetState(GameState.GameIsOn);
        }
        else if(state == GameState.GameIsOver &&
            (Input.touchCount > 0 || Input.GetMouseButtonDown(0)))
        {
            foreground.SetActive(false);

            Restart();
        }
    }

    public void GameIsOver(string winner)
    {
        winnerMessage = winner;
        SetState(GameState.GameIsOver);
    }

    public void SetState(GameState state)
    {
        this.state = state;

        if(state == GameState.NotStarted)
        {
            foreground.SetActive(true);
            text.text = startMessage;
        }
        else if (state == GameState.GameIsOver)
        {
            foreground.SetActive(true);
            text.text = winnerMessage;
        }
        else
        {
            foreground.SetActive(false);
        }
    }

    public void Restart()
    {
        NeedReload?.Invoke();
    }

    private void OnApplicationQuit()
    {
        NeedSave?.Invoke();
    }
}