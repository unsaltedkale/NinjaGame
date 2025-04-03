using UnityEngine;

// Base class for all player states
public abstract class PlayerBaseState
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public abstract void Enter();
    public abstract void Tick(float deltaTime);
    public abstract void Exit();
}

// The main state machine component
public class PlayerStateMachine : MonoBehaviour
{
    [field: SerializeField] public float MoveSpeed { get; private set; } = 5f; // Example speed

    private PlayerBaseState currentState;

    // Concrete states
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    // ... other states

    // Component References (Example)
    public Rigidbody2D RB { get; private set; }
    public Animator Animator { get; private set; }
    // Add InputReader reference if using one


    private void Awake()
    {
        // Get Components
        RB = GetComponent<Rigidbody2D>();
        Animator = GetComponentInChildren<Animator>(); // Or GetComponent<Animator>()

        // Initialize concrete states
        IdleState = new PlayerIdleState(this);
        MoveState = new PlayerMoveState(this);
        // ... initialize other states
    }

    private void Start()
    {
        // Set the initial state
        SwitchState(IdleState); // Start in Idle state
    }

    private void Update()
    {
        currentState?.Tick(Time.deltaTime);
    }

    public void SwitchState(PlayerBaseState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    // Example Input Method (Replace with your actual input system logic)
    public Vector2 GetMovementInput()
    {
        // Replace this with Input.GetAxis, Input System, etc.
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        return new Vector2(horizontal, vertical).normalized;
    }
}

// --- Concrete States ---

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        // Stop movement if any residual velocity
        stateMachine.RB.velocity = Vector2.zero;
        // Play Idle Animation (Example)
        // stateMachine.Animator.Play("IdleAnimationName");
        Debug.Log("Entering Idle State");
    }

    public override void Tick(float deltaTime)
    {
        // Check for movement input
        if (stateMachine.GetMovementInput() != Vector2.zero)
        {
            stateMachine.SwitchState(stateMachine.MoveState);
        }
    }

    public override void Exit()
    {
         Debug.Log("Exiting Idle State");
    }
}

public class PlayerMoveState : PlayerBaseState
{
    public PlayerMoveState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        // Play Move Animation (Example)
        // stateMachine.Animator.Play("MoveAnimationName");
         Debug.Log("Entering Move State");
    }

    public override void Tick(float deltaTime)
    {
        Vector2 moveInput = stateMachine.GetMovementInput();

        // Check if should transition back to Idle
        if (moveInput == Vector2.zero)
        {
            stateMachine.SwitchState(stateMachine.IdleState);
            return; // Exit early after state switch
        }

        // Apply movement
        Move(moveInput, deltaTime);

        // Update animation based on movement direction (Example)
        // stateMachine.Animator.SetFloat("Horizontal", moveInput.x);
        // stateMachine.Animator.SetFloat("Vertical", moveInput.y);
    }

    public override void Exit()
    {
        // Ensure velocity is zeroed when exiting move state if desired
        // stateMachine.RB.velocity = Vector2.zero;
         Debug.Log("Exiting Move State");
    }

    private void Move(Vector2 direction, float deltaTime)
    {
        // Simple Rigidbody velocity movement
        stateMachine.RB.velocity = direction * stateMachine.MoveSpeed;

        // Optional: Face movement direction (for top-down or side-scroller)
        // if (direction.x != 0) {
        //     stateMachine.transform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);
        // }
    }
}