using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : CollidableObject
{
    [SerializeField] Transform target;
    [SerializeField] float counter;
    private void Start()
    {
        counter = 4;
        ManagerHub.Get<PlayersManager>().CollidableObject.Add(gameObject);
        ManagerHub.Get<PlayersManager>().Players.Add(gameObject);
        StartCoroutine(FindFirstTarget());
    }
    IEnumerator FindFirstTarget()
    {
        yield return new WaitForSeconds(0.5f);
        GetNewTarget();
    }
    void Update()
    {
        if (!CanMove()) return;
        if (target is null ) { GetNewTarget(); }
        if (!target.gameObject.activeInHierarchy) { BugProtect(target.gameObject); GetNewTarget(); }

        ChangeTargetByTime();//AI yetisemedigi hedefe bagli kalmamasi icin belli sureyle hedefini degistiriyor.
        MoveFoward();
        LookAtTarget(target);
    }
    private void BugProtect(GameObject TargetObject)
    {
        ManagerHub.Get<PlayersManager>().CollidableObject.Remove(target.gameObject);
    }
    private void ChangeTargetByTime()
    {
        if (counter >= 0)
        {
            counter -= Time.deltaTime;
            if (counter <= 0)
            {
                GetNewTarget();
                counter = 4;
            }
        }
    }

    void LookAtTarget(Transform target)//AI smooth olarak gittiði yone bakiyor
    {
        Quaternion lookRotation = Quaternion.LookRotation(target.position - transform.position);
        lookRotation.x = 0;
        lookRotation.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime*75);
    }
    void GetNewTarget()
    {
        if (lastHitPlayer!=null)
        {
            if (lastHitPlayer.playerScale > playerScale)
            {
             target=ManagerHub.Get<PlayersManager>().GetRandomTransform();//eger carpistigi oyuncu kendisinden buyukse kacmak uzere farklý hedefe yoneliyor
            }
        }
        else
        {
            target = ManagerHub.Get<PlayersManager>().NearestEnemyTransform(transform);//AI kendisine en yakin objeyi hedef aliyor
        }
        
    }
    private void OnDisable()
    {
        ManagerHub.Get<PlayersManager>().CollidableObject.Remove(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CalculateCollisionForce(collision, collision.gameObject.GetComponent<CollidableObject>());
            lastHitPlayer = collision.gameObject.GetComponent<CollidableObject>();
            GetNewTarget();
        }
        if (collision.gameObject.CompareTag("Sushi"))
        {
            ObjectPool.Instance.ReturnObjectToPool(collision.gameObject);
            CollectedSushi(.1f);
            GetNewTarget();
        }
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            gameObject.SetActive(false);
            ManagerHub.Get<PlayersManager>().Players.Remove(gameObject);
            ManagerHub.Get<GameManager>().CheckEndGame();
        }

    }
}
