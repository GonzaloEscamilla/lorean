using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IGameAudio
{
   [SerializeField] private AudioClip mainSong;
   [SerializeField] private AudioSource audioSource;

   public void PlayMainSong()
   {
      audioSource.clip = mainSong;
      audioSource.Play();
   }
}

public interface IGameAudio
{
   public void PlayMainSong();
}
