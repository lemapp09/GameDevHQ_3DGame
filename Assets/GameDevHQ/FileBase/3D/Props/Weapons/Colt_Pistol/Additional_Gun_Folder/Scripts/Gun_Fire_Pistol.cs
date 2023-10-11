using System.Collections;
using System.Collections.Generic;
using LemApperson_3DGame;
using LemApperson_3DGame.Manager;
using UnityEngine;

public class Gun_Fire_Pistol : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _smoke;

    [SerializeField]
    private ParticleSystem _bulletCasing;

    [SerializeField]
    private ParticleSystem _muzzleFlashSide;

    [SerializeField]
    private ParticleSystem _Muzzle_Flash_Front;

    private Animator _anim;

    [SerializeField]
    private AudioClip _gunShotAudioClip;

    [SerializeField]
    private AudioSource _audioSource;

    public bool FullAuto;
    private InputActions _inputActions;
    private Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f); // center of the screen
    [Header("Shooting")]
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _bloodSplatter;
    
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }
    
    private void Awake() {
        _inputActions = new InputActions();
        _inputActions.Player.Shoot.started += ctx => ShootGunStart();
        _inputActions.Player.Shoot.performed += ctx => ShootGunStart();
        _inputActions.Player.Shoot.canceled += ctx => ShootGunEnd();
    }

    private void ShootGunStart()
    {
        // Debug.Log("Shoot gun");
        if (FullAuto == false)
        {
            _anim.SetTrigger("Fire");
            ShootGun();
        }

        if (FullAuto == true)
        {
            _anim.SetBool("Automatic_Fire", true);
            ShootGun();
        }
    }

    private void ShootGunEnd()
    {
        if (FullAuto == true)
        {
            _anim.SetBool("Automatic_Fire", false);
        }

        if (FullAuto == false)
        {
            _anim.SetBool("Fire", false);
        }
    }

    public void ShootGun() {
        Ray ray = _camera.ViewportPointToRay(rayOrigin);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Enemy"))) {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                AudioManager.Instance.SFX(1);
                if (hit.collider.GetComponent<Target>() != null) {
                    hit.collider.gameObject.GetComponent<Target>().ChangeColor();
                }
                Health health = hit.collider.gameObject.GetComponent<Health>();
                if (health != null) {
                    health.Damage(50);
                    GameObject bloodSplatter = Instantiate(_bloodSplatter, hit.point, Quaternion.LookRotation(hit.normal));
                    bloodSplatter.transform.parent = hit.collider.gameObject.transform;
                }
            }
        }
    }


    public void FireGunParticles()
    {
        // Debug.Log("Fired gun particles");
        // _smoke.Play();
        _bulletCasing.Play();
        _muzzleFlashSide.Play();
        _Muzzle_Flash_Front.Play();
        GunFireAudio();
    }

    public void GunFireAudio()
    {
        _audioSource.pitch = Random.Range(0.9f, 1.1f);
        _audioSource.PlayOneShot(_gunShotAudioClip);
    }

    private void OnEnable() {
        _inputActions.Player.Shoot.Enable();
    }

    private void OnDisable() {
        _inputActions.Player.Shoot.Disable();
    }
}
