using UnityEngine;

public class MinionFactory : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private Minion _minionPrefabs;

    [Header("Materials")]
    [SerializeField] private Material _redMaterial;
    [SerializeField] private Material _blueMaterial;
    [SerializeField] private Material _yellowMaterial;

    public Minion Create(MinionColor minionColor)
    {
        Minion minion = Instantiate(_minionPrefabs);
        minion.Setup(minionColor, GetMaterial(minionColor));
        return minion;
    }

    public Material GetMaterial(MinionColor minionColor)
    {
        switch(minionColor)
        {
            case MinionColor.Red:
                return _redMaterial;
			case MinionColor.Blue:
				return _blueMaterial;
		    default:
				return _yellowMaterial;
		}
    }
}
