using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    public static KitchenGameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;
    public event EventHandler OnGameOver;

    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    private State state;
    private float waitingToStartTimer = 1f;
    private float countdownToStartTimer = 3f;
    private bool isGamePaused = false;

    //private float gamePlayingTimer;
    //private float gamePlayingTimerMax = 120f;


    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Start()
    {
        if(GameInput.Instance != null)
        {
            GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        }
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if(waitingToStartTimer < 0f)
                {
                    state=State.CountdownToStart;
                    OnStateChanged?.Invoke(this, new EventArgs());
                }
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if(countdownToStartTimer < 0f)
                {
                    state=State.GamePlaying;
                    //gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, new EventArgs());
                }
                break;
            case State.GamePlaying:
                //gamePlayingTimer -= Time.deltaTime;
                //if (gamePlayingTimer < 0f)
                //{
                //    state = State.GameOver;
                //    OnStateChanged?.Invoke(this, new EventArgs());
                //}
                if(DeliveryManager.Instance && DeliveryManager.Instance.IsGameOver())
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, new EventArgs());
                }
                break;
            case State.GameOver:
                OnGameOver?.Invoke(this, new EventArgs());
                break;
        }
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }
    public bool IsCountdownToStartActive()
    {
        return state == State.CountdownToStart;
    }

    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer;
    }
    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    //public float GetGamePlayingTimerNormalized()
    //{
    //    return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    //}

    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0f;

            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;

            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }
}
