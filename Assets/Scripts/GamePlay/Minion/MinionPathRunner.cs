using UnityEngine;

public class MinionPathRunner : MonoBehaviour
{
    [SerializeField] private MinionMover _minionMover;
    [SerializeField] private Minion _minion;
    private float _speed;
    private int _currentSlotIndex;
    private PathController _pathController;
    private bool _isMoving;
    private bool _isMovingToNextSlot;

    public void StartPathRunner(PathController pathController, int startSlotIndex, float speed)
    {
        _speed = speed;
        _currentSlotIndex = startSlotIndex;
		_pathController = pathController;
		_isMoving = true;
        _isMovingToNextSlot = true;
		_minion.SetState(MinionState.MovingToPath);
        _minion.MoveTo(_pathController.GetSlotPosition(_currentSlotIndex), _speed, OnArriveSlot);
	}

	private void Update()
	{
        if (!_isMoving) return;
        if (_isMovingToNextSlot) return;
        //if (TryEnterHoleInCurrentSlot()) return;
        TryMoveToNextSlot();
	}

    private void OnArriveSlot()
    {
		_isMovingToNextSlot = false;
        if (!_isMoving) return;

        if(TryEnterHoleInCurrentSlot())
        {
            return;
        }

        TryMoveToNextSlot();
	}

    private void TryMoveToNextSlot()
    {
        if(!_isMoving) return;
        if (_isMovingToNextSlot) return;

        bool canReserve = _pathController.TryGetNextSlot(_minion, this._currentSlotIndex, out int nextIndex);

        if(!canReserve)
        {
            _minion.SetState(MinionState.WaitToNextSlot);
            return;
        }
		_isMovingToNextSlot = true;

		if (_pathController.IsLooping(_currentSlotIndex, nextIndex))
		{
            _minionMover.transform.localPosition = _pathController.GetSlotPosition(nextIndex);
			_isMovingToNextSlot = true;
			_currentSlotIndex = nextIndex;
			OnArriveSlot();
			return;
		}

		_currentSlotIndex = nextIndex;
        _isMovingToNextSlot = true;
        _minion.SetState(MinionState.MovingToPath);
        _minionMover.MoveTo(_pathController.GetSlotPosition(_currentSlotIndex), _speed, OnArriveSlot);
	}

    private bool TryEnterHoleInCurrentSlot()
    { 
        Hole hole = _pathController.GetHoleInSlot(_currentSlotIndex);
        if (hole == null) return false;
        if (!hole.CanAccept(_minion)) return false;

        _pathController.RemoveMinionInSlot(_minion, _currentSlotIndex);
        Stop();
        hole.Accept(_minion, () =>
        {
            _pathController.OnMinionCollected?.Invoke();
        });

        return true;
	}

	public void Stop()
    { 
        _isMoving = false;
        _isMovingToNextSlot = false;
        //_pathController = null;
    }

   
}
