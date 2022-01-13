using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    public void LoadSceneWithIndex(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadScene(string sceneName) {
       SceneManager.LoadScene(sceneName);
    }
}
