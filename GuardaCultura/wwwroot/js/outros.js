﻿//funcao para meter visivel a password
function show(icon, campo) {
    var x = document.getElementById(campo);
    var c = document.getElementById(icon);
    //var c = document.activeElement;
    if (x.type === "password") {
        x.type = "text";
        c.className = "fa fa-eye-slash";
    } else {
        x.type = "password";
        c.className = "fa fa-eye";
    }
}

//reset aos campos
function resetPass(icon, campo) {
    var x = document.getElementById(campo);
    var c = document.getElementById(icon);
    x.type = "password";
    c.className = "fa fa-eye";
}

// Get the modal
var modal = document.getElementById('id01');

// When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}

//carrega no butao automaticamente
function inicio(butao) {
    var link = document.getElementById(butao);
    for (var i = 0; i < 50; i++)
        link.click();
}

function goBack() {
    window.history.back();
}

function checkMiradouroFunction() {
    var checkBox = document.getElementById("checkMiradouro");
    var text = document.getElementById("checkOcupacao");
    if (checkBox.checked == false) {
        text.checked = true;
    } else {
        text.checked = false;
    }
}

function CorrigeEspacos(elemento) {
    var texto = elemento.value;
    var tamanho = texto.length;

    var auxtamanho = 0;
    var espacos = 0;
    var auxtexto = "";

    for (i = 0; i < tamanho; i++) {
        if (texto.charAt(i) == " ") {
            espacos++;
            if (espacos == 1 && i != 0) {
                auxtamanho++;
                auxtexto += " ";
            }
        } else {
            auxtamanho++;
            auxtexto += texto.charAt(i);
            espacos=0;
        }
    }
    if (auxtexto.charAt(auxtamanho-1) == " ") {
        auxtexto = auxtexto.slice(0, -1);
    }
    elemento.value = auxtexto;
}

function ApagaEspacos(elemento) {
    var texto = elemento.value;
    var tamanho = texto.length;
    var auxtexto = "";

    for (i = 0; i < tamanho; i++) {
        if (texto.charAt(i) != " ") {
            auxtexto += texto.charAt(i);
        }
    }
    elemento.value = auxtexto;
}

function DataMaxima(elemento) {
    var hoje = new Date();
    var dia = hoje.getDate();
    var mes = hoje.getMonth() + 1;
    var ano = hoje.getFullYear();
    var diaerro;

    if (dia < 10) {
        dia = '0' + dia
    }
    if (mes < 10) {
        mes = '0' + mes
    }

    hoje = ano + '-' + mes + '-' + dia;
    diaerro = dia + "/" + mes + "/" + ano;
    elemento.setAttribute("max", hoje);
    elemento.setAttribute("title", "Introduza uma data igual ou inferior a "+diaerro)
}