using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;

    public AudioClip backGround;

    private void Start()
    {
        musicSource.clip = backGround;
        musicSource.Play();
    }

}
