using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // GameObject's
    [SerializeField] private GameObject _user;
    [SerializeField] private WeaponLibrary _weaponLibrary;
    [SerializeField] private Weapon _weapon;

    // Weapons stats
    private bool _shootAction;
    private bool _canShoot;
    
    // Magazine
    private int _magazineMaxAmmount;
    private int _magazineCurrentAmmount = 30;

    #region Getter & Setters
    public bool CanShoot { get { return _canShoot; } set { _canShoot = value; } }
    #endregion
    private void Awake()
    {
        if (_weaponLibrary == null) _weaponLibrary = Resources.Load("Weapons/WeaponLibrary") as WeaponLibrary;
        if (_weapon == null) _weapon = _weaponLibrary.GetWeaponByType(gameObject.name);
    }
    public void Update()
    {
        ShootCycle();
        _canShoot = (_shootAction && _magazineCurrentAmmount != 0) ? true : false;
    }
    IEnumerator ShootCycle()
    {
        if(_canShoot) Instantiate(_weapon.correspondingPrefab, transform.position, transform.rotation, _user.transform);
        yield return new WaitForSeconds(60 / _weapon.rateOfFire);
    }
}
