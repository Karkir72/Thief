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
        StopCoroutine(nameof(FadeVolume));
        StartCoroutine(FadeVolume(1));
    }

    public void StopRing()
    {
        _animator.SetBool(AnimatorAlarm.Params.IsRinging, false);
        StopCoroutine(nameof(FadeVolume));
        StartCoroutine(FadeVolume(0));
    }

    private IEnumerator FadeVolume(float targetVolume)
    {
        if (_alarm.volume == 0)
        {
            _alarm.Play();
        }

        while (_alarm.volume != targetVolume)
        {
            _alarm.volume = Mathf.MoveTowards(_alarm.volume, targetVolume, 0.01f);
            yield return null;
        }

        if (_alarm.volume == 0)
        {
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