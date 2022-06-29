using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioControl;

    [SerializeField]
    private AudioClip[] sounds;

    private void Start() {
        audioControl = GetComponent<AudioSource>(); 
    }

    public void seleccionAudio(int indice, float volumen){
        audioControl.PlayOneShot(sounds[indice], volumen);
    }
}
