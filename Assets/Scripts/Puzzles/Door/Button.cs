using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a button that interacts with a door.
/// Activates when an object with Rigidbody2D enters its trigger.
/// </summary>
public class Button : MonoBehaviour
{
    [Header("Keep the door open after activation?")]
    [SerializeField] private bool isPermanent; // If true, the door remains open after activation

    [Header("Components required for functionality")]
    [SerializeField] private Door door; // Reference to the door this button controls
    [SerializeField] private Animator buttonAnimator; // Animator for button press animation

    private int objectsOnButton = 0; // Counter to track objects on the button

    /// <summary>
    /// Triggered when an object enters the button's trigger zone.
    /// Checks if the object has a Rigidbody2D and activates the button.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.attachedRigidbody != null) // Check if the object has a Rigidbody2D
        {
            if (objectsOnButton == 0) // If it's the first object, activate the button
            {
                ActivateButton();
            }
            objectsOnButton++;
        }
    }

    /// <summary>
    /// Triggered when an object exits the button's trigger zone.
    /// Checks if the object has a Rigidbody2D and deactivates the button if no objects remain.
    /// </summary>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.attachedRigidbody != null) // Check if the object has a Rigidbody2D
        {
            objectsOnButton--;
            if (objectsOnButton <= 0) // If no objects remain, deactivate the button
            {
                DeactivateButton();
            }
        }
    }

    /// <summary>
    /// Activates the button, opening the door and playing the press animation.
    /// </summary>
    private void ActivateButton()
    {
        door.Open();
        buttonAnimator.Play("ButtonDown");
    }

    /// <summary>
    /// Deactivates the button, closing the door (if not permanent) and playing the release animation.
    /// </summary>
    private void DeactivateButton()
    {
        if (!isPermanent)
        {
            door.Close();
        }
        buttonAnimator.Play("ButtonUp");
    }
}
