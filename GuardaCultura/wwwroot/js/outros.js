//funcao para meter visivel a password
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