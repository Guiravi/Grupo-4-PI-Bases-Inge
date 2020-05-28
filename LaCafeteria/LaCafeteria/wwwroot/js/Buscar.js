let topicosBtn = document.getElementById('topicos');
let titulosBtn = document.getElementById('titulos');
let autoresBtn = document.getElementById('autores');
let barraBusq = document.getElementById('barraBusqueda');
let topicosDrop = document.getElementById('topicosDropdown');
let buscarPorBtn = document.getElementById('buscarPor');
let aceptarBtn = document.getElementById('aceptar');
let barra = document.getElementById('barra');
let dropdown = document.getElementById('dropdown');
let textoBusq = document.getElementById('textoResultBusq');
let cantBusq = document.getElementById('cantResultBusq');
let formBusqueda = document.getElementById('formBusqueda');


topicosBtn.addEventListener('click', topicosBtnClick);
titulosBtn.addEventListener('click', titulosBtnClick);
autoresBtn.addEventListener('click', autoresBtnClick);
aceptarBtn.addEventListener('click', aceptarBtnClick);
window.addEventListener('load', cargarForm);

setEnterSubmit();


function topicosBtnClick() {
    topicosDrop.style.display = 'initial';
    barraBusq.style.display = 'none';
    buscarPorBtn.textContent = 'Tópicos';
    aceptarBtn.style.display = 'initial';
    aceptarBtn.style.position = 'relative';
    aceptarBtn.style.right = '300px';
    dropdown.focus();
} 

function titulosBtnClick() {
    topicosDrop.style.display = 'none';
    barraBusq.style.display = 'initial';
    buscarPorBtn.textContent = 'Títulos';
    aceptarBtn.style.display = 'initial';
    aceptarBtn.style.position = 'initial';
    barra.focus();
} 

function autoresBtnClick() {
    topicosDrop.style.display = 'none';
    barraBusq.style.display = 'initial';
    buscarPorBtn.textContent = 'Autores';
    aceptarBtn.style.display = 'initial';
    aceptarBtn.style.position = 'initial';
    barra.focus();
} 

function enterSubmit() {
    aceptarBtn.click();
}

function setEnterSubmit() {
    window.onkeydown = function (e) {
        if (e.keyCode === 13 && document.activeElement === barra) {
            aceptarBtn.click();
        }
    }
}

function aceptarBtnClick() {
    textoBusq.style.visibility = "visible"
    cantBusq.style.visibility = "visible"

    if (topicosBtn.checked) {
        textoBusq.textContent = `Resultados de la búsqueda: "Tópicos"`;
    } else {
        textoBusq.textContent = `Resultados de la búsqueda: "${barra.value}"`;
    }
}


function DoAjaxPostAndMore(btnClicked)
{
        var $form = $(btnClicked).parents('form');

        $.ajax({
            type: 'POST',
            url: $form.attr('action'),
            data: $form.serialize(),
            error: function(xhr, status, error) {
                  //do something about the error
             },
            success: function(response) {
                 //do something with response

            }
        });

  return false;// if it's a link to prevent post

}


function cargarForm() {
    $.ajax({
        type: 'GET',
        url: 'Buscar?handler=CargarPagina',
        success: function (data) {
        },
        error: function (error) {
            alert("Error: No se pudieron cargar los tópicos");
        }
    })
}




