using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warningFX : MonoBehaviour
{
    private static warningFX _instance;
    public static warningFX instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<warningFX>();
            }
            return _instance;
        }
    }

    [SerializeField]
    private ParticleSystem warningPs;

    public void WunningFX(GameObject go)    //warningPs star FX
    {
        ParticleSystem newfx = Instantiate(warningPs);
        newfx.transform.position = go.transform.position;
        newfx.transform.SetParent(go.transform);

        newfx.Play();

    }

}
