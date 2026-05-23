using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropWord : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Transform puzzlePanelTransform; // Ссылка на главную панель
    private Canvas mainCanvas; // Ссылка на Canvas для безопасного масштабирования

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null) canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    private void Start()
    {
        // Запоминаем главную панель (родитель объекта при старте)
        puzzlePanelTransform = transform.parent;

        // Безопасно ищем Canvas выше по иерархии
        mainCanvas = GetComponentInParent<Canvas>();
        if (mainCanvas == null)
        {
            Debug.LogError($"На объекте {gameObject.name} или выше него не найден компонент Canvas!");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;

        // Выносим на самый верх панели, чтобы при перетаскивании слово не перекрывалось другими элементами
        if (puzzlePanelTransform != null)
        {
            transform.SetParent(puzzlePanelTransform.root);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Получаем scaleFactor безопасно. Если Canvas вдруг не найден, берем 1f по умолчанию
        float scale = (mainCanvas != null) ? mainCanvas.scaleFactor : 1f;

        // Слово плавно и правильно следует за курсором мыши
        rectTransform.anchoredPosition += eventData.delta / scale;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // Если слово бросили просто на панели (не в ячейку ответа), 
        // оно остается лежать там, где отпустили мышку
        if (transform.parent == transform.root && puzzlePanelTransform != null)
        {
            transform.SetParent(puzzlePanelTransform);
        }
    }

    // Этот метод вызывается ячейкой, если в неё кладут другое слово (выталкивание)
    public void KickOutToPanel()
    {
        if (puzzlePanelTransform != null)
        {
            transform.SetParent(puzzlePanelTransform);
            // Слегка смещаем позицию вытолкнутого слова, чтобы оно не перекрывало новое
            rectTransform.anchoredPosition += new Vector2(0, -80f);
        }
    }
}

