using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sushi : MonoBehaviour, ICollectible
{
    private void OnEnable()
    {
        ManagerHub.Get<PlayersManager>().CollidableObject.Add(gameObject);
    }
    private void OnDisable()
    {
        ManagerHub.Get<PlayersManager>().CollidableObject.Remove(gameObject);
    }

    public void OnCollect()
    {
        //normalde collectible objeler burdan inherit ederek kendi verecegi etkileri özellestirerek uygulayabilir.
    }
}
