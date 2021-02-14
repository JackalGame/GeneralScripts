using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] slopePrefabs;
    [Tooltip("where do we want the block to spawn")][SerializeField] Vector2 lastPos;
    [SerializeField] Transform parent;
    [SerializeField] int numOfInitialBlocks = 4;



    // Start is called before the first frame update
    void Start()
    {
        BuildInitialSlope();
    }

    private void BuildInitialSlope()
    {
        for(int i = 0; i < numOfInitialBlocks; i++)
        {
            SpawnSlopePart();
        }
    }

    public void SpawnSlopePart() // method also called when last slope part is destroyed.
    {
        Vector2 spawnPos = new Vector2(lastPos.x, lastPos.y);
        var slopeToSpawn = slopePrefabs[Random.Range(0, slopePrefabs.Length)];
        GameObject slope = Instantiate(slopeToSpawn, spawnPos, transform.rotation);
        slope.transform.parent = parent; // Keeps object under the relevant tab within the Hierarchy.
        Transform endPoint = slope.transform.GetChild(1);
        lastPos = endPoint.transform.position;
    }

}
