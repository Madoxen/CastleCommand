using UnityEngine;
using System.Collections;


[RequireComponent(typeof(AudioSource))]
public class VariablePitchClipPlayer : MonoBehaviour
{
    AudioSource AS;
    public float PitchHigherBound;
    public float PitchLowerBound;

    private void Awake()
    {
        AS = GetComponent<AudioSource>();
    }


    public void PlaySound()
    {
        AS.pitch = Random.Range(PitchLowerBound, PitchHigherBound);
        AS.Play();
    }
  
}

