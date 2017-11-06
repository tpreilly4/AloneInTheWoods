using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroysObjects : MonoBehaviour {

    //creates public drag and drop array in uity for objects to be destroyed by object using this script
    public GameObject toBeDestroyed;

    void OnCollisionEnter(Collision col)
    {
        //When colliders meet, scripted object destroys objects in array
        if (col.gameObject.name == "Zombie" || col.gameObject.name == ("Zombie(Clone)"))
        {
            Destroy(col.gameObject);
        }
    }
}
