using UnityEngine;
using UnityEngine.SceneManagement;

public static class CustomSceneManager
{
    static bool _sceneLoadingInProgress = false;
    public static void LoadSceneWithFade(string sceneName)
    {
        if(_sceneLoadingInProgress) return;
        else
        {
            _sceneLoadingInProgress = true;
            // フェード処理
            FadeManager.Instance.FadeIn(() => 
            {
                var async = SceneManager.LoadSceneAsync(sceneName);
                async.completed += _ =>
                {
                    GameManager.Instance.GameState = GameState.InGame;
                    FadeManager.Instance.FadeOut(()=> _sceneLoadingInProgress = false);
                };
            });
        }
    }
}