using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public Transform slotsContainer;
    public GameObject slotPrefab;
    public int slotCount;
    public GameObject[] itemPrefab;

    void Start()
    {
        if (slotsContainer == null || slotPrefab == null)
        {
            Debug.LogError("Missing slotsContainer or slotPrefab.");
            return;
        }

        for (int i = 0; i < slotCount; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, slotsContainer, false);
            Slot slot = slotObj.GetComponent<Slot>();
            if (slot == null) continue;

            if (itemPrefab != null && i < itemPrefab.Length && itemPrefab[i] != null)
            {
                GameObject item = Instantiate(itemPrefab[i], slot.transform, false);
                slot.currentItem = item;
            }
        }
    }
}