using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public void CloseThis()
    {
        Destroy(this);
    }
}
