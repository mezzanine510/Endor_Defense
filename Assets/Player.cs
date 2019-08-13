using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("xSpeed")] [SerializeField] float xSpeed = 45f;
    [Tooltip("ySpeed")] [SerializeField] float ySpeed = 35f;

    [SerializeField] float positionPitchFactor = 0.25f;
    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float controlRollFactor = -15f;

    float minXPos = -17f;
    float maxXPos = 17f;
    float minYPos = -14.5f;
    float maxYPos = 15f;
    float xThrow;
    float yThrow;
    float xOffsetThisFrame;
    float yOffsetThisFrame;
    float delta;
    Vector3 currentLocalPos;

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
        currentLocalPos = transform.localPosition;

        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        xOffsetThisFrame = xThrow * xSpeed * delta;
        yOffsetThisFrame = yThrow * ySpeed * delta;

        float newXPos = Mathf.Clamp(currentLocalPos.x + xOffsetThisFrame, minXPos, maxXPos);

        float newYPos = Mathf.Clamp(currentLocalPos.y + yOffsetThisFrame, minYPos, maxYPos);

        transform.localPosition = new Vector3(
            newXPos, 
            newYPos,
            transform.localPosition.z
        );
    }

    void Rotate()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = 0f;
        float roll = xThrow * controlRollFactor;
        
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
}
