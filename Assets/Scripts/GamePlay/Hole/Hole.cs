using System;
using TMPro;
using UnityEngine;

public class Hole : MonoBehaviour
{
    [SerializeField] private TextMeshPro _holeText;
	[SerializeField] private Transform _targetPosition;
	[SerializeField] private MeshRenderer _meshRenderer;
    private int _capacity;
	private MinionColor _acceptColor;
	private float _jumpSpeed = 3f;

	public void Setup(HoleData holeData, MinionFactory factory)
	{
		_acceptColor = holeData.AcceptColor;
		_capacity = holeData.Capacity;
		_meshRenderer.material = factory.GetMaterial(_acceptColor);
		UpdateHoleRemain();
	}


	public bool CanAccept(Minion minion)
	{
		if(minion == null || _capacity <= 0) return false;
		return minion.MinionColor == _acceptColor;
	}

	public void Accept(Minion minion, Action onComplete = null)
	{
		if(!CanAccept(minion)) return;
		_capacity--;
		UpdateHoleRemain();
		minion.SetState(MinionState.JumpingToHole);
		Vector3 targetPosition = _targetPosition != null ? _targetPosition.position : this.transform.position;
		minion.MoveTo(targetPosition, _jumpSpeed, () =>
		{
			onComplete?.Invoke();
			minion.SetState(MinionState.Collected);
			minion.DeSpawn();
		});
	}

	private void UpdateHoleRemain()
	{
		_holeText.text = _capacity.ToString();
	}

}
