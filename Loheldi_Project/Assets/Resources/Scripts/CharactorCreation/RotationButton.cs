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
        Button.head.GetComponent<MeshRenderer>().materials[0].color = new Color(255 / 255f, 237 / 255f, 227 / 255f); // ¾ó±¼ ÇÇºÎ
        Button.head.GetComponent<MeshRenderer>().materials[1].color = new Color(255 / 255f, 210 / 255f, 179 / 255f); // ¾ó±¼ ÇÇºÎ 2
        Button.head.GetComponent<MeshRenderer>().materials[4].color = new Color(42 / 255f, 138 / 255f, 52 / 255f);   // ´«
        Button.head.GetComponent<MeshRenderer>().materials[6].color = new Color(231 / 255f, 81 / 255f, 90 / 255f);   // ÀÔ
        Button.body.GetComponent<MeshRenderer>().materials[2].color = new Color(255 / 255f, 237 / 255f, 227 / 255f); // ¸ö ÇÇºÎ
        Button.hair.GetComponent<MeshRenderer>().materials[0].color = new Color(0 / 255f, 0 / 255f, 0 / 255f);       // ¸Ó¸®Ä«¶ô
    }
}