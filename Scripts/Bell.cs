using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        _animator.SetBool("IsRinging", true);
        StartCoroutine(FadeIn());
    }

    public void StopRing()
    {
        _animator.SetBool("IsRinging", false);
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        _alarm.Play();

        while (_alarm.volume < 1f)
        {
            _alarm.volume = Mathf.MoveTowards(_alarm.volume, 1f, 0.01f);
            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        while (_alarm.volume > 0f)
        {
            _alarm.volume = Mathf.MoveTowards(_alarm.volume, 0f, 0.01f);
            yield return null;
        }

        _alarm.Stop();
    }
}
