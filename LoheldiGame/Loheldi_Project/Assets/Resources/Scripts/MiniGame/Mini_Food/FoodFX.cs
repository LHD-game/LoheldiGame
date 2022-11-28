using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodFX : MonoBehaviour
{
    private static FoodFX _instance;
    public static FoodFX instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<FoodFX>();
            }
            return _instance;
        }
    }

    [SerializeField]
    private ParticleSystem goodFoodPs;
    [SerializeField]
    private ParticleSystem badFoodPs;
    [SerializeField]
    private GameObject player;

    public void GoodFoodFX()    //���� ���� �Ծ��� �� fx
    {
        ParticleSystem newfx = Instantiate(goodFoodPs);
        newfx.transform.position = player.transform.position;
        newfx.transform.SetParent(player.transform);

        newfx.Play();
    }

    public void BadFoodFX()    //���� ���� �Ծ��� �� fx
    {
        ParticleSystem newfx = Instantiate(badFoodPs);
        newfx.transform.position = player.transform.position;
        newfx.transform.SetParent(player.transform);

        newfx.Play();
    }
}
