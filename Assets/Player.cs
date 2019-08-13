using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("xSpeed")] [SerializeField] float xSpeed = 55f;
    [Tooltip("ySpeed")] [SerializeField] float ySpeed = 40f;

    [SerializeField] float positionYawFactor = 1.5f;
    [SerializeField] float controlYawFactor = 15f;
    [SerializeField] float positionPitchFactor = 0f;
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float controlRollFactor = -20f;
    float minXPos = -22f;
    float maxXPos = 22f;
    float minYPos = -14f;
    float maxYPos = 14f;

    float xThrow;
    float yThrow;
    float delta;

    void Awake()
    {

    }

    void Start()
    {
        
    }

    void Update()
    {
        delta = Time.deltaTime;
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        Translate();
        Rotate();
        // float newXPos = currentLocalPos.x + xOffsetThisFrame;
        // float newYPos = currentLocalPos.y + yOffsetThisFrame;

        // if (newXPos < maxXPos && newXPos > minXPos)
        // {
        //     transform.localPosition = new Vector3(
        //         newXPos, 
        //         transform.localPosition.y, 
        //         transform.localPosition.z
        //     );
        // }

        // if (newYPos < maxYPos && newYPos > minYPos)
        // {
        //     transform.localPosition = new Vector3(
        //         transform.localPosition.x,
        //         newYPos,
        //         transform.localPosition.z
        //     );
        // }

        // print(xThrow);
        // print(xOffsetThisFrame);
    }

    void Translate()
    {
        Vector3 newLocalPosition = transform.localPosition;

        // xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        // yThrow = CrossPlatformInputManager.GetAxis("Vertical");

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
}
