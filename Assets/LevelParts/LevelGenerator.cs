using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] 
    [Tooltip("How many level parts will be generated at any given time.")]
    private int genCount = 3;

    [SerializeField]
    [Tooltip("Parts from this list will be randomly selected for generation.")]
    private List<GameObject> possibleParts;

    private List<GameObject> levelParts = new List<GameObject>();

    private void Start()
    {
        for(int ii = 0; ii < genCount; ii++)
        {
            AddNewPart();
        }
    }

    private void RemoveOldestPart()
    {
        Destroy(levelParts[0]);
    }

    public void AddNewPart()
    {
        levelParts.Add(Instantiate(RandomPart()));
        levelParts[levelParts.Count-1].transform.parent = transform;

        levelParts[levelParts.Count - 1].transform.position = levelParts.Count == 1 ?
            Vector3.zero : levelParts[levelParts.Count - 2].GetComponent<PartScript>().connectPoint.position;

        if (levelParts.Count > genCount) RemoveOldestPart();
    }

    private GameObject RandomPart()
    {
        return possibleParts[Random.Range(0, possibleParts.Count)];
    }

    private void OnMouseUp()
    {
        AddNewPart();
    }
}
