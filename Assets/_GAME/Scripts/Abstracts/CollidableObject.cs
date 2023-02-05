using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public abstract class CollidableObject : MonoBehaviour
{
    [SerializeField] GameObject TempAngleDetector;
    public Rigidbody playerRigidbody;
    [SerializeField] public short score;
    [SerializeField] public float playerScale { get { return this.transform.localScale.x; } }
    public int objectIndex { get { return gameObject.GetInstanceID(); } }
    [SerializeField] bool canMove;
    [SerializeField] float speed;
    private Vector3 currentmousePos;
    private Vector3 prevmousePos;
    private Vector3 deltamousePos;
    [SerializeField] float mult;
    public CollidableObject lastHitPlayer;
  
    public void MoveFoward()
    {
        this.playerRigidbody.velocity = transform.forward * (speed-playerScale/3);
    }
    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }
    private void LeftRightMovement()//Yonetilen karaktere ozgu sag sol mekanigi 
    {
        if (Input.GetMouseButton(0))
        {
            prevmousePos = currentmousePos;
            currentmousePos = Input.mousePosition;
            deltamousePos = currentmousePos - prevmousePos;
            transform.Rotate(Vector3.up, deltamousePos.x);
        }
        if (Input.GetMouseButtonUp(0))
        {
            this.playerRigidbody.angularVelocity = Vector3.zero;
        }
    }

    public void RaiseScore(short takenScore)
    {
        score += takenScore;
    }
    public bool CanMove()
    {
        return canMove;
    }
    public void CollidableObjectUpdate()
    {
        if (!canMove) return;
        MoveFoward();
        LeftRightMovement();
    }

    public IEnumerator OnCollision()//Carpisma sonrasi karakter carpma etkisini atlatana kadar yapilan kontrol
    {
        canMove = false;
        this.playerRigidbody.velocity = Vector3.zero;
        yield return new WaitForSeconds(0.3f);
        while (!canMove)
        {
            if (playerRigidbody.velocity.magnitude <= 0.1f)
            {
                canMove = true;
                break;
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }

        }
    }
    private void MoveDisableOnCollision()
    {
        StartCoroutine(OnCollision());

    }
    public void CollectedSushi(float sushiGrowAmount)
    {
        GrowUp(sushiGrowAmount);
    }
    void GrowUp(float growAmount)
    {
        float newScale = transform.localScale.x + growAmount;
        Vector3 newScaleVec = new Vector3(newScale, newScale, newScale);
        transform.DOScale(newScaleVec, 0.2f);
    }
    public void CalculateCollisionForce(Collision col, CollidableObject otherObject, float multiplier = 1)//Carpismada uygulanacak kuvveti acilara -angle- gore belirleyen metod
    {
        /*
         * sure sebebiyle acilari tam olarak temiz sekilde deneyemedim ancak bu sekilde yapilirsa karakter degisimi yada scale buyume durumlarý bu kodumuzu etkilemeyecek sekilde calisacaktir
         */
        MoveDisableOnCollision();//carpismada ileri yonlu hareket devre disi kaliyor
        float hitAngleBetweenLocalAngleSeconObject = FindAngle(col);
        float forceMultiplierByScale = ForceMultiplier(otherObject);//karakterlerin scale'ine bagli olarak uygulanacak guc carpanini veriyor
        Vector3 forceDir = CalculateDirection(col);//carpma sirasinda bilardo topu mantigiyla carpis yonune gore gidilecek tarafi hesapliyor
        if (hitAngleBetweenLocalAngleSeconObject < 30 || hitAngleBetweenLocalAngleSeconObject > 330)//on taraftan carpisma
        {
            playerRigidbody.AddForce(forceDir * 500 * forceMultiplierByScale);
        }
        if (hitAngleBetweenLocalAngleSeconObject > 150 && hitAngleBetweenLocalAngleSeconObject < 210)//arka taraftan carpisma
        {
            
            playerRigidbody.AddForce(forceDir * 600 * forceMultiplierByScale);
        }
        if ((hitAngleBetweenLocalAngleSeconObject > 30 && hitAngleBetweenLocalAngleSeconObject < 150) || (hitAngleBetweenLocalAngleSeconObject > 210 && hitAngleBetweenLocalAngleSeconObject < 330))//yan taraftan carpisma
        {
            playerRigidbody.AddForce(forceDir * 400 * forceMultiplierByScale);
        }

    }
    float ForceMultiplier(CollidableObject otherObject)
    {   
        float force;
        force = otherObject.playerScale / this.playerScale;
        return force;
    }
    Vector3 CalculateDirection(Collision collision)
    {
        Vector3 direction = transform.position - collision.contacts[0].point;
        direction = direction.normalized;
        return direction;
    }
    private float FindAngle(Collision collision)
    {
        TempAngleDetector.transform.position = transform.position;//sahnede var olan bir objeyi carpisma noktasina getirip carpilan noktanin acisini bulmak adina hesaplama yapiyor.
        Vector3 hitPoint = collision.contacts[0].point;
        TempAngleDetector.transform.LookAt(hitPoint, TempAngleDetector.transform.up);
        float hitAngleBetweenLocalAngle = transform.eulerAngles.y - TempAngleDetector.transform.eulerAngles.y;
        hitAngleBetweenLocalAngle = (hitAngleBetweenLocalAngle + 360f) % 360f;
        return hitAngleBetweenLocalAngle;
    }
   public void TransferScoreWhenDeath()//elenen oyuncunun ustundeki puanin ona son vuran kisiye aktarilmasi
    {
        lastHitPlayer.GrowUp(score / 10000);
        lastHitPlayer.RaiseScore(score);
    }
}
