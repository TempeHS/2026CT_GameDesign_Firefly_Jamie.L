using UnityEngine;
using UnityEngine.SceneManagement;

public class openmenu : MonoBehaviour
{
    public void OnStartClick()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void OnExitClick()
    {
        SceneManager.LoadScene("too bad");
    }
}

