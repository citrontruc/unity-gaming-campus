using System.Linq;
using UnityEngine;

public class GroupCollectible : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] instances = meshFilters
            .Select(x => new CombineInstance
            {
                mesh = x.sharedMesh,
                transform = x.transform.localToWorldMatrix,
            })
            .ToArray();

        foreach (var meshFilter in meshFilters)
        {
            meshFilter.gameObject.SetActive(false);
        }

        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(instances);
        var gameObjectMeshFilter = gameObject.AddComponent<MeshFilter>();
        gameObjectMeshFilter.sharedMesh = combinedMesh;
        gameObject.SetActive(true);
    }
}
