
function mostrarAlerta(texto, tipo = "sucesso") {
    const cores = {
        sucesso: "#198754", // verde
        erro: "#dc3545",    // vermelho
        warning: "#ffc107", // amarelo
        info: "#0d6efd"     // azul
    };

    const cor = cores[tipo] || cores.info;

    $('#divMensagem')
        .css("background-color", cor)
        .removeClass("d-none")
        .addClass("show");

    $('#lblMensagemTexto').text(texto);

    // Fecha automaticamente após 4 segundos
    setTimeout(() => ocultarAlerta(), 4000);
}

function ocultarAlerta() {
    $('#divMensagem').removeClass("show").addClass("d-none");
}
