using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a door that can be opened and closed using an animator.
/// </summary>
public class Door : MonoBehaviour
{
    [SerializeField] private Animator doorAnimator; // Reference to the Animator controlling door animations

    /// <summary>
    /// Opens the door by triggering the corresponding animation.
    /// </summary>
    public void Open()
    {
        doorAnimator.SetTrigger("OpenDoor");
    }

    /// <summary>
    /// Closes the door by triggering the corresponding animation.
    /// </summary>
    public void Close()
    {
        doorAnimator.SetTrigger("CloseDoor");
    }
}
