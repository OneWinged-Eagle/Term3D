using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class BackgroundKey : MonoBehaviour
{

    #region dll
    [DllImport("Dwmapi.dll")]static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref Margins margins);
    [DllImport("user32.dll")]static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
    [DllImport("user32.dll")]public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    private struct Margins{
        public int Left;
        public int Right;
        public int Top;
        public int Bottom;
    }
    #endregion dll

    #region Private

    private bool active = true;
    [SerializeField]private Material _keyMaterial;

    #endregion Private

    void Start() {
        transform.SetAsFirstSibling();
    }

    public void ActivateTransparency(IntPtr hWnd) {
        active = true;
        Color32 c =GetComponent<Camera>().backgroundColor;
        GetComponent<Camera>().backgroundColor=new Color32(c.r,c.g,c.b,5);
#if !UNITY_EDITOR
        var m = new Margins() { Left = -1 };
        SetWindowLong(hWnd, -16, 0x80000000 | 0x10000000);
        DwmExtendFrameIntoClientArea(hWnd, ref m);
#endif
    }

    public void DisableTransparency() {
        active = false;
        Color32 c = GetComponent<Camera>().backgroundColor;
        GetComponent<Camera>().backgroundColor = new Color32(c.r, c.g, c.b, 255);
    }

    

    



    
    
    

    void OnRenderImage(RenderTexture a, RenderTexture n)
    {
        if (active) {
            Graphics.Blit(a, n, _keyMaterial);
        }
       // else {
        //    Graphics.Blit(a, n);
      //  }
      

       
    }
}