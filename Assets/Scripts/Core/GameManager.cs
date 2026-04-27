using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    None,
    Playing,
    Win,
    Lose
}

public static class GameEvent
{
	public static Action OnGameStart;
	public static Action OnWin;
	public static Action OnLose;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private GameState gameState = GameState.None;

	private void Awake()
	{
		if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
	}

	private void Start()
	{
        GameStart();
	}

    public void SetState(GameState state)
    {
        gameState = state;
    }

	public bool IsPlaying => gameState == GameState.Playing;

    public void GameStart()
    {
        SetState(GameState.Playing);
		GameEvent.OnGameStart?.Invoke();
	}

    public void Lose()
    {
		SetState(GameState.Lose);
		GameEvent.OnLose?.Invoke();
        Debug.Log("Lose");
	}

    public void Win()
    {
		SetState(GameState.Win);
		GameEvent.OnWin?.Invoke();
		Debug.Log("Win");
	}

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(0).buildIndex);
    }
}
