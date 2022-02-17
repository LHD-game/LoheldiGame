using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCtrl : MonoBehaviour
{
    bool isOpen = false;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (CardGameManager.state == CardGameManager.STATE.IDLE)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                CheckCard();
            }
        }
    }

    void CheckCard()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            string tag = hit.transform.tag;
            if (tag.Substring(0, 4) == "Card")
            {
                hit.transform.SendMessage("OpenCard", SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    void OpenCard()
    {
        if (isOpen) return;
        isOpen = true;
        anim.Play("CardFlipAnimation");
        CardFX.instance.TrunCardFX(this.gameObject);    //fx
        CardGameManager.state = CardGameManager.STATE.HIT;
        CardGameManager.OpenCard = this.gameObject;

    }
    IEnumerator CloseCard()
    {
        yield return new WaitForSeconds(0.5f);
        anim.Play("CardFlipBackAnimation");
        isOpen = false;
        if (this.gameObject == CardGameManager.LastCard)
        {
            CardGameManager.LastCard = null;
        }
        if (this.gameObject == CardGameManager.OpenCard)
        {
            CardGameManager.OpenCard = null;
        }
    }
    IEnumerator DestroyCard()
    {
        yield return new WaitForSeconds(1);
        if (this.gameObject != null)
        {
            CardFX.instance.DisCardFX(this.transform.position); //fx
            Destroy(this.gameObject);
        }

    }
}