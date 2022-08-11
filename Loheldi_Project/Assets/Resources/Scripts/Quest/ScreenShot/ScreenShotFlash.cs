using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ½ºÅ©¸°¼¦ ÂïÀ» ¶§ È­¸é ¹øÂ½ÀÌ±â </summary>
public class ScreenShotFlash : MonoBehaviour
{
    public float duration = 0.5f;

    private UnityEngine.UI.Image _image;
    private float _currentAlpha = 1f;

    private IEnumerator Flash()
    {
        _image = GetComponent<UnityEngine.UI.Image>();
        while (!(_currentAlpha < 0f))
        {
            Color col = _image.color;
            col.a = _currentAlpha;
            _image.color = col;

            _currentAlpha -= Time.unscaledDeltaTime / duration;
            yield return null;
        }
            gameObject.SetActive(false);
        yield break;

    }

    public void Show()
    {
        _currentAlpha = 1f;
        gameObject.SetActive(true);
        StartCoroutine(Flash());
    }
}
