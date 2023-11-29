using UnityEngine;

namespace Utils
{
	public class AnimationController
	{
		private string _currentAnimation;
		public Animator Animator { get; private set; }
    
		public AnimationController(GameObject objectWithAnimator)
		{
			if (objectWithAnimator.TryGetComponent(out Animator animator))
				Animator = animator;
			else		
				Debug.LogError($"Cannot find animator on {objectWithAnimator.name}", objectWithAnimator);
		}

		public void PlayAnimClip(string newAnimation)
		{
			if (_currentAnimation == newAnimation)
				return;

			Animator.Play(newAnimation);
			_currentAnimation = newAnimation;
		}
	}
}