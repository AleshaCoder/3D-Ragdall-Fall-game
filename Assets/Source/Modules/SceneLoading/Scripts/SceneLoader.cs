using ConstantValues;
using UnityEngine.SceneManagement;

namespace SceneLoading
{
    public class SceneLoader
    {
        private Fader _fader;

        public SceneLoader(Fader fader) =>
            _fader = fader;

        public void LoadMainMenu() =>
            _fader.FadeIn(() => SceneManager.LoadScene(SceneNames.MainMenu));

        public void LoadGameScene() =>
            _fader.FadeIn(() => SceneManager.LoadScene(SceneNames.GameScene));
    }
}