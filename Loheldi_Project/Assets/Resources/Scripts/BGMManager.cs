using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public GameObject SoundManager;

    private void Start()
    {
        SoundManager.GetComponent<SoundEffect>().Sound("BGMLobby");
    }
}
