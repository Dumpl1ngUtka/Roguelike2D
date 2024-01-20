using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    private Vector2 _movementDirection;
    private Vector2 _playerInputDirection;
    private int _availableJumpCount;
    private bool _isCanDodge;
    private bool _isGrounded;
    private Rigidbody2D _rigidbody;
    private CapsuleCollider2D _collider;
    private PlayerInputSystem _inputSystem;
    private Player _player;
    private Condition _currentCondition => _player.CurrentCondition;
    private PlayerParameters _stats => _player.Parameters;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CapsuleCollider2D>();
        _player = GetComponent<Player>();
        _player.ChangeCurrentCondition(Player.ConditionType.Move);
        _inputSystem = _player.InputSystem;
    }

    private void Update()
    {
        _playerInputDirection = _inputSystem.Movement.Move.ReadValue<Vector2>();
        DodgeInput();
        JumpInput();
        BlockInput();
    }

    private void BlockInput()
    {
        if (_inputSystem.Movement.Block.IsPressed() && _currentCondition.IsCanControlCharacter)
            _player.ChangeCurrentCondition(Player.ConditionType.Block);
        else if (_currentCondition.IsCanControlCharacter)
            _player.ChangeCurrentCondition(Player.ConditionType.Move);
    }

    private void JumpInput()
    {
        if (_inputSystem.Movement.Jump.triggered && _availableJumpCount > 0)
            Jump();
    }    
    
    private void DodgeInput()
    {
        if (_inputSystem.Movement.Dodge.triggered && _isCanDodge)
            StartCoroutine(Dodge());
    }

    private void FixedUpdate()
    {
        if (_currentCondition.IsCanControlCharacter)
            HorizontalMove();
        CheckCollision();
        if (_currentCondition.IsUsedGravity)
            Gravity();
        ApplyMovement();
    }

    private IEnumerator Dodge()
    {
        _isCanDodge = false;
        _player.ChangeCurrentCondition(Player.ConditionType.Dodge);
        float directionX = 0;
        if (_playerInputDirection.x > 0)
            directionX = _stats.DodgeSpeed;
        else if (_playerInputDirection.x < 0)
            directionX = -_stats.DodgeSpeed;
        _movementDirection.x = directionX;
        var timer = 0f;
        while (timer < _stats.DodgeTime)
        {
            timer += Time.deltaTime;
            _movementDirection.y = 0;
            yield return null;
        }
        _player.ChangeCurrentCondition(Player.ConditionType.Move);
    }

    private void HorizontalMove()
    {
        if (_playerInputDirection.x == 0)
        {
            var deceleration = _isGrounded ? _stats.GroundDeceleration : _stats.AirDeceleration;
            _movementDirection.x = Mathf.MoveTowards(_movementDirection.x, 0, deceleration * Time.fixedDeltaTime);
        }
        else
        {
            var maxSpeed = _currentCondition == _player.MoveCondition? _stats.MaxSpeed: _stats.MaxSpeed * _stats.BlockMaxSpeedModifier;
            _movementDirection.x = Mathf.MoveTowards(_movementDirection.x, _playerInputDirection.x * maxSpeed, _stats.Acceleration * Time.fixedDeltaTime);
        }
    }

    private void CheckCollision()
    {
        Physics2D.queriesStartInColliders = false;

        bool groundHit = Physics2D.CapsuleCast(_collider.bounds.center, _collider.size, _collider.direction, 0, Vector2.down, _stats.GrounderDistance, ~_player.GroundLayer);
        bool ceilingHit = Physics2D.CapsuleCast(_collider.bounds.center, _collider.size, _collider.direction, 0, Vector2.up, _stats.GrounderDistance, ~_player.GroundLayer);
        if (ceilingHit)
            _movementDirection.y = Mathf.Min(0, _movementDirection.y);

        if (!_isGrounded && groundHit)
        {
            _isGrounded = true;
            _availableJumpCount = 2;
        }
        else if (_isGrounded && !groundHit)
        {
            _isGrounded = false;
        }

        if (!_isCanDodge && groundHit)
        {
            _isCanDodge = true;
        }

        Physics2D.queriesStartInColliders = true;
    }

    private void Jump()
    {
        _movementDirection.y = _stats.JumpPower;
        _availableJumpCount--;
    }   

    private void Gravity()
    {
        if (_isGrounded && _movementDirection.y < 0)
        {
            _movementDirection.y = 0;
        }
        else
        {
            var inAirGravity = _stats.FallAcceleration;
            var maxFallSpeed = _playerInputDirection.y < 0 ?
                _stats.MaxFallSpeed * _stats.FastFallMaxSpeedModifier : _stats.MaxFallSpeed;
            _movementDirection.y = Mathf.MoveTowards(_movementDirection.y, -maxFallSpeed, inAirGravity * Time.fixedDeltaTime);
        }
    }

    private void ApplyMovement()
    {
        _rigidbody.velocity = _movementDirection;
    }
}
