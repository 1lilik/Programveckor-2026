using UnityEngine;

public enum SoundType
{
    PLACEHOLDER1,
    PLACEHOLDER2,
    PLACEHOLDER3,
    PLACEHOLDER4,
    PLACEHOLDER5,
    PLACEHOLDER6,
    PLACEHOLDER7,
    PLACEHOLDER8
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundlist;
    private static SoundManager instance;
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound, float volume = 1)
    {
        instance.audioSource.PlayOneShot(instance.soundlist[(int)sound], volume);
        Debug.Log("Ljud bra" + sound.ToString());
    }
}
