using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public AudioClip eggTakenMusic;
    public AudioClip templeMusic;
    private AudioClip startMusic;

    private AudioSource _audioSource;

    public void Play(AudioClip clip, bool fromBeginning = false, bool loop = true) {
        float savePosition = _audioSource.time;
        _audioSource.Pause();
        _audioSource.clip = clip;
        _audioSource.loop = loop;
        _audioSource.time = fromBeginning ? 0 : savePosition;
        _audioSource.Play();
    }
    public void Stop() {
        _audioSource.Stop();
    }
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        startMusic = _audioSource.clip;
        ServiceManager.Instance.Get<OnDeath>().Subscribe(HandleEnd);
        ServiceManager.Instance.Get<OnExit>().Subscribe(HandleEnd);
        ServiceManager.Instance.Get<OnEggTaken>().Subscribe(HandleEggTaken);
        ServiceManager.Instance.Get<TempleEntered>().Subscribe(HandleTempleEntered);
    }

    private void HandleTempleEntered()
    {
        ServiceManager.Instance.Get<TempleEntered>().Unsubscribe(HandleTempleEntered);
        Play(templeMusic, true, true);
    }

    private void HandleEggTaken()
    {
        ServiceManager.Instance.Get<OnEggTaken>().Unsubscribe(HandleEggTaken);
        Play(eggTakenMusic, true, true);
    }
    private void HandleEnd()
    {
        ServiceManager.Instance.Get<OnExit>().Unsubscribe(HandleEnd);
        ServiceManager.Instance.Get<OnDeath>().Unsubscribe(HandleEnd);
        Play(startMusic, true, false);
    }
}
