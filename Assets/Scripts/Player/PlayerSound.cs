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
        Vector3 horizontalVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        isMoving = horizontalVel.magnitude > 0.2f;
        isRunning = Input.GetKey(KeyCode.LeftShift);

        HandleFootsteps();
        HandleBreathing();
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
            }

            float interval = isRunning ? stepInterval * 0.7f : stepInterval;
            nextStepTime = Time.time + interval;
        }
    }

    void HandleBreathing()
    {
        if (breathingClip == null) return;

        Vector3 horizontalVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        bool isMoving = horizontalVel.magnitude > 0.2f;

        if (isRunning && isMoving)
        {
            if (!breathingSource.isPlaying)
            {
                breathingSource.clip = breathingClip;
                breathingSource.volume = breathingVolume;
                breathingSource.loop = true;
                breathingSource.Play();
            }
        }
        else if (breathingSource.isPlaying)
        {
            breathingSource.Stop();
        }
    }
}
