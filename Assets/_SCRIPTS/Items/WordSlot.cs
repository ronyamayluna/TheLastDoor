using UnityEngine;
using UnityEngine.EventSystems;

public class WordSlot : MonoBehaviour, IDropHandler
{
    public int puzzleSlotIndex;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            DragDropWord draggedWord = eventData.pointerDrag.GetComponent<DragDropWord>();

            if (draggedWord != null)
            {
                // Если в ячейке уже лежит слово, выталкиваем его обратно на общую панель
                if (transform.childCount > 0)
                {
                    Transform existingWord = transform.GetChild(0);
                    DragDropWord existingWordScript = existingWord.GetComponent<DragDropWord>();

                    if (existingWordScript != null)
                    {
                        existingWordScript.KickOutToPanel();
                    }
                }

                // Закрепляем новое слово строго по центру ячейки
                draggedWord.transform.SetParent(transform);
                draggedWord.transform.localPosition = Vector3.zero;

                // Проверяем, собрано ли предложение
                if (SentencePuzzleManager.Instance != null)
                {
                    SentencePuzzleManager.Instance.CheckSentence();
                }
            }
        }
    }
}

