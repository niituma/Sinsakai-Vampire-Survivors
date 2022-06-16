using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RigidbodyVelocity
{
    public Vector3 _velocity;
    public float _angularVeloccity;
    public RigidbodyVelocity(Rigidbody2D rigidbody)
    {
        _velocity = rigidbody.velocity;
        _angularVeloccity = rigidbody.angularVelocity;
    }
}

public class Pauseble : MonoBehaviour
{
    /// <summary>/// ��������GameObject/// </summary>
    [SerializeField] GameObject[] _ignoreGameObjects;
    /// <summary>/// �|�[�Y��Ԃ��ύX���ꂽ�u�Ԃ𒲂ׂ邽�߁A�O��̃|�[�Y�󋵂��L�^���Ă���/// </summary>
    bool prevPausing;
    /// <summary>/// Rigidbody�̃|�[�Y�O�̑��x�̔z��/// </summary>
    RigidbodyVelocity[] _rigidbodyVelocities;
    /// <summary>/// �|�[�Y����Rigidbody�̔z��/// </summary>
    Rigidbody2D[] _pausingRigidbodies;
    /// <summary>/// �|�[�Y����Animator�̔z��/// </summary>
    Animator[] _pausingAnimators;
    /// <summary>/// �|�[�Y����MonoBehaviour�̔z��/// </summary>
    MonoBehaviour[] _pausingMonoBehaviours;
    ParticleSystem[] _particles;

    void Update()
    {
        // �|�[�Y��Ԃ��ύX����Ă�����APause/Resume���Ăяo���B
        if (prevPausing != GameManager.Instance._isPause)
        {
            if (GameManager.Instance._isPause)
            {
                Pause();
                Time.timeScale = 0f;
            }
            else if (!GameManager.Instance._isPause)
            {
                Time.timeScale = 1f;
                Resume();
            }
            prevPausing = GameManager.Instance._isPause;
        }
    }
    /// <summary>///���f/// </summary>
    public void Pause()
    {
        // Rigidbody�̒�~
        // �q�v�f����A�X���[�v���łȂ��AIgnoreGameObjects�Ɋ܂܂�Ă��Ȃ�Rigidbody�𒊏o
        Predicate<Rigidbody2D> rigidbodyPredicate =
            obj => !obj.IsSleeping() &&
                   Array.FindIndex(_ignoreGameObjects, gameObject => gameObject == obj.gameObject) < 0;
        _pausingRigidbodies = Array.FindAll(transform.GetComponentsInChildren<Rigidbody2D>(), rigidbodyPredicate);
        _rigidbodyVelocities = new RigidbodyVelocity[_pausingRigidbodies.Length];
        Predicate<Animator> animatorPredicate =
            obj => Array.FindIndex(_ignoreGameObjects, gameObject => gameObject == obj.gameObject) < 0;
        _pausingAnimators = Array.FindAll(transform.GetComponentsInChildren<Animator>(), animatorPredicate);
        Predicate<ParticleSystem> particlesPredicate =
            obj => Array.FindIndex(_ignoreGameObjects, gameObject => gameObject == obj.gameObject) < 0;
        _particles = Array.FindAll(transform.GetComponentsInChildren<ParticleSystem>(), particlesPredicate);
        for (int i = 0; i < _pausingRigidbodies.Length; i++)
        {
            // ���x�A�p���x���ۑ����Ă���
            _rigidbodyVelocities[i] = new RigidbodyVelocity(_pausingRigidbodies[i]);
            _pausingRigidbodies[i].Sleep();
        }
        for (int i = 0; i < _pausingAnimators.Length; i++)
        {
            _pausingAnimators[i].speed = 0;
        }
        for (int i = 0; i < _particles.Length; i++)
        {
            _particles[i].Pause();
        }

        // MonoBehaviour�̒�~
        // �q�v�f����A�L�������̃C���X�^���X�łȂ����́AIgnoreGameObjects�Ɋ܂܂�Ă��Ȃ�MonoBehaviour�𒊏o
        Predicate<MonoBehaviour> monoBehaviourPredicate =
            obj => obj.enabled &&
                   obj != this &&
                   Array.FindIndex(_ignoreGameObjects, gameObject => gameObject == obj.gameObject) < 0;
        _pausingMonoBehaviours = Array.FindAll(transform.GetComponentsInChildren<MonoBehaviour>(), monoBehaviourPredicate);
        foreach (var monoBehaviour in _pausingMonoBehaviours)
        {
            monoBehaviour.enabled = false;
        }

    }

    /// <summary>/// �ĊJ/// </summary>
    public void Resume()
    {
        // Rigidbody�̍ĊJ
        for (int i = 0; i < _pausingRigidbodies.Length; i++)
        {
            _pausingRigidbodies[i].WakeUp();
            _pausingRigidbodies[i].velocity = _rigidbodyVelocities[i]._velocity;
            _pausingRigidbodies[i].angularVelocity = _rigidbodyVelocities[i]._angularVeloccity;
        }
        for (int i = 0; i < _pausingAnimators.Length; i++)
        {
            _pausingAnimators[i].speed = 1;
        }
        for (int i = 0; i < _particles.Length; i++)
        {
            _particles[i].Play();
        }

        // MonoBehaviour�̍ĊJ
        foreach (var monoBehaviour in _pausingMonoBehaviours)
        {
            monoBehaviour.enabled = true;
        }
    }
}