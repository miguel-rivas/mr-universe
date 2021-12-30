using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour {
    public void LoadSceneWithIndex(int sceneIndex) {
        Application.LoadLevel(sceneIndex);
    }

    public void LoadScene(string sceneName) {
        Application.LoadLevel(sceneName);
    }
}
