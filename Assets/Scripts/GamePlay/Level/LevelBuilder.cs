using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] private LaneData[] _lanesData;
    [SerializeField] private HoleData[] _holesData;

	[SerializeField] private LaneController[] _laneControllers;
	[SerializeField] private Hole[] _holes;
	[SerializeField] private MinionFactory _factory;
	[SerializeField] private LevelController _levelController;

	private void Start()
	{
		CreateTestData();
		BuildLevel();
	}

	private void CreateTestData()
	{
		_lanesData = new LaneData[]
		{
			new LaneData()
			{
				MinionColors = new MinionColor[]
				{
					MinionColor.Blue,
					MinionColor.Blue,
					MinionColor.Blue,
					MinionColor.Blue,
					MinionColor.Blue,
					MinionColor.Blue,
					MinionColor.Blue,
					MinionColor.Red,
					MinionColor.Red,
					MinionColor.Red,
					MinionColor.Red,
					MinionColor.Blue,
					MinionColor.Blue,
					MinionColor.Blue,
					MinionColor.Blue,
					MinionColor.Red,
					MinionColor.Red,
					MinionColor.Red,
					MinionColor.Red,
					MinionColor.Red,
					MinionColor.Red,
					MinionColor.Red,
					MinionColor.Red,
					MinionColor.Red,
					MinionColor.Red,
					MinionColor.Blue,
					MinionColor.Blue,
					MinionColor.Blue,
					MinionColor.Blue,
					MinionColor.Blue,
					MinionColor.Blue,
					MinionColor.Blue,
				}
			}
		};
		_holesData = new HoleData[]
		{
			new HoleData()
			{
				AcceptColor = MinionColor.Blue,
				Capacity = 18
			},
			new HoleData()
			{
				AcceptColor = MinionColor.Red,
				Capacity = 14
			},
		};
	}

	private void BuildLevel()
	{
		BuildLane();
		BuildHole();
	}

	private void BuildLane()
	{
		int laneCount = Mathf.Min(_laneControllers.Length, _lanesData.Length);
		int peopleCount = 0;
		for (int i = 0; i < laneCount; i++)
		{
			SetupLane(_laneControllers[i], _lanesData[i]);
			peopleCount += _lanesData[i].MinionColors.Count();
		}

		_levelController.Setup(peopleCount);
	}

	private void BuildHole()
	{
		int holeCount = Mathf.Min(_holes.Length, _holesData.Length);
		for(int i = 0;i < holeCount;i++)
		{
			SetupHole(_holes[i], _holesData[i]);
		}
	}

	private void SetupLane(LaneController lane, LaneData laneData)
	{
		lane.Setup(laneData.MinionColors, _factory);
	}

	private void SetupHole(Hole hole, HoleData holeData)
	{
		hole.Setup(holeData, _factory);
	}
}
