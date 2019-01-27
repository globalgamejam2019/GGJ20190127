﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

/*
 *  玩家管理类
 *  by mingkai.zheng
 */
public enum playerStatus
{
    Run = 0,
    JumpStart = 1,
    JumpEnd =  2,
    JumpAuto = 3,
    SrounchStart = 4,
    SrounchEnd = 5,
    Idle = 6,
}

public enum movingDirection
{
    None = 0,
    Left = 1,
    Right = 2,
}
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerManager : MonoBehaviour
{
    public PlayerData _playerData;

    [SerializeField]
    private int _firstBlood = 0;
    [SerializeField]
    private float _jumpSpeed = 1.0f;
    [SerializeField]
    private float _jumpDownForce = 1.0f;
    [SerializeField]
    private float _jumpMovingForce = 1.0f;
    [SerializeField]
    private float _MovingSpeed = 1.0f;
    [SerializeField]
    public List<AudioClip> playerClipList;

    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private Dictionary<int, int> _animatorHashIdList;
    private Collider2D _collider2D;
    private SpriteRenderer _spriteRenderer;
    private AudioSource _audioSource;

    private static movingDirection _movingSatus = movingDirection.None;
    private static bool _jumping = false;
    private static bool _scrunching = false;


    void Awake()
    {
        _playerData = new PlayerData(_firstBlood);
        _animator = GetComponent<Animator>();
        _collider2D = GetComponent<Collider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();

        _animatorHashIdList = new Dictionary<int, int>();
        _animatorHashIdList.Add((int) playerStatus.Idle, Animator.StringToHash("Idle"));
        _animatorHashIdList.Add((int) playerStatus.JumpStart, Animator.StringToHash("JumpStart"));
        _animatorHashIdList.Add((int) playerStatus.JumpEnd, Animator.StringToHash("JumpEnd"));
        _animatorHashIdList.Add((int) playerStatus.JumpAuto, Animator.StringToHash("JumpAuto"));
        _animatorHashIdList.Add((int) playerStatus.SrounchStart, Animator.StringToHash("SrounchStart"));
        _animatorHashIdList.Add((int) playerStatus.SrounchEnd, Animator.StringToHash("SrounchEnd"));
        _animatorHashIdList.Add((int) playerStatus.Run, Animator.StringToHash("Run"));

        //玩家音效
        playerClipList = new List<AudioClip>();
        for (int i = 0; i < 2; i++)
        {
            playerClipList.Add(Resources.Load<AudioClip>("Audio/PlayerAudio/" + i));
        }
        UpdateSomethings();
    }

    void Update()
    {
        //controller 
        UpdateControllerListen();
    }

    #region Controller

    private void UpdateControllerListen()
    {
        //跳 + 
        if (_jumping)
        {
            //停止跑动
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                _movingSatus = movingDirection.None;
                SetupMoving(false);
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                _movingSatus = movingDirection.None;
                SetupMoving(false);
            }
            print("跳跃过程中");
            return;
        }

        //蹲 + 
        if (_scrunching)
        {
            if (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.RightControl) &&
                !Input.GetKey(KeyCode.DownArrow))
            {
                _scrunching = false;
                SetupScrunchEnd();
                //不蹲
            }
            else
            {
                _scrunching = true;
                //保持蹲
                return;
            }
        }
        else//普通蹲
        {
            if (Input.GetKey(KeyCode.LeftControl) ||
                Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.DownArrow))
            {
                _scrunching = true;
                SetupScrunchStart();
                //开始蹲
                return;
            }
        }

        //走 + 跳/蹲
        if (_movingSatus != movingDirection.None)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                _jumping = true;
                SetupJumpStart(playerStatus.JumpStart);
                //开始斜跳
                return;
            }

            if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl) ||  Input.GetKeyDown(KeyCode.DownArrow))
            {
                _scrunching = true;
                SetupScrunchStart();
                //开始蹲
                return;
            }
        }


        //普通跳
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            _jumping = true;
            SetupJumpStart(playerStatus.JumpAuto);
            //普通跳
            return;
        }

        //普通走
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _movingSatus = movingDirection.Left;
            SetupMoving(true);
            if (_spriteRenderer.sprite == null && transform.localScale.x > 0)
            {
                print("旋转");
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            //向左跑
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            _movingSatus = movingDirection.None;
            SetupMoving(false);
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            _movingSatus = movingDirection.Right;
            SetupMoving(true);
            if (_spriteRenderer.sprite == null && transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            //向右跑
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            _movingSatus = movingDirection.None;
            SetupMoving(false);
        }
        return;
    }

    private void SetAnimatorTrigger(playerStatus playerStatus)
    {
        _animator.SetTrigger(_animatorHashIdList[(int)playerStatus]);
    }

    private void SetAnimatorBool(playerStatus playerStatus, bool active)
    {
        _animator.SetBool(_animatorHashIdList[(int) playerStatus], active);
    }

    private void SetupJumpStart(playerStatus jumpStatus)
    {
        if (_movingSatus == movingDirection.Left)
        {
            //SetAnimatorBool(playerStatus.Run, false);
            _rigidbody2D.AddForce(-transform.right * _jumpMovingForce);
        }
        else if (_movingSatus == movingDirection.Right)
        {
            //SetAnimatorBool(playerStatus.Run, false);
            _rigidbody2D.AddForce(transform.right * _jumpMovingForce);
        }
        //_rigidbody2D.AddForce(transform.up * _jumpSpeed);
        _rigidbody2D.velocity = transform.up * _jumpSpeed;
        SetAnimatorTrigger(jumpStatus);
        SetupAudioClip(playerClipList[1]);
    }

    private void SetupJumpEnd()
    {
        if (_jumping)
        {
            _jumping = false;
            SetAnimatorTrigger(playerStatus.JumpEnd);
            _rigidbody2D.velocity = Vector2.zero;
        }
    }

    private void SetupMoving(bool active)
    {
        SetAnimatorBool(playerStatus.Run, active);

        if (active)
        {
            if (_movingSatus == movingDirection.Left)
            {
                _spriteRenderer.flipX = true;

                _rigidbody2D.MovePosition(_rigidbody2D.position + _MovingSpeed * Vector2.left);
            }
            else if (_movingSatus == movingDirection.Right)
            {
                _spriteRenderer.flipX = false;

                _rigidbody2D.MovePosition(_rigidbody2D.position + _MovingSpeed * Vector2.right);
            }
        }
    }

    private void SetupScrunchStart()
    {
        if (_movingSatus != movingDirection.None)
            SetAnimatorBool(playerStatus.Run, false);
        SetAnimatorTrigger(playerStatus.SrounchStart);
        SetupAudioClip(playerClipList[0]);
    }

    private void SetupScrunchEnd()
    {
        _scrunching = false;
        SetAnimatorTrigger(playerStatus.SrounchEnd);
    }

    #endregion

    #region Collider

    void OnCollisionEnter2D(Collision2D Collision2D)
    {
        //接触到地板
        if (Collision2D.transform.tag == "floor")
        {
            SetupJumpEnd();
            return;
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        //接触到门

        //接触到其他物品
        GoodEffect goodEffect = GoodEffect.None;
        if (collider2D.tag == "Buff")
        {
            goodEffect = GoodEffect.Buff;
        }
        else if (collider2D.tag == "DeBuff")
        {
            goodEffect = GoodEffect.Debuff;
        }
        _playerData.blood += collider2D.GetComponent<Good>().GetInteractiveGood(goodEffect);
        print(_playerData.blood);
        if (_playerData.blood > 100) _playerData.blood = 100;

        if (!Singleton<GameManager>.Instance.isHasChangeAvatar && collider2D.name == "ball")
        {
            Singleton<GameManager>.Instance.isHasChangeAvatar = true;
            Singleton<GameManager>.Instance.SetBallPlayer();
        }
        else if(!Singleton<GameManager>.Instance.isHasChangeAvatar && collider2D.name == "shoubing")
        {
            Singleton<GameManager>.Instance.isHasChangeAvatar = true;
            Singleton<GameManager>.Instance.SetVideoPlayer();;
        }

        UpdateSomethings();
    }

    private void UpdateSomethings()
    {
        //刷新血条UI
        Singleton<GameManager>.Instance.UpdateBloodSlider(PlayerData.MAXBLOOD, _playerData.blood);

        //第一次BloodMax，更改背景音乐
        if (!Singleton<GameManager>.Instance.firstBloodMax && _playerData.blood >= 100)
        {
            Singleton<GameManager>.Instance.firstBloodMax = true;
            Singleton<GameManager>.Instance.SetAudioBgm(AudioSoundType.bloodMax);
        }

        if (_playerData.blood < 0)
        {
            Singleton<GameManager>.Instance.GameOver();
        }
    }

    #endregion


    #region Animation Callback 

    public void JumpSpeedUpDown()
    {
        _rigidbody2D.AddForce(-transform.up * _jumpDownForce);
        return;
    }

    #endregion

    private void SetupAudioClip(AudioClip audioClip)
    {
        _audioSource.Stop();
        _audioSource.clip = audioClip;
        _audioSource.Play();
        _audioSource.loop = false;
    }
}
