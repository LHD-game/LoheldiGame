using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationButton : MonoBehaviour
{
    public GameObject player;

    public void Left()
    {
        player.transform.rotation = Quaternion.Euler(player.transform.rotation.x, player.transform.eulerAngles.y - 30f, player.transform.rotation.z);
    }
    public void Right()
    {
        player.transform.rotation = Quaternion.Euler(player.transform.rotation.x, player.transform.eulerAngles.y + 30f, player.transform.rotation.z);
    }
    public void Reset()
    {
        player.transform.rotation = Quaternion.Euler(0f, 148.454f, 0f);
    }
}
