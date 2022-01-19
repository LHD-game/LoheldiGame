using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpButton : MonoBehaviour
{
    public static bool OnLand = false;
    public Rigidbody Player;

    public void OnClick()
    {
        if (OnLand)
        {
            Player.AddForce(transform.up * 10000);
            MainGameManager.exp = MainGameManager.exp + 10;
            OnLand = false;
        }
    }
}
