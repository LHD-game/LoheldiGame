using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpButton : MonoBehaviour
{
    public static bool OnLand = false;
    public GameObject Player;
    public Rigidbody Playerrb;

    public GameObject ShopMok; // ¸ñ°ø¹æ

    public void OnClick()
    {
        if (Player.GetComponent<Interaction>().NearNPC)
        {
            ShopMok.SetActive(true);
        }
        else
        {
            if (OnLand)
            {
                Playerrb.AddForce(transform.up * 10000);
                OnLand = false;
            }
        }
    }
}
