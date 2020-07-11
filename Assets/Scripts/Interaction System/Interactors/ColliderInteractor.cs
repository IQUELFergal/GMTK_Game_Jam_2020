﻿using UnityEngine;

public class ColliderInteractor : MonoBehaviour
{
    private IInteractable currentInteractable = null;

    void Update()
    {
        CheckForInteraction();
    }

    private void CheckForInteraction()
    {
        if (currentInteractable == null) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            currentInteractable.Interact();
            if (interactionUI != null)
            {
                interactionUI.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var interactable = other.GetComponent<IInteractable>();
        if (interactable == null) return;
        currentInteractable = interactable;
    }
       
    private void OnTriggerExit2D(Collider2D other)
    {
        var interactable = other.GetComponent<IInteractable>();
        if (interactable == null) return;
        if (interactable != currentInteractable) return;
        
        currentInteractable = null;
    }
}