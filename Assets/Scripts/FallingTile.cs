using UnityEngine;
using System.Collections;

public class FallingTile : MonoBehaviour
{
    [SerializeField] private float warningTime = 1f;
    [SerializeField] private AudioSource audioSource;
[SerializeField] private AudioClip impactClip;

    private Rigidbody rb;
    private Renderer rend;
    private bool isFalling = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();

        rb.useGravity = false;
        rb.isKinematic = true;
    }

    public void TriggerFall()
    {
        if (isFalling) return;
        StartCoroutine(FallRoutine());
    }

    private IEnumerator FallRoutine()
    {

        if(audioSource != null && impactClip != null)
        {
            audioSource.PlayOneShot(impactClip);
        }
        isFalling = true;

        rend.material.color = Color.red;

        yield return new WaitForSeconds(warningTime);

        rb.isKinematic = false;
        rb.useGravity = true;
    }

    public bool IsFalling()
    {
        return isFalling;
    }
}