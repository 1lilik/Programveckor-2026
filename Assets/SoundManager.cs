using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        AudioSource[] sources = GetComponents<AudioSource>();
        foreach (AudioSource source in sources)
        {
            if (source.clip == null)
            {
                soundEffectAudio = source;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static SoundManager Instance = null;
    private AudioSource soundEffectAudio;
    [field: SerializeField]
    public AudioClip placeholder1 { get; set; }

    [field: SerializeField]
    public AudioClip placeholder2 { get; set; }

    [field: SerializeField]
    public AudioClip placeholder3 { get; set; }

    [field: SerializeField]
    public AudioClip placeholder4 { get; set; }

    [field: SerializeField]
    public AudioClip placeholder5 { get; set; }

    [field: SerializeField]
    public AudioClip placeholder6 { get; set; }

    [field: SerializeField]
    public AudioClip placeholder7 { get; set; }

    [field: SerializeField]
    public AudioClip placeholder8 { get; set; }

    [field: SerializeField]
    public AudioClip placeholder9 { get; set; }
}
