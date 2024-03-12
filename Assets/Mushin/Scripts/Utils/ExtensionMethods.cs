using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public static class ExtensionMethods
{
    #region Transform

    public static void DestroyAllChildren(this Transform transform)
    {
        foreach (Transform child in transform)
        {
            Object.Destroy(child.gameObject);
        }
    }

    #endregion

    #region Task

    public static async void WrapErrors(this Task task)
    {
        await task; //Para mostrar en el editor las posibles excepciones de la task
    }

    #endregion

    #region Image

    public static void SetAlpha(this Image image, float alpha)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
    }

    #endregion
}