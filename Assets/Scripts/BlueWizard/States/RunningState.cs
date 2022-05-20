using UnityEngine;

public class RunningState : State<BlueWizardController>
{
    private float mMovement;
    private Rigidbody2D mRigidBody;
    private bool mIsJumpPressed;
    private bool mIsFiring;
    private Transform mTransform;
    private Animator mAnimator;
    private Transform mFireballPoint;

    public RunningState(BlueWizardController mController, 
        FiniteStateMachine<BlueWizardController> mFSM) : base(mController, mFSM)
    {
        mRigidBody = mController.GetComponent<Rigidbody2D>();
        mAnimator = mController.GetComponent<Animator>();
        mTransform = mController.transform;
        mFireballPoint = mController.transform.Find("FireballPoint");
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("Entrando al estado RUNNINGSTATE");
        mIsJumpPressed = false;
        mIsFiring = false;
    }

    public override void OnExit()
    {
        base.OnExit();
        Debug.Log("Saliendo del estado RUNNINGSTATE");
    }

    public override void OnHandleInput()
    {
        base.OnHandleInput();
        mMovement = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump")) mIsJumpPressed = true;
        if (Input.GetButtonDown("Fire1")) mIsFiring= true;
    }

    public override void OnLogicUpdate()
    {
        base.OnLogicUpdate();
        if (mMovement == 0f)
        {
            mFSM.ChangeState(mController.IdleState);
        }
        if (mIsJumpPressed)
        {
            mFSM.ChangeState(mController.JumpingState);
        }
        mAnimator.SetInteger("Move", mMovement == 0f ? 0 : 1);

        if (mIsFiring)
        {
            Fire();
            mIsFiring = false;
        }
    }

    public override void OnPhysicsUpdate()
    {
        base.OnPhysicsUpdate();
        Rotate();
        Move();
    }

    private void Rotate()
    {
        if (mMovement < 0f)
        {
            mTransform.rotation = Quaternion.Euler(
                0f,
                180f,
                0f
            );
        }
        else
        {
            mTransform.rotation = Quaternion.Euler(
                0f,
                0f,
                0f
            );
        }
    }

    private void Move()
    {
        float targetSpeed = mMovement * mController.moveSpeed;
        float speedDif = targetSpeed - mRigidBody.velocity.x;
        float accelRate = Mathf.Abs(
            targetSpeed) > 0.01f ? mController.accel : mController.deccel;
        float movement = Mathf.Pow(
            accelRate * Mathf.Abs(speedDif),
            mController.speedExp
        ) * Mathf.Sign(speedDif);

        mRigidBody.AddForce(movement * Vector2.right);
    }

    private void Fire()
    {
        mFireballPoint.GetComponent<ParticleSystem>().Play(); // ejecutamos PS
        GameObject obj = GameObject.Instantiate(mController.fireball, mFireballPoint);
        obj.transform.parent = null;

    }
}
