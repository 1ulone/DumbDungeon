using UnityEngine;
using System;

public enum gunType 
{
	handgun,
	auto,
	spread,
	sniper
}


[CreateAssetMenu(fileName = "new Gun",menuName = "data/Gun")]
public class Gun : ScriptableObject
{
	public gunType id;
	public int damage = 10;
	public int count = 1;
	public int ammo = 12;
	public float fireRate = 10;
	public float reloadTime = 1;
	public Sprite sprite;

	private Vector2 dir;
	private Transform position;
	private Transform parent;

	public Action bulletBehaviour(Vector2 dir, Transform position, Transform parent)
	{
		this.dir = dir;
		this.position = position;
		this.parent = parent;

		return () => 
		{
			switch(id)
			{
				case gunType.handgun: { StraightShot(); } break;
				case gunType.auto: { StraightShot(); } break;
				case gunType.spread: { Spread(); } break;
				case gunType.sniper: { StraightShot(); } break;
			}
		};
	}

	private void StraightShot()
	{
		Bullet bullet = Pool.instances.Create("bullet", position.position, parent.rotation).GetComponent<Bullet>();
		bullet.MoveBullet(dir);
	}

	private void Spread() {}
}


