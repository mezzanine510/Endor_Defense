using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("xSpeed")] [SerializeField] float xSpeed = 55f;
    [Tooltip("ySpeed")] [SerializeField] float ySpeed = 40f;

    [SerializeField] float positionYawFactor = 0.25f;
    [SerializeField] float controlYawFactor = 15f;
    [SerializeField] float positionPitchFactor = 0.25f;
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float controlRollFactor = -20f;

    float minXPos = -29f;
    float maxXPos = 29f;
    float minYPos = -15f;
    float maxYPos = 17f;
    float xThrow;
    float yThrow;
    float xOffsetThisFrame;
    float yOffsetThisFrame;
    float delta;
    Vector3 newLocalPosition;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        delta = Time.deltaTime;
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
        newLocalPosition = transform.localPosition;

        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        xOffsetThisFrame = xThrow * xSpeed * delta;
        yOffsetThisFrame = yThrow * ySpeed * delta;

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
