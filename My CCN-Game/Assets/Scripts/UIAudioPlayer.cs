using UnityEngine;

public class UIAudioPlayer : MonoBehaviour
{
    public AudioClip buttonClick;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayButtonClick()
    {
        audioSource.PlayOneShot(buttonClick);
    }
}