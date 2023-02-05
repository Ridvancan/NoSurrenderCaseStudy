//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CollisionManager : BaseManager
//{
//    [SerializeField] GameObject TempAngleDetector;
//    [SerializeField] GameObject TempAngleDetector2;
//    private void Start()
//    {

//    }
//    public void ManageCollision(Collision collision, CollidableObject firstObject, CollidableObject secondObject)
//    {
//        if (firstObject.objectIndex < secondObject.objectIndex) return;

//        MoveDisableOnCollision(firstObject, secondObject);

//        float hitAngleBetweenLocalAngleSeconObject = FindAngle(collision, firstObject);
//        //float hitAngleBetweenLocalAngleFirstObject = FindAngle(collision, secondObject);

//        Vector3 forceTransformFirstObject = CalculateDirection(collision, firstObject);
//        Vector3 forceTransformSecondObject = CalculateDirection(collision, secondObject);


//        //firstObject.AddCalculatedForce(forceTransformFirstObject);

//        if ((hitAngleBetweenLocalAngleSeconObject < 30 || hitAngleBetweenLocalAngleSeconObject > 330))
//        {
//            //secondObject.playerRigidbody.AddForce(forceTransformFirstObject * 200);
//            //firstObject.playerRigidbody.AddForce(forceTransformSecondObject * 300);
//            Debug.Log("önden çarptý " + hitAngleBetweenLocalAngleSeconObject);
//        }
//        if ((hitAngleBetweenLocalAngleSeconObject > 150 && hitAngleBetweenLocalAngleSeconObject < 210))
//        {
//            //secondObject.playerRigidbody.AddForce(forceTransformFirstObject * 500);
//            //firstObject.playerRigidbody.AddForce(forceTransformSecondObject * 300);
//            Debug.Log("arka çarptý " + hitAngleBetweenLocalAngleSeconObject);
//        }
//        if ((hitAngleBetweenLocalAngleSeconObject > 30 && hitAngleBetweenLocalAngleSeconObject < 150) || (hitAngleBetweenLocalAngleSeconObject > 210 && hitAngleBetweenLocalAngleSeconObject < 330))
//        {
//            //secondObject.playerRigidbody.AddForce(forceTransformFirstObject * 500);
//            //firstObject.playerRigidbody.AddForce(forceTransformSecondObject * 500);
//            Debug.Log("yanlar çarptý " + hitAngleBetweenLocalAngleSeconObject);
//        }
//    }

//    private float FindAngle(Collision collision, CollidableObject collidableObject)
//    {
//        TempAngleDetector.transform.position = collidableObject.transform.position;
//        Vector3 hitPoint = collision.contacts[0].point;
//        TempAngleDetector.transform.LookAt(hitPoint, TempAngleDetector.transform.up);
//        float hitAngleBetweenLocalAngle = collidableObject.transform.eulerAngles.y - TempAngleDetector.transform.eulerAngles.y;
//        hitAngleBetweenLocalAngle = (hitAngleBetweenLocalAngle + 360f) % 360f;
//        return hitAngleBetweenLocalAngle;
//    }
//    Vector3 CalculateDirection(Collision collision, CollidableObject collidableObject)
//    {
//        Vector3 direction = collidableObject.transform.position - collision.contacts[0].point;
//        direction = direction.normalized;
//        return -direction;
//    }
//    private void MoveDisableOnCollision(CollidableObject firstObject, CollidableObject secondObject)
//    {
//        StartCoroutine(firstObject.OnCollision());
//        StartCoroutine(secondObject.OnCollision());
//    }



//}
