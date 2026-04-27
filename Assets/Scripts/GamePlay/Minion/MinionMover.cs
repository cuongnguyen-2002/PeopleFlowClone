using System;
using UnityEngine;

public class MinionMover : MonoBehaviour
{
    private float _speed;
    private bool _isMoving;
    private Vector3 _target;
    private Action _onComplete;

    public void MoveTo(Vector3 target, float speed, Action onComplete = null)
    {
        _speed = speed;
        _target = target;
        _onComplete = onComplete;
        _isMoving = true;
    }

	private void Update()
	{
        if (!_isMoving) return;

        this.transform.localPosition = Vector3.MoveTowards(
            this.transform.localPosition, 
            _target, 
            _speed * Time.deltaTime);

        if((this.transform.localPosition - _target).sqrMagnitude <= 0.01f)
        {
            _isMoving = false;
			Action complete = _onComplete;
			_onComplete = null;
			complete?.Invoke();
		}
	}

    public void StopMove()
    {
        _isMoving = false;
    }
}
