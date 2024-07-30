using UnityEngine;
using UnityEngine.Android;

public class Almacenamiento : MonoBehaviour
{
    void Start()
    {
        // Verifica si el permiso ya ha sido otorgado
        if (Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            // El permiso ya ha sido concedido.
        }
        else
        {
            // Solicitar el permiso.
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            if (Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
            {
                // El permiso ha sido concedido.
            }
            else
            {
                // El permiso ha sido denegado. Maneja esta situación adecuadamente.
            }
        }
    }
}
