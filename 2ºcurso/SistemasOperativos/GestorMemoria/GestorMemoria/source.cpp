#include <Windows.h>
#include <iostream>
#include <fstream>
#include <vector>
#include "resource.h"
#include <string>
#include "Memoria.h"

#define MAX_MEMORIA 2000

INT_PTR CALLBACK fVentana(HWND, UINT, WPARAM, LPARAM);
bool abrirArchivo(std::string,std::string&);
vector<Proceso> leerProcesos(std::string);
void primerHueco(vector<Proceso>&,HWND,vector<string>&);
void siguienteHueco(vector<Proceso>&, HWND,vector<string>&);

vector<string> estados;
int index = -1;

int WINAPI WinMain(HINSTANCE hInst, HINSTANCE hPrev, PSTR cmdLine, int cShow) {
    HWND hVentana = CreateDialog(hInst, MAKEINTRESOURCE(IDD_VENTANA), NULL, fVentana);
	HWND hCombobox = GetDlgItem(hVentana, IDC_COMBO1);
	SendMessage(hCombobox, CB_ADDSTRING, 0, (LPARAM)"PrimerHueco");
    SendMessage(hCombobox, CB_ADDSTRING, 0, (LPARAM)"SiguienteHueco");
    MSG msg;
    ZeroMemory(&msg, sizeof(MSG));
    ShowWindow(hVentana, cShow);

    while (GetMessage(&msg, NULL, 0, 0)) {
        TranslateMessage(&msg);
        DispatchMessage(&msg);
    }
    return 0;
}

INT_PTR CALLBACK fVentana(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam) {
    switch (msg) {
    case WM_COMMAND: {
        if (LOWORD(wParam) == IDC_BUTTON1 && HIWORD(wParam) == BN_CLICKED) {
            int comboLength = GetWindowTextLength(GetDlgItem(hwnd, IDC_COMBO1)) + 1;
            char text[256];
            HWND hCombo = GetDlgItem(hwnd, IDC_COMBO1);
            GetWindowText(hCombo, text, comboLength);
            if (!(strcmp(text, "PrimerHueco") == 0) && !(strcmp(text, "SiguienteHueco") == 0)) {
                MessageBox(hwnd, "Seleccione una algoritmo", "Error", MB_OK | MB_ICONERROR);
                return FALSE;
            }
            string Archivo;
            string Texto;
            HWND hEdit = GetDlgItem(hwnd, IDC_EDIT1);
            int Tam = GetWindowTextLength(hEdit);
            TCHAR* buffer = new TCHAR[Tam + 1];
            GetWindowText(hEdit, buffer, Tam + 1);
            Texto = buffer;
            delete[] buffer;
            if (!abrirArchivo(Texto, Archivo)) {
                MessageBox(hwnd, "No se pudo abrir el archivo", "Error", MB_OK | MB_ICONERROR);
                return FALSE;
            }
            vector<Proceso> procesos = leerProcesos(Archivo);
            ShowWindow(hCombo, SW_HIDE);
            ShowWindow(hEdit, SW_HIDE);
			ShowWindow(GetDlgItem(hwnd, IDC_BUTTON1), SW_HIDE);
			ShowWindow(GetDlgItem(hwnd, IDC_STATIC3), SW_HIDE);
			ShowWindow(GetDlgItem(hwnd, IDC_STATIC2), SW_HIDE);
            EnableWindow(GetDlgItem(hwnd, IDC_BUTTON1), FALSE);

            if (strcmp(text, "PrimerHueco") == 0) {
                primerHueco(procesos, hwnd, estados);
            }
            else if (strcmp(text, "SiguienteHueco") == 0) {
                siguienteHueco(procesos, hwnd, estados);
            }
            MessageBox(hwnd, "Se ha creado el archivo particiones.txt", "Exito", MB_OK | MB_ICONINFORMATION);
            if (SetTimer(hwnd, 1, 5000, NULL) == 0) MessageBox(hwnd, "No se pudo iniciar el timer", "Error", MB_OK | MB_ICONERROR);
			return TRUE;
        }
    }break;

    case WM_CLOSE:
        DestroyWindow(hwnd);
        break;
    case WM_DESTROY:
        PostQuitMessage(73);
        break;
    case WM_TIMER: {
        if (index+1 < estados.size()) {
            index++;
            InvalidateRect(hwnd, NULL, TRUE);
        }
        else {
            KillTimer(hwnd, 1);
        }
		return TRUE;
    }break;
    case WM_PAINT: {
        PAINTSTRUCT ps;
        HDC hdc = BeginPaint(hwnd, &ps);

        // dibujar sólo si hay un estado seleccionado
        if (index >= 0 && index < static_cast<int>(estados.size())) {
            const int left = 50;
            const int top = 50;
            const int right = 800;
            const int bottom = 400;

            // rectángulo exterior de la estantería (relleno marrón)
            RECT shelfRect = { left, top, right, bottom };
            HBRUSH hShelfBrush = CreateSolidBrush(RGB(160, 82, 45)); // marrón madera
            FillRect(hdc, &shelfRect, hShelfBrush);
            DeleteObject(hShelfBrush);

            // área interior (márgenes) donde se colocan los libros — fondo más claro
            const int margin = 6;
            RECT inner = { left + margin, top + margin, right - margin, bottom - margin };
            HBRUSH hInner = CreateSolidBrush(RGB(205, 133, 63)); // marrón claro
            FillRect(hdc, &inner, hInner);
            DeleteObject(hInner);

            // borde exterior de la estantería: dibujar marco con FrameRect para no repintar el interior
            HBRUSH hShelfFrame = CreateSolidBrush(RGB(90, 45, 20));
            FrameRect(hdc, &shelfRect, hShelfFrame);
            DeleteObject(hShelfFrame);

            // paleta de colores para libros
            const COLORREF palette[] = {
                RGB(220, 20, 60), RGB(30, 144, 255), RGB(34, 139, 34),
                RGB(255, 140, 0), RGB(148, 0, 211), RGB(70, 130, 180),
                RGB(255, 99, 71), RGB(60, 179, 113), RGB(255, 215, 0),
                RGB(0, 206, 209)
            };
            const size_t paletteCount = sizeof(palette) / sizeof(palette[0]);

            // escala basada en el ancho interior (evita que los libros salgan fuera)
            const int innerWidth = inner.right - inner.left;
            const double scale = static_cast<double>(innerWidth) / static_cast<double>(MAX_MEMORIA);

            // parsear el estado: "instante [pos nombre tam] ..."
            std::istringstream iss(estados[index]);
            int instante = 0;
            iss >> instante;

            char ch;
            while (iss >> ch) {
                if (ch != '[') break;
                int pos = 0;
                std::string nombre;
                int tam = 0;
                iss >> pos >> nombre >> tam;
                // consumir el ']'
                iss >> ch;

                // cálculo posición y tamaño escalados usando el área interior
                int x = inner.left + static_cast<int>(pos * scale + 0.5);
                int w = static_cast<int>(tam * scale + 0.5);

                // asegurar anchura mínima visible
                if (w < 2) w = 2;

                // recortar para que no salga por la izquierda ni por la derecha del inner
                if (x < inner.left) {
                    int delta = inner.left - x;
                    x = inner.left;
                    w -= delta;
                }
                if (x + w > inner.right) {
                    w = inner.right - x;
                }

                // si tras el recorte no hay ancho, saltar
                if (w <= 0) continue;

                RECT r = { x, inner.top + 6, x + w, inner.bottom - 6 };

                // color según tipo
                HBRUSH hBrush;
                if (nombre == "Hueco") {
                    hBrush = CreateSolidBrush(RGB(245, 245, 220)); // beige claro para hueco
                }
                else {
                    unsigned int hash = 0;
                    for (unsigned char c : nombre) hash = hash * 31 + c;
                    COLORREF col = palette[hash % paletteCount];
                    hBrush = CreateSolidBrush(col);
                }

                // rellenar el bloque (libro/hueco)
                FillRect(hdc, &r, hBrush);

                // borde del bloque: usar FrameRect para no sobreescribir el relleno
                HBRUSH hFrame = CreateSolidBrush(RGB(40, 40, 40));
                FrameRect(hdc, &r, hFrame);
                DeleteObject(hFrame);

                // texto vertical centrado (si cabe)
                SetBkMode(hdc, TRANSPARENT);
                SetTextColor(hdc, RGB(0, 0, 0));
                std::string label = nombre;

                // construir cadena vertical (cada carácter en su propia línea)
                std::string verticalLabel;
                verticalLabel.reserve(label.size() * 2);
                for (size_t k = 0; k < label.size(); ++k) {
                    verticalLabel.push_back(label[k]);
                    if (k + 1 < label.size()) verticalLabel.push_back('\n');
                }

                // rectángulo para el texto con padding y asegurando que está dentro de r
                RECT tr = r;
                const int padText = 3;
                tr.left += padText;
                tr.right -= padText;

                // calcular altura del texto vertical
                RECT calc = tr;
                DrawTextA(hdc, verticalLabel.c_str(), -1, &calc, DT_CENTER | DT_NOPREFIX | DT_CALCRECT);

                int textHeight = calc.bottom - calc.top;
                int availHeight = r.bottom - r.top;

                // si el texto es más alto que el bloque, limitar la altura al bloque
                if (textHeight > availHeight) {
                    textHeight = availHeight;
                }

                RECT drawRect = tr;
                drawRect.top = r.top + ((availHeight - textHeight) / 2);
                drawRect.bottom = drawRect.top + textHeight;

                // dibujar el texto centrado horizontalmente
                DrawTextA(hdc, verticalLabel.c_str(), -1, &drawRect, DT_CENTER | DT_NOPREFIX | DT_END_ELLIPSIS);

                DeleteObject(hBrush);
            }
        }

        EndPaint(hwnd, &ps);
    } break;
    }
    return FALSE;
}

void primerHueco(vector<Proceso>& procesos, HWND hwnd,vector<string> &estados) {
    ofstream escribir("particiones.txt");
	stringstream ss;

    Memoria m(MAX_MEMORIA);

    if (!escribir.is_open()) {
        MessageBox(hwnd, "No se pudo abrir o crear el archivo", "Error", MB_OK | MB_ICONERROR);
        return;
    }

    while (!procesos.empty() || !m.isEmpty()) {

        for (int i = 0; i < procesos.size(); i++) {
            if (procesos[i].instanteLLegada <= m.getInstante_Tiempo()) {
                try {
                    m.addProcesoPrimerHueco(procesos[i]);
                    procesos.erase(procesos.begin() + i);
                    i--;
                }
                catch (const runtime_error&) {

                }
                catch (...) {
                    MessageBox(hwnd, "No se pudo abrir el archivo", "Error", MB_OK | MB_ICONERROR);
                    DestroyWindow(hwnd);
                }
            }
        }
        escribir << m << endl;
		ss << m;
		estados.push_back(ss.str());
		ss.str("");
        m.passInstante();
    }
    escribir << m << endl;
	ss << m;
	estados.push_back(ss.str());
	ss.clear();
    ss.str("");
}

void siguienteHueco(vector<Proceso>& procesos, HWND hwnd, vector<string> &estados) {
    ofstream escribir("particiones.txt");
	stringstream ss;

    Memoria m(MAX_MEMORIA);

    if (!escribir.is_open()) {
        MessageBox(hwnd, "No se pudo abrir o crear el archivo", "Error", MB_OK | MB_ICONERROR);
        return;
    }

    while (!procesos.empty() || !m.isEmpty()) {

        for (int i = 0; i < procesos.size(); i++) {
            if (procesos[i].instanteLLegada <= m.getInstante_Tiempo()) {
                try {
                    m.addProcesoSiguienteHueco(procesos[i]);
                    procesos.erase(procesos.begin() + i);
                    i--;
                }
                catch (const runtime_error&) {

                }
                catch (...) {
                    MessageBox(hwnd, "No se pudo abrir el archivo", "Error", MB_OK | MB_ICONERROR);
                    DestroyWindow(hwnd);
                }
            }
        }
        escribir << m << endl;
		ss << m;
		estados.push_back(ss.str());
        ss.str("");
        m.passInstante();
    }
    escribir << m << endl;
	ss << m;
	estados.push_back(ss.str());
    ss.str("");
    escribir.close();
}

bool abrirArchivo(std::string nombre, std::string& archivo) {
    std::ifstream lectura(nombre);
    if (!lectura.is_open()) {
        return false;
    }
    std::string aux;
    archivo = "";
    while (getline(lectura, aux)) {
        archivo += aux + "\n";
    }
    std::cout << archivo << std::endl;
    lectura.close();
    return true;
}

vector<Proceso> leerProcesos(string archivo) {
    stringstream ss(archivo);
    string proceso;
    vector<Proceso> procesos;

    while (getline(ss, proceso)) {
        stringstream ss2(proceso);
        Proceso p;
        getline(ss2, p.nombre, ' ');
        ss2 >> p.instanteLLegada; ss2.ignore(); ss2 >> p.tamano; ss2.ignore(); ss2 >> p.tiempoEjec;
        procesos.push_back(p);
    }

    return procesos;

}