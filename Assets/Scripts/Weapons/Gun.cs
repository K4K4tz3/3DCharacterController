using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // GameObject's
    [SerializeField] private GameObject _user;
    [SerializeField] private WeaponLibrary _weaponLibrary;
    [SerializeField] private Weapon _weapon;
    [SerializeField] public List<AudioSource> _shotSound;
    [SerializeField] private AudioSource _magSwapSound;

    // Weapons stats
    private bool _shootAction;
    private bool _canShoot;
    
    // Magazine
    private int _magazineMaxAmmount;
    private int _magazineCurrentAmmount = 30;
    private int _currentSound;

    #region Getter & Setters
    public bool CanShoot { get { return _canShoot; } set { _canShoot = value; } }
    #endregion
    private void Awake()
    {
        if (_weaponLibrary == null) _weaponLibrary = Resources.Load("Weapons/WeaponLibrary") as WeaponLibrary;
        if (_weapon == null) _weapon = _weaponLibrary.GetWeaponByType(gameObject.name);
        if (_shotSound == null) Debug.Log("Weapon doesnt have a shot sound");
    }
    public void Update()
    {
        //ShootCycle();
        //_canShoot = (_shootAction && _magazineCurrentAmmount != 0) ? true : false;
    }
    public IEnumerator Shoot()
    {
        while (_canShoot && _magazineCurrentAmmount > 0)
        {
            Instantiate(_weapon.correspondingPrefab, transform.position, transform.rotation, _user.transform);
            _magazineCurrentAmmount--;
            _shotSound[_currentSound].Play();
            _currentSound++;
            yield return new WaitForSeconds(60 / _weapon.rateOfFire);
        }
    }
    public void Reload()
    {
        _magSwapSound.Play();
        _magazineCurrentAmmount = 30;
        _currentSound = 0;
    }
}
