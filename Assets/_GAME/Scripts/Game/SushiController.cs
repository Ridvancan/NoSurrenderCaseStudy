using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushiController : MonoBehaviour
{
    [SerializeField] List<GameObject> sushies;

    [System.Obsolete]
    private void Start()
    {
        StartCoroutine(SpawnSushies());
        StartCoroutine(SpawnSushiBurst(4));
    }

    [System.Obsolete]
    IEnumerator SpawnSushies()
    {
        while (ManagerHub.Get<GameManager>().currentState==GameState.inGame)
        {
            SpawnSushi();
            yield return new WaitForSeconds(1f);
        }
    }
    IEnumerator SpawnSushiBurst(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            SpawnSushi();
            yield return new WaitForSeconds(0.2f);
        }
    }

    [System.Obsolete]
    private void SpawnSushi()
    {
        GameObject tempObj = ObjectPool.Instance.GetObjectFromPool();
        tempObj.transform.position = new Vector3(Random.RandomRange(15, -15), 2, Random.RandomRange(15, -15));
    }
}
