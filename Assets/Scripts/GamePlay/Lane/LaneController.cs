using System.Collections.Generic;
using UnityEngine;

public class LaneController : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;
    [Header("Queue Config")]
    [SerializeField] private float _spacing = 0.6f;
    [SerializeField] private float _shiftSpeed = 4f;
    [SerializeField] private float _releaseCoolDown = 0.2f;

    private MinionFactory _minionFactory;
    private List<Minion> _minionQueue = new List<Minion>();
    private float _lastReleaseTimer = 0;
    public int MinionCount => _minionQueue.Count;
    public bool HasMinion => _minionQueue.Count > 0;

	public void Setup(IReadOnlyList<MinionColor> minions, MinionFactory minionFactory)
    {
        Clear();
        _minionFactory = minionFactory;

		for (int i = 0; i < minions.Count; i++)
        {
            Minion minion = _minionFactory.Create(minions[i]);
            _minionQueue.Add(minion);
            minion.SetState(MinionState.InQueue);
            minion.transform.position = GetQueuePosition(i);
            minion.transform.rotation = this.transform.rotation;
		}
    }

    private bool CanRelease()
    {
        if(!HasMinion) return false;
        float time = Time.time - _lastReleaseTimer;
        return time > _releaseCoolDown;
	}

    public Minion ReleaseFrontMinion()
    {
        if (!CanRelease()) return null;

        Minion frontMinion = _minionQueue[0];
        _minionQueue.RemoveAt(0);
        ShiftQueueForward();
		frontMinion.SetState(MinionState.MovingToPath);
		return frontMinion;
    }    

    private void ShiftQueueForward()
    {
        for(int i = 0; i < MinionCount; i++)
        {
            Minion minion = _minionQueue[i];
            Vector3 targetPosition = GetQueuePosition(i);
            minion.MoveTo(targetPosition, _shiftSpeed);
		}
    }

    private void Clear()
    {
		_minionQueue.Clear();
	}

    private Vector3 GetQueuePosition(int index)
    {
        Vector3 backwardDirection = -this.transform.forward;
        return _startPoint.position + backwardDirection * _spacing * index;
    }

}
