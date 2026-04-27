using System.Collections.Generic;
using UnityEngine;

public class MinionSkinMesh : MonoBehaviour
{
    [SerializeField] private List<SkinnedMeshRenderer> skinnedMeshes;

    public void SetMaterial(Material material)
    {
        foreach(var skin in skinnedMeshes)
        {
            skin.material = material;
        }
    }
}
