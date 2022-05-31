using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager2 : MonoBehaviour
{
    public GameObject SoundManager;

    void Start()
    {
        SoundManager.GetComponent<SoundEffect>().Sound("BGMField");
    }
}
