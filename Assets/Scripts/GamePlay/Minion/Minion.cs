using System;
using System.Collections.Generic;
using UnityEngine;

public enum MinionState
{
	InQueue,
	MovingToPath,
	JumpingToHole,
    WaitToNextSlot,
	Collected
}

public class Minion : MonoBehaviour
{
    public MinionColor MinionColor { get; private set; }
    public MinionState MinionState { get; private set; }

    [SerializeField] private MinionSkinMesh _meshRenderer;
    [SerializeField] private MinionMover _minionMover;
    [SerializeField] private MinionPathRunner _minionPathRunner;

    public void Setup(MinionColor color, Material material)
    {
        MinionColor = color;
		_meshRenderer.SetMaterial(material);
        MinionState = MinionState.InQueue;
	}

    public void SetState(MinionState newState)
    {
        MinionState = newState;
    }

    public void MoveTo(Vector3 target, float speed, Action onComplete = null)
    {
        _minionMover.MoveTo(target, speed, onComplete);
    }

    public void MoveToPath(PathController pathController, int slotIndex ,float speed)
    {
		_minionPathRunner.StartPathRunner(pathController, slotIndex, speed);
	}

    public void DeSpawn()
    {
        //back to pool
        _minionMover.StopMove();
        _minionPathRunner.Stop();

        this.gameObject.SetActive(false);

	}
}
