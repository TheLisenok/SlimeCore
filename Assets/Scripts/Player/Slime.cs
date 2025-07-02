using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.U2D;

/// <summary>
/// This class with Slime parameters snd methods
/// </summary>

public class Slime : ElementHolder
{
    #region Singletone
    public static Slime Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    #endregion


    [SerializeField] private int health = 3; // Здоровье слайма
    [SerializeField] private int maxHealth = 3; // Максимальное здоровье
    [SerializeField] private float moveSpeed = 5f; // Скорость передвижения
    [SerializeField] private float jumpForce = 10f; // Сила прыжка
    [SerializeField] private ElementData defaultElement; // Дефолтная стихия слайма

    [SerializeField] private SpriteShapeRenderer spriteShapeRenderer;
    [SerializeField] private Light2D slimeLight;
    [SerializeField] private SlimeSoundClips soundClips;

    //public Element currentElement;

    private AudioSource _damage;

    private void Start()
    {
        if (defaultElement != null)
        {
            currentElement = CreateElementFromData(defaultElement);
            Debug.Log($"Слайм начально имеет элемент {currentElement.Name}");

            spriteShapeRenderer.color = currentElement.SlimeEdgeColor; // Меняем цвет слайма
            slimeLight.color = currentElement.SlimeEdgeColor; // Меняем цвет света вокруг слайма
        }
        else
        {
            Debug.LogWarning("Default ElementData не задано!");
        }

        // Обновляем UI хп
        HealthUI.Instance.AddHP(health);

        _damage = soundClips.Damage;
    }

    public void SetSlimeElementForced(ElementType elementType)
    {
        ChangeElement(ElementManager.Instance.CreateElementFromType(elementType));
    }

    // Метод для изменения стихии
    public void ChangeElement(Element newElement)
    {
        if (newElement != null)
        {
            Element result = currentElement.ReactWith(newElement); // Реакция текущего элемента с новым

            currentElement = result; // Устанавливаем новый элемент после реакции
            currentElement.ApplyEffect(this); // Применяем эффект

            Debug.Log($"Слайм изменил элемент на {currentElement.Name}");

            spriteShapeRenderer.color = currentElement.SlimeEdgeColor; // Меняем цвет слайма
            slimeLight.color = currentElement.SlimeEdgeColor; // Меняем цвет света вокруг слайма

            // TODO: change this to normal
            gameObject.GetComponent<SlimeController>().SoftUpdate(currentElement.Data.SoftnessCoefficient); // Change softness Slime to element softness
        }
        else
        {
            Debug.LogWarning("Переданный Element пустой!");
        }
    }

    // Фабричный метод для создания элементов из данных
    private Element CreateElementFromData(ElementData elementData)
    {
        switch (elementData.Type)
        {
            case ElementType.Fire:
                return new FireElement(elementData);
            case ElementType.Water:
                return new WaterElement(elementData);
            case ElementType.Air: 
                return new AirElement(elementData);
            case ElementType.Ice: 
                return new IceElement(elementData);
            case ElementType.Electricity: 
                return new ElectricityElement(elementData);
            default:
                return new EmptyElement(elementData); // Если данных нет, создаем пустой элемент
        }
    }

    // Метод для получения урона
    public void TakeDamage(int damage)
    {
        health -= damage;

        HealthUI.Instance.RemoveHP(damage);
        

        if (health <= 0)
        {
            Die();
        }
        else
        {
            EmotionManager.Instance.ActivateEmotion(EmotionType.Uhgh); // Реакция слайма на урон
            _damage.Play();
        }
    }

    // Метод для лечения
    public void Heal(int amount)
    {
        health = Mathf.Min(health + amount, maxHealth);
    }

    // Метод для смерти слайма
    private void Die()
    {
        Debug.Log("Slime died!");
        EmotionManager.Instance.ActivateEmotion(EmotionType.Dead);

        this.gameObject.GetComponent<SlimeController>().CanMove = false; // TODO: если будет много ссылок на скрипт управления, то сделай адекватнее

        GameManager.Instance.RestartWithDelay(3f);

    }

    #region Getters
    public float getMoveSpeed() // Вроде как на шарпах низя делать константные методы
    {
        return moveSpeed;
    }
    public float getJumpForce()
    {
        return jumpForce;
    }
    
    public ElementType getCurrentElement()
    {
        return currentElement.Data.Type;
    }
    #endregion
}