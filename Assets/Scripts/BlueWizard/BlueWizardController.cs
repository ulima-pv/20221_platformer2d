using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueWizardController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float accel;
    public float deccel;
    public float speedExp;

    public State<BlueWizardController> IdleState { get; private set; }
    public State<BlueWizardController> RunningState { get; private set; }

    private FiniteStateMachine<BlueWizardController> mFSM = 
        new FiniteStateMachine<BlueWizardController>();

    private void Start()
    {
        IdleState = new IdleState(this, mFSM);
        RunningState = new RunningState(this, mFSM);

        // Iniciamos la maquina de estados con el estado por defecto (inicial)
        mFSM.Start(IdleState);
    }

    private void Update()
    {
        mFSM.GetCurrentState().OnHandleInput();
        mFSM.GetCurrentState().OnLogicUpdate();
    }

    private void FixedUpdate()
    {
        mFSM.GetCurrentState().OnPhysicsUpdate();
    }
}
