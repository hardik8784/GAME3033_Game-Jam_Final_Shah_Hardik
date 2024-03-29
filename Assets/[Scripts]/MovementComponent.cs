///-----------------------------
///     Author          : Hardik Shah
///     Last Modified   : 2022/04/22
///     Description     : Script 
///     Revision History: Added Movement component
/// ----------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class MovementComponent : MonoBehaviour
{
    //Movement Variables
    [SerializeField]
    float WalkSpeed = 5.0f;
    [SerializeField]
    float RunSpeed = 10.0f;
    [SerializeField]
    float JumpForce = 5.0f;

    [Header("Player Pickups")]
    //public int Astronaut_Coin = 0;
    //public TMP_Text Astronaut_CoinText;

    //Components
    PlayerController playerController;
    Rigidbody RigidBody;
    Animator Playeranimator;
    public GameObject Coin;

    //Movement Refrences
    Vector2 InputVector = Vector2.zero;
    Vector3 MoveDirection = Vector3.zero;

    //public AudioClip Astronaut_Coin_Clip;

    public readonly int MovementXHash = Animator.StringToHash("MovementX");
    public readonly int MovementYHash = Animator.StringToHash("MovementY");
    public readonly int IsWalkingHash = Animator.StringToHash("IsWalking");
    public readonly int IsJumpingHash = Animator.StringToHash("IsJumping");
    public readonly int IsRunningHash = Animator.StringToHash("IsRunning");
    public readonly int IsFallingHash = Animator.StringToHash("IsFalling");

    private void Awake()
    {
        Playeranimator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        RigidBody = GetComponent<Rigidbody>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        playerController.isFalling = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Astronaut_Coin >= 3)
        //{
        //    UnityEngine.SceneManagement.SceneManager.LoadScene("Win");
        //}

        if (playerController.isJumping) return;
        if (!(InputVector.magnitude > 0.0f))
        {
            MoveDirection = Vector3.zero;
            playerController.isWalking = false;
            Playeranimator.SetBool(IsWalkingHash, playerController.isWalking);
        }
        MoveDirection = transform.forward * InputVector.y + transform.right * InputVector.x;
        float CurrentSpeed = playerController.isRunning ? RunSpeed : WalkSpeed;

        if (CurrentSpeed == WalkSpeed)
        {
            playerController.isRunning = false;
            Playeranimator.SetBool(IsRunningHash, playerController.isRunning);
        }

        Vector3 MovementDirection = MoveDirection * (CurrentSpeed * Time.deltaTime);

        transform.position += MovementDirection;

        CheckFalling();

    }

    public void CheckFalling()
    {
        if (this.transform.position.y < -1)
        {
            playerController.isFalling = true;
            Playeranimator.SetBool(IsFallingHash, playerController.isFalling);
        }
    }

    public void OnMovement(InputValue value)
    {
        playerController.isWalking = true;
        Playeranimator.SetBool(IsWalkingHash, playerController.isWalking);
        InputVector = value.Get<Vector2>();
        Playeranimator.SetFloat(MovementXHash, InputVector.x);
        Playeranimator.SetFloat(MovementYHash, InputVector.y);
        //print("InputVector:" + InputVector);
    }

    public void OnRun(InputValue value)
    {
        playerController.isRunning = value.isPressed;
        Playeranimator.SetBool(IsRunningHash, playerController.isRunning);
    }

    public void OnJump(InputValue value)
    {
        //CheckFalling();
        if (playerController.isJumping) return;

        playerController.isJumping = value.isPressed;
        RigidBody.AddForce((transform.up + MoveDirection) * JumpForce, ForceMode.Impulse);
        Playeranimator.SetBool(IsJumpingHash, playerController.isJumping);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground") && !playerController.isJumping) return;

        playerController.isJumping = false;

        Playeranimator.SetBool(IsJumpingHash, false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Coin")
        {
            print("Increase Timer");
            playerController.CountdownTimer += 10;
            Destroy(other.gameObject);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Coin"))
    //    {
    //        Debug.Log(other.gameObject);

    //        Destroy(other.gameObject);
    //    }
    //}

}
