using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ftrash : MonoBehaviour
{
    public static bool complete;
    private void OnTriggerEnter(Collider other)
    {

        if (other.name == "回收區")
        {
            complete = true;
        }
    }
}
