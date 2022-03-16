using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationButton : MonoBehaviour
{
    public GameObject player;

    public void Left()
    {
        player.transform.Rotate(new Vector3(0f, 0f, -30f));
    }
    public void Right()
    {
        player.transform.Rotate(new Vector3(0f, 0f, 30f));
    }
    public void Reset()
    {
        player.transform.rotation = Quaternion.Euler(-90f, 0f, 60f);
    }
}