using System.Collections.Generic;
using UnityEngine;
public enum SlotType
{
    None,
    Start,
    End
}

public class PathSlot : MonoBehaviour
{
    public Minion CurrentMinion { get; private set; }
    [SerializeField] private Hole _hole;
    [SerializeField] private SlotType _slotType;
    public SlotType SlotType => _slotType;

	public Hole Hole => _hole;
    public bool IsEmpty => CurrentMinion == null;

    public void Occupy(Minion minion)
    {
        CurrentMinion = minion;
    }

    public void Clear()
    {
        CurrentMinion = null;
    }
}


