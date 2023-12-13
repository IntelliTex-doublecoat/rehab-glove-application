using UnityEngine;
using UnityEngine.UI;

public class RabbitJump : MonoBehaviour
{
    public float jumpHeight = 2f;    // 跳跃的高度
    public float jumpDistance = 1.1f;  // 跳跃的距离
    public float jumpLongDistance = 2f;  // 长跳跃的距离

    public float holdDuration = 6f;   // 按住空格键的持续时间
    public float jumpDuration = 2f;  // 跳跃的持续时间
    public float winJumpDuration = 2f;  // 跳跃的持续时间

    public float rotDuration;  // 跳跃的持续时间

    public CircularProgressBar progressBarFill; // 圆形进度条的填充部分，拖拽进来
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
    private bool winJumping = false;

    private float jumpStartedTime;
    public int currentSeqNum = 0;

    public ParticleSystem particleSystem; // 将 Particle System 组件引用拖放到这里


    void Start()
    {
        gameController = GetComponent<GameController>();
        jumpStartRotation = transform.rotation;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            timeHoldingSpace += Time.deltaTime;

            // 更新进度条填充的显示
            float progress = Mathf.Clamp01(timeHoldingSpace / holdDuration);
            progressBarFill.Progress = Mathf.Clamp01(progress); // 限制在0到1之间

            // 如果按住了6秒以上并且尚未跳跃，则开始跳跃
            if (timeHoldingSpace >= holdDuration && !jumpStarted)
            {
                jumpStarted = true;
                jumpStartPos = transform.position;
                jumpStartRotation = transform.Find("rabbit").rotation * Quaternion.Euler(0f, 90f, 0f);
                jumpStartedTime = Time.time;
                jumping = true;
            }
        }
        else
        {
            timeHoldingSpace = 0f;
            jumpStarted = false;
            progressBarFill.Progress = 0; // 重置进度条
        }

        // 短跳跃情况
        if (jumping && (currentSeqNum == 0 || currentSeqNum == 2))
        {
            float normalizedTime = (Time.time - jumpStartedTime) / jumpDuration;
            float yOffset = Mathf.Sin(normalizedTime * Mathf.PI) * jumpHeight;
            float xOffset = normalizedTime * jumpDistance;

            Vector3 jumpDirection = jumpStartRotation * Vector3.forward; // 基于起跳时的旋转方向
            Vector3 newPosition = jumpStartPos + jumpDirection * xOffset + Vector3.up * yOffset;
            transform.position = newPosition;

            if (normalizedTime >= 1f) //跳到了
            {
                jumping = false;
                gameController.ChangeImage();
                currentSeqNum++;
            }
        }

        // 转向情况
        if (jumping && currentSeqNum == 1)
        {
            float normalizedTime = (Time.time - jumpStartedTime) / rotDuration;

            // 计算旋转角度
            float rotationAngle = 60f;

            // 通过子物体的名称获取子物体的引用，只旋转兔子
            Transform rabbitTransform = transform.Find("rabbit");
            // Debug.Log(rabbitTransform.rotation.eulerAngles.y);


            // 基于当前旋转进行旋转
            // Quaternion targetRotation = Quaternion.Euler(0f, rotationAngle, 0f);
            // rabbitTransform.rotation = Quaternion.Lerp(rabbitTransform.rotation, targetRotation, normalizedTime);

            // 计算旋转角度
            float currentRotationAngle = Mathf.Lerp(120f, rotationAngle, normalizedTime);

            // 计算旋转
            Quaternion currentRotation = Quaternion.Euler(0f, currentRotationAngle, 0f);

            // 应用旋转
            rabbitTransform.rotation = currentRotation;


            if (normalizedTime >= 1f) // 旋转完成
            {
                jumping = false;
                gameController.ChangeImage();
                currentSeqNum++;
            }
        }

        // 转向情况2
        if (jumping && currentSeqNum == 3)
        {
            float normalizedTime = (Time.time - jumpStartedTime) / rotDuration;

            // 计算旋转角度
            float rotationAngle = 120f;

            // 通过子物体的名称获取子物体的引用，只旋转兔子
            Transform rabbitTransform = transform.Find("rabbit");
            // Debug.Log(rabbitTransform.rotation.eulerAngles.y);


            // 基于当前旋转进行旋转
            // Quaternion targetRotation = Quaternion.Euler(0f, rotationAngle, 0f);
            // rabbitTransform.rotation = Quaternion.Lerp(rabbitTransform.rotation, targetRotation, normalizedTime);

            // 计算旋转角度
            float currentRotationAngle = Mathf.Lerp(60f, rotationAngle, normalizedTime);

            // 计算旋转
            Quaternion currentRotation = Quaternion.Euler(0f, currentRotationAngle, 0f);

            // 应用旋转
            rabbitTransform.rotation = currentRotation;


            if (normalizedTime >= 1f) // 旋转完成
            {
                jumping = false;
                gameController.ChangeImage();
                currentSeqNum++;
            }
        }

        // 长跳跃情况
        if (jumping && currentSeqNum == 4)
        {
            float normalizedTime = (Time.time - jumpStartedTime) / jumpDuration;
            float yOffset = Mathf.Sin(normalizedTime * Mathf.PI) * jumpHeight;
            float xOffset = normalizedTime * jumpLongDistance;

            Vector3 jumpDirection = jumpStartRotation * Vector3.forward; // 基于起跳时的旋转方向
            Vector3 newPosition = jumpStartPos + jumpDirection * xOffset + Vector3.up * yOffset;
            transform.position = newPosition;

            if (normalizedTime >= 1f) //跳到了
            {
                jumping = false;
                gameController.HideImage();
                // currentSeqNum++;
                winJumping = true;
                winJumpStartTime = Time.time;
                winOriginalPosition = transform.Find("rabbit").position;
                winOriginalRotation = transform.Find("rabbit").rotation;
                
                // 播放粒子效果
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
                // 计算上升和下降的高度
                float yOffset = Mathf.Sin(normalizedTime * Mathf.PI) * winJumpHeight;
                Vector3 newPosition = winOriginalPosition + Vector3.up * yOffset;

                // 计算旋转
                float rotationAngle = rotationAmount * normalizedTime;
                Quaternion newRotation = winOriginalRotation * Quaternion.Euler(0f, rotationAngle, 0f);

                // 应用上升和旋转
                transform.Find("rabbit").position = newPosition;
                transform.Find("rabbit").rotation = newRotation;
            }
            else
            {
                // 重置位置和旋转
                winJumping = false;
                transform.Find("rabbit").position = winOriginalPosition;
                transform.Find("rabbit").rotation = winOriginalRotation;
            }
        }

    }
}