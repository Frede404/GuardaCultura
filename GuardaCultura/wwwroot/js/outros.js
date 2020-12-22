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

function CodificaImagem(path){
    bos = null;
    try {
        f = new File(path);
        fis = new FileInputStream(f);
        buffer = new byte[1024];
        bos = new ByteArrayOutputStream();
        for (len; (len = fis.read(buffer)) != -1;) {
            bos.write(buffer, 0, len);
        }
    } catch (e) {
        System.err.println(e.getMessage());
    }

    return bos != null ? bos.toByteArray() : null;
}

function returnimage(nome) {
    document.getElementById(nome).innerHTML = CodificaImagem;
}