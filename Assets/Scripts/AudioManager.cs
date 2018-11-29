using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

    public short audioInstancesQtd = 4;
    public float instancesDistance = 2f;

    public Oscilator oscilator1;
    public Oscilator oscilator2;
    public Oscilator oscilator3;

    private AudioSource[] audioSources;
    private bool playingAudio = false;

    protected virtual void Awake()
    {
        base.Awake();
        audioSources = new AudioSource[audioInstancesQtd];

        float rotStep = 360.0f / audioInstancesQtd;
        float angle = 0f;
        for (int i = 0; i < audioInstancesQtd; i++)
        {
            //New audio source
            GameObject newGameObject = new GameObject("AudioSource" + (i + 1));
            newGameObject.transform.parent = transform;

            //Positioning
            newGameObject.transform.Translate(
                new Vector3(
                    Mathf.Cos(angle * Mathf.Deg2Rad),
                    0f,
                    Mathf.Sin(angle * Mathf.Deg2Rad))
                    * instancesDistance);

            //Actually creating audioSource
            audioSources[i] = newGameObject.AddComponent<AudioSource>();

            //Increasing angle value
            angle += rotStep;
        }
    }
    
    public void PlayAudio(AudioClip audioClip)
    {
        if (!playingAudio)
        {
            StartCoroutine(WaitForAudio(audioClip));
        }
    }

    public void PlayAudio(AudioClip audioClip, System.Action audioEnded)
    {
        if (!playingAudio)
        {
            StartCoroutine(WaitForAudio(audioClip, audioEnded));
        }
    }

    private void ChooseAudioSource(AudioClip audioClip)
    {
        AudioSource chosenAudioSource = audioSources[Random.Range(0, audioInstancesQtd - 1)];
        chosenAudioSource.clip = audioClip;
        chosenAudioSource.Play();
    }
    
    IEnumerator WaitForAudio(AudioClip audio)
    {
        playingAudio = true;
        Instance.ChooseAudioSource(audio);
        yield return new WaitForSeconds(audio.length);
        playingAudio = false;
    }

    IEnumerator WaitForAudio(AudioClip audio, System.Action onAudioEnd)
    {
        playingAudio = true;
        Instance.ChooseAudioSource(audio);
        yield return new WaitForSeconds(audio.length);
        playingAudio = false;
        onAudioEnd();
    }

}