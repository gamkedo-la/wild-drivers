using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    public void ChangeScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
    public void Options()
    {
        Debug.Log("Options button clicked");
    }
    public void Credits()
    {
        Debug.Log("Credits button clicked");
    }
}
