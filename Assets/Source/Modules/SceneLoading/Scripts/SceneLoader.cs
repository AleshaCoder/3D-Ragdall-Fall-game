using ConstantValues;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneLoading
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private Fader _fader;

        //public SceneLoader(Fader fader) =>
        //    _fader = fader;

        public void LoadMainMenu() =>
            _fader.FadeIn(() => SceneManager.LoadScene(SceneNames.Menu));

        public void LoadGameScene() => _fader.FadeIn(() => SceneManager.LoadScene(SceneNames.GameScene));
        public void LoadPlaneScene() => _fader.FadeIn(() => SceneManager.LoadScene(SceneNames.PlaneScene));
        public void LoadStairsScene() => _fader.FadeIn(() => SceneManager.LoadScene(SceneNames.StairsScene));
        public void LoadCityScene() => _fader.FadeIn(() => SceneManager.LoadScene(SceneNames.CityScene));
    }
}