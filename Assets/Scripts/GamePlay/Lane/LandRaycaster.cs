using UnityEngine;

public class LandRaycaster : MonoBehaviour
{
    [SerializeField] private MobileInputReader _inputReader;
	[SerializeField] private LevelController _levelController;
	[SerializeField] private LayerMask _layerMask;
	private Camera _camera;
	[SerializeField] private float _maxDistance;


	private void Start()
	{
		_camera = Camera.main;
	}

	private void OnEnable()
	{
		_inputReader.OnTab += OnTab;
	}

	private void OnTab(Vector2 screenPoint)
	{
		LaneRaycast(screenPoint);
	}

	private void LaneRaycast(Vector2 sceenPoint)
	{
		Ray ray = _camera.ScreenPointToRay(sceenPoint);

		if(Physics.Raycast(ray, out RaycastHit hitInfo, _maxDistance))
		{
			LaneController laneController = hitInfo.collider.GetComponent<LaneController>();
			if(laneController != null)
			{
				_levelController.TrySendMinionFromLane(laneController);
			}
		}
	}
}
