using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("Speed")]
    [Tooltip("xSpeed")] [SerializeField] float xSpeed = 55f;
    [Tooltip("ySpeed")] [SerializeField] float ySpeed = 40f;

    [Header("Position-based rotation")]
    [SerializeField] float positionYawFactor = 1.5f;
    [SerializeField] float positionPitchFactor = 0f;

    [Header("Control-based rotation")]
    [SerializeField] float controlYawFactor = 15f;
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float controlRollFactor = -20f;

    float minXPos = -22f;
    float maxXPos = 22f;
    float minYPos = -14f;
    float maxYPos = 14f;

    float xThrow;
    float yThrow;
    float delta;
    bool controlsEnabled = true;

    void Update()
    {
        if (controlsEnabled) {
            delta = Time.deltaTime;
            xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
            yThrow = CrossPlatformInputManager.GetAxis("Vertical");
            Translate();
            Rotate();
        }
    }

    void Translate()
    {
        Vector3 newLocalPosition = transform.localPosition;

        float xOffsetThisFrame = xThrow * xSpeed * delta;
        float yOffsetThisFrame = yThrow * ySpeed * delta;

        newLocalPosition.x = Mathf.Clamp(transform.localPosition.x + xOffsetThisFrame, minXPos, maxXPos);
        newLocalPosition.y = Mathf.Clamp(transform.localPosition.y + yOffsetThisFrame, minYPos, maxYPos);

        transform.localPosition = newLocalPosition;
    }

    void Rotate()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float yawDueToPosition = transform.localPosition.x * positionYawFactor;
        float yawDueToControlThrow = xThrow * controlYawFactor;

        float yaw = yawDueToPosition + yawDueToControlThrow;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float roll = xThrow * controlRollFactor;
        
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void OnPlayerCrash() // called by string reference
    {
        controlsEnabled = false;
    }
}
