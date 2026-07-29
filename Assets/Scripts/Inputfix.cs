using UnityEngine;
using UnityEngine.InputSystem;

public class DebugInput : MonoBehaviour
{
    public void OnInteract(InputAction.CallbackContext ctx)
    {
        Debug.Log("UNITY EVENT FIRED");
    }
}
