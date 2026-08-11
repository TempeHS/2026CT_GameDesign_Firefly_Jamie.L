using UnityEngine;
using UnityEngine.UI;

public class tabs : MonoBehaviour
{
    [SerializeField] private Image[] tabImages;
    [SerializeField] private Button[] tabButtons;
    [SerializeField] private GameObject[] pages;

    void Awake()
    {
        for (int i = 0; i < tabButtons.Length; i++)
        {
            int index = i; // capture loop variable safely
            tabButtons[i].onClick.AddListener(() => ActivateTab(index));
        }
    }

    void Start()
    {
        ActivateTab(0);
    }

    public void ActivateTab(int tabNo)
    {
        int count = Mathf.Min(pages.Length, tabImages.Length);
        if (tabNo < 0 || tabNo >= count) return;

        for (int i = 0; i < count; i++)
        {
            pages[i].SetActive(false);
            tabImages[i].color = Color.grey;
        }

        pages[tabNo].SetActive(true);
        tabImages[tabNo].color = Color.white;
    }
}