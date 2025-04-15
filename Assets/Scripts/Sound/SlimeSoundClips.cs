using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSoundClips : MonoBehaviour
{
    public AudioSource MoveAmbient;
    public AudioSource Damage;
    public AudioSource Jump;
    public AudioSource Land;
    public List<AudioSource> Move = new List<AudioSource>();
}
