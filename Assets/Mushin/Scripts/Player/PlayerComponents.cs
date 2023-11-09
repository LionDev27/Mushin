using UnityEngine;

public class PlayerComponents : MonoBehaviour
{
    public PlayerInputs PlayerInputController { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public PlayerLevel PlayerLevel { get; private set; }
    public PlayerMovement PlayerMovement { get; private set; }
    public PlayerDash PlayerDash { get; private set; }
    public PlayerDamageable PlayerDamageable { get; private set; }

    public static PlayerComponents Instance;
    
    protected virtual void Awake()
    {
        if (!Instance)
            Instance = this;
        
        PlayerInputController = GetComponent<PlayerInputs>();
        Rigidbody = GetComponent<Rigidbody2D>();
        PlayerLevel = GetComponent<PlayerLevel>();
        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerDash = GetComponent<PlayerDash>();
        PlayerDamageable = GetComponent<PlayerDamageable>();
    }
}
