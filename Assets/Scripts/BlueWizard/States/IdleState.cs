using UnityEngine;

public class IdleState : State<BlueWizardController>
{
    private float mMovement;

    public IdleState(BlueWizardController mController, 
        FiniteStateMachine<BlueWizardController> mFSM) : base(mController, mFSM)
    {

    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("Entrando al estado IDLESTATE");
    }

    public override void OnExit()
    {
        base.OnExit();
        Debug.Log("Saliendo del estado IDLESTATE");
    }

    public override void OnHandleInput()
    {
        base.OnHandleInput();
        mMovement = Input.GetAxis("Horizontal");
    }

    public override void OnLogicUpdate()
    {
        base.OnLogicUpdate();
        if (mMovement != 0f)
        {
            mFSM.ChangeState(mController.RunningState);
        }
    }

    public override void OnPhysicsUpdate()
    {
        base.OnPhysicsUpdate();
    }
}
