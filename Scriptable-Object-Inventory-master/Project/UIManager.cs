using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    // Ссылки на экраны интерфейса
    public GameObject mainMenuScreen;
    public GameObject settingsScreen;
    public GameObject inventoryScreen;

    // Кнопки меню
    public Button newGameButton;
    public Button continueButton;
    public Button settingsButton;
    public Button exitButton;

    // Кнопки настроек
    public Button backToMenuButton;
    public Button bindKeysButton;


    // Кнопки инвентаря
    public Button closeInventoryButton;
    
    // Общий список элементов инвентаря
    public List<ItemCounter> itemCounters = new List<ItemCounter>();
    
    // Словарь счетчиков (для быстрого доступа по имени)
    private Dictionary<string, ItemCounter> itemCounterMap = new Dictionary<string, ItemCounter>();

    public void Start()
    {
        // Инициализация словаря счетчиков
        foreach (var counter in itemCounters)
        {
            if (!itemCounterMap.ContainsKey(counter.itemName))
                itemCounterMap.Add(counter.itemName, counter);
        }

        // Подписка на события кнопок меню
        newGameButton.onClick.AddListener(OnNewGameButtonClick);
        continueButton.onClick.AddListener(OnContinueButtonClick);
        settingsButton.onClick.AddListener(OnSettingsButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);

        // Подписка на события кнопок настроек
         backToMenuButton.onClick.AddListener(OnBackToMenuButtonClick);
         bindKeysButton.onClick.AddListener(OnBindKeysButtonClick);

        // Подписка на события кнопок инвентаря
         closeInventoryButton.onClick.AddListener(OnCloseInventoryButtonClick);

        // Изначально показываем только главное меню
        ShowScreen(mainMenuScreen);
    }
    
    
    // Функция для добавления количества предметов
    public void AddItem(string itemName, int amount)
    {
        if (itemCounterMap.ContainsKey(itemName))
        {
            itemCounterMap[itemName].amount += amount;
            itemCounterMap[itemName].UpdateText();
        }
    }

    // Функция для уменьшения количества предметов
    public void RemoveItem(string itemName, int amount)
    {
        if (itemCounterMap.ContainsKey(itemName))
        {
             itemCounterMap[itemName].amount = Mathf.Max(0, itemCounterMap[itemName].amount - amount);
             itemCounterMap[itemName].UpdateText();
        }
    }


    // Общая функция показа экрана
    private void ShowScreen(GameObject screen)
    {
        if (mainMenuScreen != null) mainMenuScreen.SetActive(screen == mainMenuScreen);
        if (settingsScreen != null) settingsScreen.SetActive(screen == settingsScreen);
        if(inventoryScreen != null) inventoryScreen.SetActive(screen == inventoryScreen);
    }

    // Обработчики нажатий на кнопки меню
    private void OnNewGameButtonClick()
    {
        Debug.Log("Нажата кнопка Новая игра");
        // Добавить переход на игровой экран
    }

    private void OnContinueButtonClick()
    {
        Debug.Log("Нажата кнопка Продолжить");
        // Добавить логику загрузки сохранения
    }

    private void OnSettingsButtonClick()
    {
       Debug.Log("Нажата кнопка Настройки");
        ShowScreen(settingsScreen);
    }

    private void OnExitButtonClick()
    {
        Debug.Log("Нажата кнопка Выход");
        Application.Quit();
    }
    
    // Обработчики кнопок настроек
    private void OnBackToMenuButtonClick()
    {
      Debug.Log("Нажата кнопка Назад");
      ShowScreen(mainMenuScreen);
    }
     private void OnBindKeysButtonClick()
    {
     Debug.Log("Нажата кнопка Привязка Клавиш");
    }

    // Обработчики кнопок инвентаря
    private void OnCloseInventoryButtonClick()
    {
      Debug.Log("Нажата кнопка Закрыть инвентарь");
      ShowScreen(mainMenuScreen);
    }


    // Класс для счетчиков
     [System.Serializable]
    public class ItemCounter
    {
        public string itemName;
        public int amount;
        public Text counterText;
        
         public void UpdateText()
        {
            if (counterText != null)
            {
                counterText.text = itemName + ": " + amount;
            }
        }
    }
}