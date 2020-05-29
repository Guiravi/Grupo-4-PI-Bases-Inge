let topicosRad = document.getElementById('topicos');
let titulosRad = document.getElementById('titulos');
let autoresRad = document.getElementById('autores');
let barraBusq = document.getElementById('barraBusqueda');
let topicosDrop = document.getElementById('topicosDropdown');
let buscarPorBtn = document.getElementById('buscarPor');
let aceptarBtn = document.getElementById('aceptar');
let barra = document.getElementById('barra');
let dropdown = document.getElementById('dropdown');
let tiposArtRad = document.getElementById('tiposArtRadio');
let reiniciarBtn = document.getElementById('reiniciar');

topicosRad.addEventListener('click', topicosRadClick);
titulosRad.addEventListener('click', titulosRadClick);
autoresRad.addEventListener('click', autoresRadClick);
topicosRad.addEventListener('click', anyOptionClick);
titulosRad.addEventListener('click', anyOptionClick);
autoresRad.addEventListener('click', anyOptionClick);


setEnterSubmit();


function topicosRadClick() {
    topicosDrop.style.display = 'initial';
    barraBusq.style.display = 'none';
    buscarPorBtn.textContent = 'Tópicos';
    aceptarBtn.style.display = 'initial';
    aceptarBtn.style.position = 'relative';
    aceptarBtn.style.right = '300px';
    dropdown.focus();
} 

function titulosRadClick() {
    topicosDrop.style.display = 'none';
    barraBusq.style.display = 'initial';
    buscarPorBtn.textContent = 'Títulos';
    aceptarBtn.style.display = 'initial';
    aceptarBtn.style.position = 'initial';
    barra.focus();
} 

function autoresRadClick() {
    topicosDrop.style.display = 'none';
    barraBusq.style.display = 'initial';
    buscarPorBtn.textContent = 'Autores';
    aceptarBtn.style.display = 'initial';
    aceptarBtn.style.position = 'initial';
    barra.focus();
}

function anyOptionClick() {
    tiposArtRad.style.display = 'initial';
    reiniciarBtn.style.display = 'initial';
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



