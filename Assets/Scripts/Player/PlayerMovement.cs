using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput), typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private PlayerInput _playerInput;
	[SerializeField] private CharacterController _characterController;
	[SerializeField] private float _speed;
	private Vector3 _moveDirection;
	private EPlayerState _playerState;
	private AnimationController _animationController;
	private Transform _cameraTransform;
	

	private void Start()
	{
		if (!_playerInput)
			_playerInput = GetComponent<PlayerInput>();
		if (!_characterController)
			_characterController = GetComponent<CharacterController>();

		_cameraTransform = Camera.main.transform;
		_animationController = new AnimationController(gameObject);
	}

	private void FixedUpdate()
	{
		SetMoveDirection();
		MovePlayer();
		_playerState = _moveDirection.Equals(Vector2.zero) ? EPlayerState.Idle : EPlayerState.Move;
		_animationController.PlayAnimClip(_playerState.ToString());
	}

	private void SetMoveDirection()
	{
		var actionValue = _playerInput.actions["Move"].ReadValue<Vector2>();
		_moveDirection = new Vector3(actionValue.x, 0, actionValue.y);
	}

	private void MovePlayer()
	{
		if (_moveDirection.Equals(Vector3.zero))
			return;

		var moveDirection =
			_cameraTransform.forward * _moveDirection.z+
			_cameraTransform.right * _moveDirection.x;
		moveDirection.y = 0;
		
		transform.forward = moveDirection;
		_characterController.Move(moveDirection * _speed * Time.deltaTime);
	}
}