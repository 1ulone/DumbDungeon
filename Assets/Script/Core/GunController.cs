using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{
	[Header("Constant Variable")]
	private List<GunCombo> combos = new List<GunCombo>() 
	{
		new GunCombo() 
		{ 
			comboLine = new gunType[] { gunType.handgun, gunType.handgun, gunType.auto }, 
			passiveEffects = () => 
			{
				Debug.Log("A");
			}
		},
		new GunCombo() 
		{ 
			comboLine = new gunType[] { gunType.auto, gunType.auto, gunType.auto }, 
			passiveEffects = () => 
			{
				Debug.Log("B");
			}
		},
		new GunCombo() 
		{ 
			comboLine = new gunType[] { gunType.handgun, gunType.spread, gunType.auto }, 
			passiveEffects = () => 
			{
				Debug.Log("C");
			}
		},
		new GunCombo() 
		{ 
			comboLine = new gunType[] { gunType.auto, gunType.spread, gunType.sniper }, 
			passiveEffects = () => 
			{
				Debug.Log("D");
			}
		},
	};

	[Header("Main Variable")]
	[SerializeField] private Transform gunParent;
	[SerializeField] private Transform gunTransform;
	[SerializeField] private Gun gun;

	private Queue<Gun> currentGunLine = new Queue<Gun>();
	private List<Action> passiveSkills = new List<Action>();

	private PlayerInput input;
	private InputAction shoot;

	private float fireRate;
	private float reloadTime;
	private int ammo;

	private void OnEnable()
	{
		input = FindFirstObjectByType<PlayerInput>();
		shoot = input.actions["Attack"];
		shoot.Enable();

		fireRate = 0;
		reloadTime = 0;
		ammo = gun.ammo;
	}

	private void Update()
	{	
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 difference = mousePosition - (Vector2)transform.position;
		float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
		gunParent.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

		if (fireRate > 0)
			fireRate -= Time.deltaTime;
		if (reloadTime > 0)
			reloadTime -= Time.deltaTime;
		Debug.Log(reloadTime);

		if (shoot.WasPressedThisFrame() && fireRate <= 0 && reloadTime <= 0)
		{
			gun.bulletBehaviour(difference.normalized, gunTransform, gunParent).Invoke();
			fireRate = gun.fireRate;
			ammo--;

			if (ammo <= 0)
			{
				ammo = gun.ammo;
				reloadTime = gun.reloadTime;
			}
		}
	}

	public void UpradeGun(Gun nGun)
	{
		gun = nGun;
		currentGunLine.Enqueue(gun);

		if (currentGunLine.Count == 3)
		{
			CheckForPassive();
			if (currentGunLine.Count > 3)
				currentGunLine.Dequeue();
		}
	}

	private void CheckForPassive()
	{
		foreach(GunCombo g in combos)
		{
			if (g.comboLine[0] == currentGunLine.ToArray()[0].id && 
				g.comboLine[1] == currentGunLine.ToArray()[1].id && 
				g.comboLine[2] == currentGunLine.ToArray()[2].id)
			{
				passiveSkills.Add(g.passiveEffects);
			}
		}
	}
}

[System.Serializable]
public class GunCombo 
{
	public gunType[] comboLine;
	public Action passiveEffects;
}
