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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TrunCardFX(GameObject go)    //카드 뒤집힐 때 fx
    {
        ParticleSystem newfx = Instantiate(turnCardPs);
        newfx.transform.position = go.transform.position;
        newfx.transform.SetParent(go.transform);
        
        newfx.Play();

    }

    public void DisCardFX(Vector3 v)
    {
        ParticleSystem newfx = Instantiate(disCardPs);
        newfx.transform.position = v;
        //newfx.transform.SetParent(go.transform);

        newfx.Play();
    }
}
