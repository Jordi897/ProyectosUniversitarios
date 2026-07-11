#include <Windows.h>
#include "resource.h"

BOOL CALLBACK fVentana(HWND, UINT, WPARAM, LPARAM);

int WINAPI WinMain(HINSTANCE hInst, HINSTANCE hPrev, PSTR cmdLine, int cShow) {
    HWND hVentana = CreateDialog(hInst, MAKEINTRESOURCE(IDD_VENTANA), NULL, fVentana);

    MSG msg;
    ZeroMemory(&msg, sizeof(MSG));

    ShowWindow(hVentana, cShow);

    while (GetMessage(&msg, NULL, 0, 0)) {
        TranslateMessage(&msg);
        DispatchMessage(&msg);
    }
    return 0;
}

BOOL CALLBACK fVentana(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam) {
    switch (msg) {
    case WM_CLOSE:
        DestroyWindow(hwnd);
        break;
    case WM_DESTROY:
        PostQuitMessage(73);
        break;
    }
    return FALSE;
}
