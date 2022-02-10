using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFX : MonoBehaviour
{
    private static CardFX _instance;
    public static CardFX instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CardFX>();
            }
            return _instance;
        }
    }

    [SerializeField]
    private ParticleSystem turnCardPs;
    [SerializeField]
    private ParticleSystem disCardPs;


    void Start()
    {

    }

    public void TrunCardFX(GameObject go)    //card turning FX
    {
        ParticleSystem newfx = Instantiate(turnCardPs);
        newfx.transform.position = go.transform.position;
        newfx.transform.SetParent(go.transform);

        newfx.Play();

    }

    public void DisCardFX(Vector3 v)    //card destroy FX
    {
        ParticleSystem newfx = Instantiate(disCardPs);
        newfx.transform.position = v;
        //newfx.transform.SetParent(go.transform);

        newfx.Play();
    }
}