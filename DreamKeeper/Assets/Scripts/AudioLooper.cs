using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLooper : MonoBehaviour
{
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        if (EndSampleCount == 0)
        {
            EndSampleCount = audioSource.clip.samples - 1024;
        }
    }

    void Update()
    {
        if (!Loop)
        {
            audioSource.loop = false;
            return;
        }
        if(EndSampleCount <= audioSource.timeSamples)
        {
            audioSource.timeSamples = StartSampleCount;
        }
    }

    public int StartSampleCount = 0;
    public int EndSampleCount = 0;
    public bool Loop = true;
}
