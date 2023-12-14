using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO.Ports;


public class RabbitJump : MonoBehaviour
{
    public float jumpHeight = 2f;    // jump height
    public float jumpDistance = 1.1f;  // jump distance
    public float jumpLongDistance = 2f;  // long jump distance

    public float holdDuration = 6f;   // hold duration
    public float jumpDuration = 2f;
    public float winJumpDuration = 2f;

    public float rotDuration;

    public CircularProgressBar progressBarFill;
    private GameController gameController;
    private float winJumpStartTime;

    private Vector3 winOriginalPosition;
    private Quaternion winOriginalRotation;
    private float winJumpHeight = 2f;
    private float rotationAmount = 360f;


    private float timeHoldingSpace = 0f;
    private bool jumpStarted = false;
    private Vector3 jumpStartPos;
    private Quaternion jumpStartRotation;
    private bool jumping = false;
    private bool rotateLeft = false;
    private bool rotateRight = false;
    private bool longJumping = false;


    private bool winJumping = false;

    private float jumpStartedTime;
    public int currentSeqNum = 0;

    public ParticleSystem particleSystem;

    SerialPort stream = new SerialPort("/dev/cu.usbmodem1421", 9600); // customize based on the serail port

    public int handState = 0; // 0: normal pos; 1: left turn gesture; 2: right turn gesture; 3: slight hold gesture; 4: stretch (large) hold gesture

    void Start()
    {
        gameController = GetComponent<GameController>();
        jumpStartRotation = transform.rotation;

        stream.Open(); // Open the Serial Stream
    }


    void Update()
    {
        string value = stream.Readline();
        handState = int.Parse(value);

        if (handState != 0)
        {
            if (handState == 1)
            {
                timeHoldingSpace += Time.deltaTime;
                float progress = Mathf.Clamp01(timeHoldingSpace / holdDuration);
                progressBarFill.Progress = Mathf.Clamp01(progress); // restrict to [0,1]

                // if hold for certain duration and have not yet started to jump
                if (timeHoldingSpace >= holdDuration && !jumpStarted)
                {
                    jumpStarted = true;
                    jumpStartPos = transform.position;
                    jumpStartRotation = transform.Find("rabbit").rotation * Quaternion.Euler(0f, 90f, 0f);
                    jumpStartedTime = Time.time;
                    rotateLeft = true;
                }
            }
            else if (handState == 2)
            {
                timeHoldingSpace += Time.deltaTime;
                float progress = Mathf.Clamp01(timeHoldingSpace / holdDuration);
                progressBarFill.Progress = Mathf.Clamp01(progress); // restrict to [0,1]

                // if hold for certain duration and have not yet started to jump
                if (timeHoldingSpace >= holdDuration && !jumpStarted)
                {
                    jumpStarted = true;
                    jumpStartPos = transform.position;
                    jumpStartRotation = transform.Find("rabbit").rotation * Quaternion.Euler(0f, 90f, 0f);
                    jumpStartedTime = Time.time;
                    rotateRight = true;
                }
            }
            else if (handState == 3)
            {
                timeHoldingSpace += Time.deltaTime;
                float progress = Mathf.Clamp01(timeHoldingSpace / holdDuration);
                progressBarFill.Progress = Mathf.Clamp01(progress); // restrict to [0,1]

                // if hold for certain duration and have not yet started to jump
                if (timeHoldingSpace >= holdDuration && !jumpStarted)
                {
                    jumpStarted = true;
                    jumpStartPos = transform.position;
                    jumpStartRotation = transform.Find("rabbit").rotation * Quaternion.Euler(0f, 90f, 0f);
                    jumpStartedTime = Time.time;
                    jumping = true;
                }
            }
            else if (handState == 4)
            {
                timeHoldingSpace += Time.deltaTime;
                float progress = Mathf.Clamp01(timeHoldingSpace / holdDuration);
                progressBarFill.Progress = Mathf.Clamp01(progress); // restrict to [0,1]

                // if hold for certain duration and have not yet started to jump
                if (timeHoldingSpace >= holdDuration && !jumpStarted)
                {
                    jumpStarted = true;
                    jumpStartPos = transform.position;
                    jumpStartRotation = transform.Find("rabbit").rotation * Quaternion.Euler(0f, 90f, 0f);
                    jumpStartedTime = Time.time;
                    longJumping = true;
                }
            }
        }
        else
        {
            timeHoldingSpace = 0f;
            jumpStarted = false;
            progressBarFill.Progress = 0; // reset progress bar
        }

        // short jump
        if (jumping && (currentSeqNum == 0 || currentSeqNum == 2))
        {
            float normalizedTime = (Time.time - jumpStartedTime) / jumpDuration;
            float yOffset = Mathf.Sin(normalizedTime * Mathf.PI) * jumpHeight;
            float xOffset = normalizedTime * jumpDistance;

            Vector3 jumpDirection = jumpStartRotation * Vector3.forward;
            Vector3 newPosition = jumpStartPos + jumpDirection * xOffset + Vector3.up * yOffset;
            transform.position = newPosition;

            if (normalizedTime >= 1f)
            {
                jumping = false;
                gameController.ChangeImage();
                currentSeqNum++;
            }
        }

        // rotate
        if (rotateLeft && currentSeqNum == 1)
        {
            float normalizedTime = (Time.time - jumpStartedTime) / rotDuration;

            // calculate rotation angle
            float rotationAngle = 60f;

            // only rotate the rabbit
            Transform rabbitTransform = transform.Find("rabbit");
            // Debug.Log(rabbitTransform.rotation.eulerAngles.y);

            // Quaternion targetRotation = Quaternion.Euler(0f, rotationAngle, 0f);
            // rabbitTransform.rotation = Quaternion.Lerp(rabbitTransform.rotation, targetRotation, normalizedTime);

            // calculate rotation angle
            float currentRotationAngle = Mathf.Lerp(120f, rotationAngle, normalizedTime);

            // calculate rotation
            Quaternion currentRotation = Quaternion.Euler(0f, currentRotationAngle, 0f);

            // implement rotation
            rabbitTransform.rotation = currentRotation;


            if (normalizedTime >= 1f) // rotate end
            {
                rotateLeft = false;
                gameController.ChangeImage();
                currentSeqNum++;
            }
        }

        // rotate case 2
        if (rotateRight && currentSeqNum == 3)
        {
            float normalizedTime = (Time.time - jumpStartedTime) / rotDuration;

            // calcu rotation angle
            float rotationAngle = 120f;

            // rotate only the rabbit
            Transform rabbitTransform = transform.Find("rabbit");
            // Debug.Log(rabbitTransform.rotation.eulerAngles.y);

            // Quaternion targetRotation = Quaternion.Euler(0f, rotationAngle, 0f);
            // rabbitTransform.rotation = Quaternion.Lerp(rabbitTransform.rotation, targetRotation, normalizedTime);

            // calcu rotation angle
            float currentRotationAngle = Mathf.Lerp(60f, rotationAngle, normalizedTime);

            // calcu rotation
            Quaternion currentRotation = Quaternion.Euler(0f, currentRotationAngle, 0f);

            // apply rotation
            rabbitTransform.rotation = currentRotation;


            if (normalizedTime >= 1f) // rotation finished
            {
                rotateRight = false;
                gameController.ChangeImage();
                currentSeqNum++;
            }
        }

        // long jump
        if (longJumping && currentSeqNum == 4)
        {
            float normalizedTime = (Time.time - jumpStartedTime) / jumpDuration;
            float yOffset = Mathf.Sin(normalizedTime * Mathf.PI) * jumpHeight;
            float xOffset = normalizedTime * jumpLongDistance;

            Vector3 jumpDirection = jumpStartRotation * Vector3.forward;
            Vector3 newPosition = jumpStartPos + jumpDirection * xOffset + Vector3.up * yOffset;
            transform.position = newPosition;

            if (normalizedTime >= 1f) //arrived
            {
                longJumping = false;
                gameController.HideImage();
                // currentSeqNum++;
                winJumping = true;
                winJumpStartTime = Time.time;
                winOriginalPosition = transform.Find("rabbit").position;
                winOriginalRotation = transform.Find("rabbit").rotation;

                // play particle effect
                if (particleSystem != null)
                {
                    particleSystem.Play();
                }

            }
        }


        if (winJumping)
        {
            float normalizedTime = (Time.time - winJumpStartTime) / winJumpDuration;

            if (normalizedTime <= 1f)
            {
                // calcu up/down distance
                float yOffset = Mathf.Sin(normalizedTime * Mathf.PI) * winJumpHeight;
                Vector3 newPosition = winOriginalPosition + Vector3.up * yOffset;

                // calcu rotate
                float rotationAngle = rotationAmount * normalizedTime;
                Quaternion newRotation = winOriginalRotation * Quaternion.Euler(0f, rotationAngle, 0f);

                // apply rise and rotate
                transform.Find("rabbit").position = newPosition;
                transform.Find("rabbit").rotation = newRotation;
            }
            else
            {
                // reset
                winJumping = false;
                transform.Find("rabbit").position = winOriginalPosition;
                transform.Find("rabbit").rotation = winOriginalRotation;
            }
        }

    }


}

