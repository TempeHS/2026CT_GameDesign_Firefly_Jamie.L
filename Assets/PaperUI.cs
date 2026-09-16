using TMPro;
using UnityEngine;

public class PaperUI : MonoBehaviour
{
    public static PaperUI Instance;

    public GameObject panel;
    public TMP_Text textField;

    void Awake()
    {
        Instance = this;
    }

    public void Open(string text)
    {
        textField.text = text;
        panel.SetActive(true);
    }

    public void Close()
    {
        panel.SetActive(false);
    }
}
