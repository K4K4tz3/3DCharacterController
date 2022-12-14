using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/New Weapon")]
public class Weapon : ScriptableObject
{
    public WeaponTypes type;
    public float weight; // kilogram

    public AmmunitionTypes amunition;
    public GameObject correspondingPrefab;

    public float rateOfFire; // per minute
    public float muzzelVelocity; // meter per second
}

[CreateAssetMenu(menuName = "Scriptable Objects/New Weapon Library")]
public class WeaponLibrary : ScriptableObject
{
    public Weapon[] weapons;

    public Weapon GetWeaponByType(string type)
    {
        foreach(Weapon weapon in weapons) if(weapon.type.ToString() == type) return weapon;
        return null;
    }
}
