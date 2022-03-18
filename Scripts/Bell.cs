using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]

public class Bell : MonoBehaviour
{
    private AudioSource _alarm;
    private Animator _animator;

    private void Start()
    {
        _alarm = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    public void StartRing()
    {
        _animator.SetBool(AnimatorAlarm.Params.IsRinging, true);
        StartCoroutine(FadeVolume());
    }

    public void StopRing()
    {
        _animator.SetBool(AnimatorAlarm.Params.IsRinging, false);
        StartCoroutine(FadeVolume());
    }

    private IEnumerator FadeVolume()
    {
        if (_alarm.isPlaying == false)
        {
            _alarm.Play();
            while (_alarm.volume < 1f)
            {
                _alarm.volume = Mathf.MoveTowards(_alarm.volume, 1f, 0.01f);
                yield return null;
            }
        }
        else
        {
            while (_alarm.volume > 0f)
            {
                _alarm.volume = Mathf.MoveTowards(_alarm.volume, 0f, 0.01f);
                yield return null;
            }

            _alarm.Stop();
        }
    }
}

public static class AnimatorAlarm
{
    public static class Params
    {
        public const string IsRinging = "IsRinging";
    }

    public static class States
    {
        public const string Default = "Default";
        public const string AlarmRings = "AlarmRings";
    }
}