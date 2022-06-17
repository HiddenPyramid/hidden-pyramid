using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FromCintematicToMain : MonoBehaviour
{
    public SceneLoadTrigger sceneLoadTrigger;
    public void LoadNextScene()
    {
        sceneLoadTrigger.LoadNextScene();
    }
}
