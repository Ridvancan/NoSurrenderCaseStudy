using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CollidableObject
{
    private void OnEnable()
    {
        EventManager.levelSuccessEvent.AddListener(EndGame);
        EventManager.levelFailEvent.AddListener(EndGame);
        EventManager.levelStartEvent.AddListener(StartGame);
    }
    private void OnDisable()
    {
        EventManager.levelSuccessEvent.RemoveListener(EndGame);
        EventManager.levelFailEvent.RemoveListener(EndGame);
        EventManager.levelStartEvent.RemoveListener(StartGame);
    }
    private void StartGame()
    {
        SetCanMove(true);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CalculateCollisionForce(collision, collision.gameObject.GetComponent<CollidableObject>());
            lastHitPlayer = collision.gameObject.GetComponent<CollidableObject>();
        }
        if (collision.gameObject.CompareTag("Sushi"))
        {
            ObjectPool.Instance.ReturnObjectToPool(collision.gameObject);
            CollectedSushi(.1f);
            IncreaseScore(1000);
        }
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            gameObject.SetActive(false);
            EventManager.levelFailEvent?.Invoke();
        }
    }
    void IncreaseScore(short collectedScore)
    {
        score += collectedScore;
        UIHub.Get<IngameUI>().IncreaseScore(score);
    }
    void EndGame()
    {
        SetCanMove(false);
        ManagerHub.Get<PlayersManager>().Players.Remove(gameObject);
    }
    void Update()
    {
        CollidableObjectUpdate();
    }
    public void ResetCharacter()
    {
        gameObject.SetActive(true);
        transform.position = new Vector3(6, 0, -4);
        transform.localScale = Vector3.one;
        SetCanMove(false);
        gameObject.SetActive(true);
    }
}
