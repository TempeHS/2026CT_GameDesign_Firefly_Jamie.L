using TMPro;
using UnityEngine;

public class PaperUI : MonoBehaviour
{
    public static PaperUI Instance;

    public GameObject panel;
    public TMP_Text textField;
    public GameObject Paper;
        public GameObject Closebutton;

    void Awake()
    {
        Instance = this;
    }

    public void Open(string text)
    {
        textField.text = text;
        panel.SetActive(true);
        Paper.SetActive(true);
        Closebutton.SetActive(true);
    }

    public void Close()
    {
        panel.SetActive(false);
        Instance = this;
        Debug.Log("PaperUI Awake, Instance set");
    }

}
