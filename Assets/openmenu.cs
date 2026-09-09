using UnityEngine;
using UnityEngine.SceneManagement;

public class openmenu : MonoBehaviour
{
    public void OnStartClick()
    {
        SceneManager.LoadScene("Main Bedroom");
    }
    public void OnExitClick()
    {
        SceneManager.LoadScene("too bad");
    }
}

