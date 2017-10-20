using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroysObjects : MonoBehaviour {

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Zombie")
        {
            Destroy(col.gameObject);
        }
    }
}
