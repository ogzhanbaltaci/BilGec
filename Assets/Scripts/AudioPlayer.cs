using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Jump")]
    [SerializeField] AudioClip jumpClip;
    [SerializeField] [Range(0f, 1f)] float jumpClipVolume = 1f;
    [Header("Jump")]
    [SerializeField] AudioClip playerHurtClip;
    [SerializeField] [Range(0f, 1f)] float playerHurtClipVolume = 1f;
    [Header("Jump")]
    [SerializeField] AudioClip enemyHurtClip;
    [SerializeField] [Range(0f, 1f)] float enemyHurtClipVolume = 1f;
    [Header("Music")]
    [SerializeField] AudioClip musicClip;
    [SerializeField] [Range(0f, 1f)] float musicClipVolume = 1f;
    [Header("MenuButton")]
    [SerializeField] AudioClip menuButtonClip;
    [SerializeField] [Range(0f, 1f)] float menuButtonClipVolume = 1f;
    AudioSource audioSource;
    void Awake()
    {
        audioSource = FindObjectOfType<AudioSource>();
        ManageSingleton();
    }
    void ManageSingleton()
    {
        int instanceCount = FindObjectsOfType(GetType()).Length;
        if(instanceCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public void PlayPlayerHurtClip()
    {
        PlayClip(playerHurtClip, playerHurtClipVolume);
    }
    public void PlayEnemyHurtClip()
    {
        PlayClip(enemyHurtClip, enemyHurtClipVolume);
    }
    public void PlayInGameMusicClip()
    {
        audioSource.clip = musicClip;
        audioSource.Play();
    }
    public void PlayJumpClip()
    {
        PlayClip(jumpClip, jumpClipVolume);
    }
    public void PlayMenuButtonClip()
    {
        PlayClip(menuButtonClip, menuButtonClipVolume);
    }
    void PlayClip(AudioClip clip, float volume)
    {
        if(clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }
}
