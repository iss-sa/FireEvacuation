using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---Audio Source---")]
    [SerializeField] private AudioSource backgroundSource;
    [SerializeField] private AudioSource SFXSource;

    [Header("---Audio Clips---")]
    public AudioClip door;
    public AudioClip elevator;
    public AudioClip fire;
    public AudioClip fireAlarm;
    public AudioClip fireSafetyTools;
    public AudioClip fireShout;
    public AudioClip phoneBreak;
    public AudioClip phoneCall;
    public AudioClip scream;
    public AudioClip stairsSteps;
    public AudioClip windowBreak;
    public static AudioManager instance;

    private void Awake()
    {
        // if there are two Audio Managers, destroy one, keep other (only one needed)
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // keep current audio manager throughout all scenes
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        backgroundSource.clip = fire;
        backgroundSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
