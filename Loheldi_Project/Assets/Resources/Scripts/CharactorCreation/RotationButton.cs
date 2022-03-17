using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationButton : MonoBehaviour
{
    public GameObject player;

    public void Left()
    {
        player.transform.Rotate(new Vector3(0f, -30f, 0f));
    }
    public void Right()
    {
        player.transform.Rotate(new Vector3(0f, 30f, 0f));
    }
    public void Reset()
    {
        player.transform.rotation = Quaternion.Euler(0f, -120f, 0f);
        Button.SA();
        Button.EC();
        Button.MB();
        Button.HCA();
    }
}