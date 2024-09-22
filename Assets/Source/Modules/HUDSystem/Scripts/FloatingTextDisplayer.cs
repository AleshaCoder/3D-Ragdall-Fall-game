using TMPro;
using UnityEngine;
using System.Collections;

namespace Assets.Source.HUDSystem.Scripts
{
    public class FloatingTextDisplayer : MonoBehaviour, IFloatingTextDisplayer
    {
        public TMP_Text floatingText;
        public float floatDuration = 1.5f; // Время показа текста
        public float fadeDuration = 0.5f;  // Время плавного исчезания
        public float minRiseSpeed = 1.0f;     // Скорость подъема текста
        public float maxRiseSpeed = 1.0f;     // Скорость подъема текста
        public bool Busy { get; private set; } = false;  // Свойство занятости

        private Coroutine currentCoroutine;

        public void DisplayText(string text)
        {
            floatingText.gameObject.SetActive(true);
            floatingText.text = text;

            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }

            currentCoroutine = StartCoroutine(FadeInAndFloat());
        }

        private IEnumerator FadeInAndFloat()
        {
            Busy = true;
            float elapsedTime = 0f;
            Color originalColor = floatingText.color;
            originalColor.a = 0f;
            floatingText.color = originalColor;

            Vector3 originalPosition = floatingText.transform.position;
            float speed = Random.Range(minRiseSpeed, maxRiseSpeed);
            // Плавное появление текста
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
                originalColor.a = alpha;
                floatingText.color = originalColor;

                floatingText.transform.position = originalPosition + Vector3.up * speed * elapsedTime;
                yield return null;
            }

            floatingText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

            // Держим текст на экране в течение floatDuration
            yield return new WaitForSeconds(floatDuration);

            // Плавное исчезновение текста
            elapsedTime = 0f;
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Clamp01(1f - (elapsedTime / fadeDuration));
                originalColor.a = alpha;
                floatingText.color = originalColor;

                floatingText.transform.position = originalPosition + Vector3.up * speed * (fadeDuration + elapsedTime);
                yield return null;
            }

            floatingText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
            floatingText.gameObject.SetActive(false);
            floatingText.transform.position = originalPosition;
            Busy = false;
        }
    }
}
