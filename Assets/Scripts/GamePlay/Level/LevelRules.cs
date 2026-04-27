using System;
using UnityEngine;

public static class LevelRulesEvent
{
	public static Action<float, float> OnChangeTimer;
}

public class LevelRules : MonoBehaviour
{
    [SerializeField] private float _timeLimit = 40f;
	private float _currentTimerLimit = 0f;

	private GameManager _gameManager;

	private void Start()
	{
		_gameManager = GameManager.Instance;
	}

	private void OnEnable()
	{
		GameEvent.OnGameStart += OnGameStart;
	}

	private void OnDisable()
	{
		GameEvent.OnGameStart -= OnGameStart;

	}

	private void Update()
	{
		if (!_gameManager.IsPlaying) return;

		TickTimer();
	}

	private void TickTimer()
	{
		if (_currentTimerLimit <= 0f) return;

		_currentTimerLimit -= Time.deltaTime;
		if (_currentTimerLimit <= 0f ) _currentTimerLimit = 0;
		LevelRulesEvent.OnChangeTimer?.Invoke(_currentTimerLimit, _timeLimit);

		if(_currentTimerLimit == 0)
		{
			_gameManager.Lose();
		}
	}

	private void OnGameStart()
	{
		_currentTimerLimit = _timeLimit;
	}
}
