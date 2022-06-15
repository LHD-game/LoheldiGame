using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    void destroy(string DestryedObject)
    {
        Destroy(GameObject.Find(DestryedObject));
    }
}
