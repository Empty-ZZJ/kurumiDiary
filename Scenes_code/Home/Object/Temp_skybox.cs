using UnityEngine;

public class Temp_skybox : MonoBehaviour
{
    public static bool Temp_skybox_state;
    public Material usually;
    public static Material usually2;

    private void Start ()
    {
        usually2 = usually;
    }

    public static void Temp_Core_touch_on ()
    {
        Temp_skybox_state = true;
    }

    public static void Cancel_Temp_touch ()
    {
        Temp_skybox_state = false;
    }

    public static void Temp_Core_skybox_on ()
    {
        RenderSettings.skybox = usually2;
        DynamicGI.UpdateEnvironment();
        Temp_skybox_state = true;
    }

    public static void Cancel_Temp_skybox ()
    {
        Temp_skybox_state = false;
    }
}