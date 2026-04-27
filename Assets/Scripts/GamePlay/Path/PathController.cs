using System;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    [SerializeField] private List<PathSlot> _pathSlots = new List<PathSlot>();
    private float _moveSpeed = 2f;
	public Action OnMinionCollected;
	public bool IsFull()
    {
        foreach(var slot in _pathSlots)
        {
            if(slot.IsEmpty) return false;
        }

        return true;
    }

    public bool CanEnter()
    {
        return _pathSlots.Count > 0 && _pathSlots[0].IsEmpty;
    }

    public void AddMinion(Minion minion)
    {
        if(!CanEnter())
        {
            Minion current = _pathSlots[0].CurrentMinion;
            TryGetNextSlot(current, 0, out int nextIndex);
			current.MoveToPath(this, nextIndex, _moveSpeed);
		}

        _pathSlots[0].Occupy(minion);
        minion.SetState(MinionState.MovingToPath);
        minion.transform.parent = this.transform;
        minion.MoveToPath(this, 0, _moveSpeed);
	}

	public bool TryGetNextSlot(Minion minion, int currentIndex, out int nextIndex)
    {
        nextIndex = NextSlotIndex(currentIndex);
        if(!IsValidIndex(nextIndex) || !IsValidIndex(currentIndex)) return false;

        PathSlot currentSlot = _pathSlots[currentIndex];
        PathSlot nextSlot = _pathSlots[nextIndex];

        if (!nextSlot.IsEmpty) return false;

        if(currentSlot.CurrentMinion == minion)
        {
            currentSlot.Clear();
        }

        nextSlot.Occupy(minion);

        return true;
	}

    private int NextSlotIndex(int currentIndex)
    {
        return (currentIndex + 1) % _pathSlots.Count;
    }

    private bool IsValidIndex(int currentIndex)
    {
        return currentIndex >= 0 && currentIndex < _pathSlots.Count;
    }

    public Vector3 GetSlotPosition(int slotIndex)
    {
        if(!IsValidIndex(slotIndex)) return this.transform.position;
        return _pathSlots[slotIndex].transform.localPosition;
    }

    public Hole GetHoleInSlot(int slotIndex)
    {
        if (!IsValidIndex(slotIndex)) return null;
        return _pathSlots[slotIndex].Hole;
	}

    public void RemoveMinionInSlot(Minion minion,int slotIndex)
    {
        if(!IsValidIndex(slotIndex) || minion == null) return;

        if (_pathSlots[slotIndex].CurrentMinion == minion)
        {
            _pathSlots[slotIndex].Clear();
        }
    }

	public bool IsLooping(int currentIndex, int nextIndex)
	{
		if (!IsValidIndex(currentIndex) || !IsValidIndex(nextIndex)) return false;

		PathSlot currentSlot = _pathSlots[currentIndex];
		PathSlot nextSlot = _pathSlots[nextIndex];
        return currentSlot.SlotType == SlotType.End && nextSlot.SlotType == SlotType.Start;
	}
}


