using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class runFX : MonoBehaviour
{
    private static runFX _instance;
    public static runFX instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<runFX>();
            }
            return _instance;
        }
    }

    [SerializeField]
    private ParticleSystem runningPs;

    public void RunningFX(GameObject go)    //card turning FX
    {
        ParticleSystem newfx = Instantiate(runningPs);
        newfx.transform.position = go.transform.position;
        newfx.transform.SetParent(go.transform);

        newfx.Play();

    }
}
