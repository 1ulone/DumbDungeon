using UnityEngine;
using System;

[CreateAssetMenu(fileName = "new Gun",menuName = "data/Gun")]
public class Gun : ScriptableObject
{
}

[System.Serializable]
public class GunCombo 
{
	public Gun[] comboLine;
	public Action passiveEffects;
}
