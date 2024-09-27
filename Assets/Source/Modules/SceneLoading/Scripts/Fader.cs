using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SceneLoading
{
    [RequireComponent(typeof(Image))]
    public class Fader : MonoBehaviour
    {
        [SerializeField] private float _fadeInTime;
        [SerializeField] private float _fadeOutTime;

        private Image _image;
        private Coroutine _currentCoroutine;
        private Color _tempColor;

        private void Awake()
        {
            _image = GetComponent<Image>();
            DontDestroyOnLoad(this);
        }

        private void OnEnable() =>
            SceneManager.sceneLoaded += FadeOut;

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= FadeOut;
            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);
        }

        public void FadeIn(UnityAction isDarken) =>
            _currentCoroutine = StartCoroutine(Darken(isDarken));

        private void FadeOut(Scene scene, LoadSceneMode loadSceneMode) =>
            _currentCoroutine = StartCoroutine(Lighten());

        private IEnumerator Darken(UnityAction actionAfterDarken)
        {
            _image.gameObject.SetActive(true);

            while (_image.color.a < 1f)
            {
                _tempColor = _image.color;
                _tempColor.a += Time.deltaTime / _fadeInTime;
                _image.color = _tempColor;

                yield return null;
            }

            actionAfterDarken?.Invoke();
            StopCoroutine(_currentCoroutine);
        }

        private IEnumerator Lighten()
        {
            while (_image.color.a > 0.1f)
            {
                _tempColor = _image.color;
                _tempColor.a -= Time.deltaTime / _fadeOutTime;
                _image.color = _tempColor;

                yield return null;
            }

            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);
            //_image.gameObject.SetActive(false);
        }
    }
}