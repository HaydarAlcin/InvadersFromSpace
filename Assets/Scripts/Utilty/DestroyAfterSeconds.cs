using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{

    public float seconds;

    void Start()
    {
        //Destroy(this.gameObject, seconds);
        if (transform.position.y > 7f)
        {
            gameObject.SetActive(false);
        }
    }
}
