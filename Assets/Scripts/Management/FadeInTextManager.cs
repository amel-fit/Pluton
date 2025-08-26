using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Management
{
    public class FadeInTextManager : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text endText; 
        [SerializeField]
        private TMP_Text amelText;
        [SerializeField]
        private TMP_Text vedoText;


        private float _fadeTime = 5.0f;
        private float _startAlpha = 0;
        // Start is called once before the first execution of Update after the MonoBehaviour is created

        private float _fadePerSecond;
        void Start()
        {
            _fadePerSecond = 1 / _fadeTime;
            StartCoroutine(FadeInEndText());
        }


        private IEnumerator Fade(TMP_Text textToFadeIn)
        {
            float fadeTimeLeft = _fadeTime;
            float currentAlpha = _startAlpha;
            while (fadeTimeLeft > 0)
            {
                fadeTimeLeft -= Time.deltaTime;
                currentAlpha += _fadePerSecond * Time.deltaTime;

                //Debug.Log(currentAlpha);
                textToFadeIn.color = new Color(textToFadeIn.color.r, textToFadeIn.color.g, textToFadeIn.color.b,
                    currentAlpha);
                yield return null;
            }

            
            yield return null;
        }

        
        private IEnumerator FadeInEndText()
        {
            yield return new WaitForSeconds(3);
            StartCoroutine(Fade(endText));
            yield return new WaitUntil(() => endText.color.a > 0.9f);
            yield return new WaitForSeconds(1.0f);
            StartCoroutine(FadeInCredits());
            yield return null;
        }

        private IEnumerator FadeInCredits()
        {
            StartCoroutine(Fade(amelText));
            StartCoroutine(Fade(vedoText));
            yield return null;
        }

        
    }
}
