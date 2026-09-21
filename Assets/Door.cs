using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, IInteractable
{
    public bool locked = false;
    public InteractionMessage interactionMessage;

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        if (locked)
        {
            interactionMessage.ShowMessage("This door is locked...");
            return;
        }

        if (CompareTag("main-bed"))
        {
            PlayerController.targetSpawn = "BedroomDoorSpawn";
            SceneManager.LoadScene("Main Bedroom");
        }
        else if (CompareTag("main-bath"))
        {
            PlayerController.targetSpawn = "BathroomDoorSpawn";
            SceneManager.LoadScene("Bathroom");
        }
        else if (CompareTag("bed-main"))
        {
            PlayerController.targetSpawn = "bedtomainspawn";
            SceneManager.LoadScene("Outside Room");
        }
        else if (CompareTag("bath-main"))
        {
            PlayerController.targetSpawn = "bathtomainspawn";
            SceneManager.LoadScene("Outside Room");
        }
        else if (CompareTag("main-kitchen"))
        {
            PlayerController.targetSpawn = "KitchenDoorSpawn";
            SceneManager.LoadScene("Kitchen");
        }
        else if (CompareTag("kitchen-main"))
        {
            PlayerController.targetSpawn = "kitchentomainspawn";
            SceneManager.LoadScene("Outside Room");
        }
        else if (CompareTag("main-study"))
        {
            PlayerController.targetSpawn = "StudySpawn";
            SceneManager.LoadScene("Study");
        }
        else if (CompareTag("study-main"))
        {
            PlayerController.targetSpawn = "studytomainspawn";
            SceneManager.LoadScene("Outside Room");
        }
        else if (CompareTag("tut-bed"))
        {
            PlayerController.targetSpawn = "Tutspawn";
            SceneManager.LoadScene("Main Bedroom");
        }
        else if (CompareTag("main-out"))
        {
            PlayerController.targetSpawn = "Outsidespawn";
            SceneManager.LoadScene("Outside");
        }
        else if (CompareTag("out-main"))
        {
            PlayerController.targetSpawn = "outtomainspawn";
            SceneManager.LoadScene("Outside Room");
        }
    }
}