using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	private bool _isAttacking;
	private Animator _animator;
	public int playerDamage = 1;
	PlayerInventory playerInventory;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
		playerInventory = GetComponent<PlayerInventory>();
	}

    private void Start()
    {
		//Save system
		if (SaveManager.instance.hasLoaded)
		{
			playerDamage = SaveManager.instance.activeSave.damage;
		}
		else
		{
			SaveManager.instance.activeSave.damage = playerDamage;
		}
	}

    private void LateUpdate()
	{
		// Animator
		if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
		{
			_isAttacking = true;
		}
		else
		{
			_isAttacking = false;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (_isAttacking == true)
		{
			if (collision.CompareTag("Enemy"))
			{
				collision.SendMessageUpwards("AddDamage", playerDamage);
				playerInventory.AddPotionFillAmount(5f);
			}
		}
	}
    
}
