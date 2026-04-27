using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private Button _restartBtn;
	[SerializeField] private ResultPanel _result;

	private void OnEnable()
	{
		LevelRulesEvent.OnChangeTimer += OnUpdateTimer;
		GameEvent.OnWin += OnWin;
		GameEvent.OnLose += OnLose;
		GameEvent.OnGameStart += OnGameStart;
		_restartBtn.onClick.AddListener(OnRestart);
	}

	private void OnDisable()
	{
		LevelRulesEvent.OnChangeTimer -= OnUpdateTimer;
		GameEvent.OnWin -= OnWin;
		GameEvent.OnLose -= OnLose;
		GameEvent.OnGameStart -= OnGameStart;
		_restartBtn.onClick.RemoveAllListeners();
	}

	private void OnUpdateTimer(float currentTime, float TotalTime)
    {
		_timeText.text = Math.Ceiling(currentTime).ToString();
	}

	private void OnWin()
	{
		_result.Show(true);
	}

	private void OnLose()
	{
		_result.Show(false);
	}

	private void OnGameStart()
	{
		_result.Hide();
	}

	private void OnRestart()
	{
		GameManager.Instance.Restart();
	}
}
