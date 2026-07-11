
/**
 * Realiza una búsqueda enviando una petición POST al handler del servidor
 * y muestra las sugerencias recibidas en el contenedor #Sugerencias.
 *
 * @param {string} text - Texto introducido por el usuario.
 * @param {string} tipe - Tipo de búsqueda (proviene del data-tipo del input).
 */
async function buscar(text, tipe) {
    fetch(`${ruta}Handler/Search.ashx`, {
        method: "Post",
        headers: { "Content-type": "application/json" },
        body: JSON.stringify({
            query: text,
            tipo: tipe
        })
    }).then(res => res.json()).then(data => { // Convierte la respuesta en JSON
        const box = document.getElementById("Sugerencias");
        box.innerHTML = ""; // Limpia el contenedor antes de mostrar nuevas sugerencias
        // Construye el HTML con cada sugerencia recibida
        data.forEach(item => {

            const div = document.createElement("div");
            const button = document.createElement("button");
            button.classList.add("sugerenciaButton");

            button.textContent = item;

            button.addEventListener("click", () => {
                document.querySelector(`.buscador[data-tipo="${tipe}"]`).value = item; // Rellena el input con la sugerencia
            });

            div.appendChild(button);
            box.appendChild(div);
        });
        // Muestra u oculta el contenedor según si hay resultados
        box.style.display = data.length > 0 ? "block" : "none";
    });
}


/**
 * Añade un listener a todos los inputs con clase .buscador.
 * Cada vez que el usuario escribe, se llama a buscar().
 */
document.querySelectorAll(".buscador").forEach(input => {
    input.addEventListener("input", e => {
        const text = e.target.value;
        const tipe = e.target.dataset.tipo;
        buscar(text,tipe);
    })
});