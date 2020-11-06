using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum BGMType { Stage = 0, Boss}
public class BgmController : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] bgmClips;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ChangeBgm(BGMType index)
    {
        audioSource.Stop();

        audioSource.clip = bgmClips[(int)index];
        audioSource.Play();

    }
}
