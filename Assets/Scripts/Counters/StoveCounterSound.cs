using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;


    private AudioSource audioSource;
    private float warningSoundTimer;
    private bool playWarningSound;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        SoundManager.OnVolumeChanged += SoundManager_OnVolumeChanged;
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    { 
        float burnShowProgressAmount = .5f;
        playWarningSound = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;
    }

    private void SoundManager_OnVolumeChanged(object sender, System.EventArgs e)
    {
        float volume = SoundManager.Instance.GetVolume();
        audioSource.volume = volume;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        if (playSound)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
    private void Update()
    {
        if (playWarningSound)
        {
            warningSoundTimer -= Time.deltaTime;
            if (warningSoundTimer <= 0f)
            {
                float warningSoundTimerMax = .2f;
                warningSoundTimer = warningSoundTimerMax;

                SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
            }
        }
    }
}
