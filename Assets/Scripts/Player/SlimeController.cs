using System;
using System.Collections;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public Slime slime; // Ссылка на компонент Slime
    public Rigidbody2D coreRigidbody; // Центральное тело слайма
    public Rigidbody2D[] outerPoints; // Внешние точки слайма
    [SerializeField] private SpringJoint2D[] springs; // Все пружины, связывающие точки
    [SerializeField] private SlimeSoundClips soundClips; // Звуки при передвижении

    public bool CanMove = true;

    private float moveSpeed;
    private float jumpForce;

    private AudioSource _moveAmbient; 
    private float _moveAmb_startVolume;
    private AudioSource _jumpSound;

    private void Start()
    {
        moveSpeed = slime.getMoveSpeed();
        jumpForce = slime.getJumpForce();

        _moveAmbient = soundClips.MoveAmbient;
        _moveAmb_startVolume = _moveAmbient.volume;
        _jumpSound = soundClips.Jump;
    }

    private void Update()
    {
        if (CanMove)
        {
            Move();
            Jump();
        }

        UpdateSoftness();
    }

    void Move()
    {
        // Движение центрального тела
        float horizontal = Input.GetAxis("Horizontal");
        Vector2 velocity = new Vector2(horizontal * moveSpeed, coreRigidbody.velocity.y);
        coreRigidbody.velocity = velocity;

        // Проверка движения и управление эмбиентом
        if (Mathf.Abs(horizontal) > 0.1f)
        {
            // Если персонаж движется и звук не играет, запускаем эмбиент
            if (!_moveAmbient.isPlaying)
            {
                _moveAmbient.time = UnityEngine.Random.Range(0f, _moveAmbient.clip.length); // Рандомный старт
                _moveAmbient.Play();
            }
        }
        else
        {
            // Если персонаж остановился, останавливаем эмбиент
            if (_moveAmbient.isPlaying)
            {
                StartCoroutine(FadeOutSound(_moveAmbient, 0.5f)); // 0.5 сек затухания
            }
        }
    }

    void Jump()
    {
        // Прыжок
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            coreRigidbody.velocity = new Vector2(coreRigidbody.velocity.x, jumpForce);
            _jumpSound.Play();
        }
    }

    void UpdateSoftness()
    {
        // Центрирование точек вокруг центрального тела
        foreach (var point in outerPoints)
        {
            Vector2 direction = (coreRigidbody.position - point.position).normalized;
            point.AddForce(direction * moveSpeed * 0.5f);
        }
    }

    bool IsGrounded()
    {
        // Простая проверка земли
        return Physics2D.Raycast(coreRigidbody.position, Vector2.down, 2f, LayerMask.GetMask("Ground"));
    }

    public void SoftUpdate(float softnessCoefficient) // 1 - стандартная мягкость, 0.5 - мягче, 2 - жёстче
    {
        // Регулировка жёсткости пружин
        foreach (var spring in springs)
        {
            if (spring != null)
            {
                spring.frequency = Mathf.Lerp(1f, 10f, softnessCoefficient); // Чем больше, тем жёстче
                spring.dampingRatio = Mathf.Lerp(0.1f, 0.9f, softnessCoefficient); // Гашение колебаний
            }
        }
    }


    IEnumerator FadeOutSound(AudioSource audioSource, float fadeTime)
    {
        while (audioSource.volume > 0)
        {
            audioSource.volume -= _moveAmb_startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = _moveAmb_startVolume; // Возвращаем громкость для следующего проигрывания
    }


}