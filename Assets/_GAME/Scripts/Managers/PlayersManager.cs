using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersManager : BaseManager
{
    public List<GameObject> CollidableObject;
    public List<GameObject> Players;
    private void OnEnable()
    {
        EventManager.levelStartEvent.AddListener(PlayersBegin);
        EventManager.levelFailEvent.AddListener(FreezeCharacters);
        EventManager.levelFailEvent.AddListener(ClearList);
        EventManager.levelSuccessEvent.AddListener(ClearList);
    }
    private void OnDisable()
    {
        EventManager.levelStartEvent.RemoveListener(PlayersBegin);
        EventManager.levelFailEvent.RemoveListener(ClearList);
        EventManager.levelFailEvent.RemoveListener(FreezeCharacters);
        EventManager.levelSuccessEvent.RemoveListener(ClearList);
    }

    [Obsolete]
    public Transform GetRandomTransform()
    {
        return CollidableObject[UnityEngine.Random.RandomRange(0, CollidableObject.Count)].transform;
    }
    public Transform NearestEnemyTransform(Transform selfTransform)
    {
        float distance = 100;
        Transform tempTransform = null;
        for (int i = 0; i < CollidableObject.Count; i++)
        {
            if (selfTransform != CollidableObject[i].transform)
            {
                if (distance > Vector3.Distance(selfTransform.position, CollidableObject[i].transform.position))
                {
                    tempTransform = CollidableObject[i].transform;
                    distance = Vector3.Distance(selfTransform.position, tempTransform.position);
                    
                }
            }
        }
        return tempTransform;
    }
    public void FreezeCharacters()
    {
        foreach (var item in Players)
        {
                if (item.GetComponent<CollidableObject>())
                {
                    item.GetComponent<CollidableObject>().SetCanMove(false);
                }
        }
    }
    public void ClearList()
    {
        Players.Clear();
    }
    public void PlayersBegin()
    {
        foreach (var item in Players)
        {
            if (item.GetComponent<CollidableObject>())
            {
                item.GetComponent<CollidableObject>().SetCanMove(true);
            }
        }
    }
}
