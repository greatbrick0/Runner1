using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField, Tooltip("How many level parts will be generated at any given time.")]
    private int genCount = 3;

    [SerializeField, Tooltip("Parts from this list will be randomly selected for generation.")]
    private List<GameObject> possibleParts;
    private GameObject previousChoice;

    private List<GameObject> levelParts = new();
    private Vector3 furthestPoint;
    [SerializeField, Tooltip("How close the player must be to the end of the level to generate a new part.")]
    private float genDistance = 20.0f;

    private Transform trans;
    [SerializeField]
    private Transform playerTrans;

    private void Start()
    {
        previousChoice = possibleParts[0];
        for(int i = 0; i < genCount; ++i)
        {
            AddNewPart();
        }
    }

    private void Update()
    {
        if (furthestPoint.z - playerTrans.position.z < genDistance) AddNewPart();
    }

    private void RemoveOldestPart()
    {
        Destroy(levelParts[0]);
        levelParts.RemoveAt(0);
    }

    public void AddNewPart()
    {
        levelParts.Add(Instantiate(RandomPart()));
        levelParts[^1].transform.parent = transform;

        levelParts[^1].transform.position = levelParts.Count == 1 ?
            Vector3.zero : levelParts[^2].GetComponent<PartScript>().connectPoint.position;

        furthestPoint = levelParts[^1].GetComponent<PartScript>().connectPoint.position;
        if (levelParts.Count > genCount) RemoveOldestPart();
    }

    private GameObject RandomPart()
    {
        List<GameObject> newSet = new List<GameObject>(possibleParts);
        newSet.Remove(previousChoice);
        int randomInt = Random.Range(0, newSet.Count);
        previousChoice = newSet[randomInt];

        return newSet[randomInt];
    }

    public void MoveAllParts(Vector3 offset)
    {
        for(int ii = 0; ii < trans.childCount; ii++)
        {
            trans.GetChild(ii).position += offset;
        }
    }
}
