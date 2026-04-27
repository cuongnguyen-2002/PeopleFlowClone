using UnityEngine;

public class LevelController : MonoBehaviour
{
	private GameManager _gameManager;
	[SerializeField] private PathController _pathController;
	[SerializeField] private LaneController _laneController;

	private int _minionCount = 0;
	private int _totalMinion = 0;

	private void Start()
	{
		_gameManager = GameManager.Instance;
	}

	private void OnEnable()
	{
		_pathController.OnMinionCollected += HandleMinionCollected;
	}

	private void OnDisable()
	{
		_pathController.OnMinionCollected -= HandleMinionCollected;

	}

	public void Setup(int totalMinion)
	{
		_totalMinion = totalMinion;
		_minionCount = 0;
	}

	private bool IsPlaying()
	{
		return _gameManager != null && _gameManager.IsPlaying;
	}

	public void TrySendMinionFromLane(LaneController lane)
	{
		if (!IsPlaying()) return;
		if(lane == null) return;
		if(!_pathController.CanEnter() && _pathController.IsFull())
		{
			_gameManager.Lose();
			return;
		}

		Minion minion = _laneController.ReleaseFrontMinion();

		if(minion == null) return;

		_pathController.AddMinion(minion);

	}

	public void MinionCollected()
	{
		_minionCount++;
	}

	public bool IsComplete()
	{
		return _minionCount >= _totalMinion;
	}

	public void HandleMinionCollected()
	{
		if (!IsPlaying()) return;

		MinionCollected();

		if(IsComplete())
		{
			_gameManager.Win();
			Debug.Log("Win");
		}
	}
}
