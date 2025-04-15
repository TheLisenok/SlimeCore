using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeContactSound : MonoBehaviour
{
    [SerializeField] private SlimeSoundClips _clips;

    private List<AudioSource> _moveAudioSources;

    private void Awake()
    {
        _moveAudioSources = _clips.Move;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int _choice = Random.Range(0, _moveAudioSources.Count);
        _moveAudioSources[_choice].Play();
    }
}
