 using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerSound : MonoBehaviour
{
    [Header("===== Footstep Settings =====")]
    public AudioClip[] footstepClips;
    public float stepInterval = 0.5f;
    [Range(0f, 1f)] public float footstepVolume = 0.6f;
    [Range(0.5f, 2f)] public float footstepPitch = 1.0f;

    [Header("===== Running Breath Settings =====")]
    public AudioClip breathingClip;
    [Range(0f, 1f)] public float breathingVolume = 0.3f;

    private AudioSource footstepSource;
    private AudioSource breathingSource;
    private Rigidbody rb;
    private float nextStepTime = 0f;
    private bool isMoving;
    private bool isRunning;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Audio bước chân
        footstepSource = gameObject.AddComponent<AudioSource>();
        footstepSource.loop = false;

        // Audio thở
        breathingSource = gameObject.AddComponent<AudioSource>();
        breathingSource.loop = true;
    }

    void Update()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        isMoving = input.magnitude > 0.1f; 
        isRunning = Input.GetKey(KeyCode.LeftShift);

        HandleFootsteps();
        HandleBreathing();
        Debug.Log("Player is moving: " + isMoving + ", is running: " + isRunning);
    }

    void HandleFootsteps()
    {
        if (!isMoving) return;

        if (Time.time >= nextStepTime)
        {
            if (footstepClips.Length > 0)
            {
                AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];
                footstepSource.pitch = footstepPitch + Random.Range(-0.1f, 0.1f);
                footstepSource.PlayOneShot(clip, footstepVolume);
                Debug.Log("Played footstep sound: " + clip.name);
            }

            float interval = isRunning ? stepInterval * 0.7f : stepInterval;
            nextStepTime = Time.time + interval;
        }
    }

    void HandleBreathing()
    {
        if (breathingClip == null) return;

        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        bool isMoving = input.magnitude > 0.1f;

        if (isRunning && isMoving)
        {
            if (!breathingSource.isPlaying)
            {
                breathingSource.clip = breathingClip;
                breathingSource.volume = breathingVolume;
                breathingSource.loop = true;
                breathingSource.Play();
                Debug.Log("Started playing breathing sound.");
            }
        }
        else if (breathingSource.isPlaying)
        {
            breathingSource.Stop();
        }
    }
}
