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

function resetPass(icon, campo) {
    var x = document.getElementById(campo);
    var c = document.getElementById(icon);
    x.type = "password";
    c.className = "fa fa-eye";
}
