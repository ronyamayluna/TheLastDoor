
using UnityEngine;
using UnityEngine.UI;

public class FillAreaCanvasLoading : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;

    public void Start()
    {
        ChangeEdgeRight(0, 837);
    }

    public void ChangeEdgeRight(float inset, float size)
    {
        if (rectTransform != null)
        {
            // Устанавливаем отступ и размер относительно левой стороны
            rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 837, size);
        }
        
    }
}
