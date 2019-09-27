//Variáveis globais//
var regex_celular = /^(?:(?:\+|00)?(55)\s?)?([0-9]{2})((9[0-9]{8}))$/; 
var regex_email = /^[a-z0-9.]+@[a-z-0-9]+\.[a-z]+\:?.([a-z]+)$/;
var regex_nome = /^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ ]+$/;
var regex_sobrenome = /^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ ]+$/;
var js_cerquilha = "#";

var foneEmailInvalido = false;
var senhaInvalida = false;
var nomeInvalido = false;
var sobrenomeInvalido = false;
var datanascimentoInvalida = false;

//Variáveis globais//

//Funções de cadastro e login//
//valida o email ou celular
$.fn.verificaEmailCelular = function (id) {
    var jid = js_cerquilha.concat(id)
    var phone = $(jid).val();

    foneEmailInvalido = false;

    if (!phone.match(regex_celular)) {
        foneEmailInvalido = true;
    }

    if (!phone.match(regex_email) && foneEmailInvalido == true) {
        foneEmailInvalido = true;
    } else {
        foneEmailInvalido = false;
    }

    if (foneEmailInvalido) {
        $(jid).next().text("Por favor preecha um Email ou Celular válido!");
    } else {
        $(jid).next().text("");
    }
};

//valida a senha
$.fn.varificarSenhaLenght = function (id) {
    var jid = js_cerquilha.concat(id)
    var senhaLength = $(jid).val().length;

    senhaInvalida = false;

    if (senhaLength < 8) {
        $(jid).next().text("Sua senha deve conter no mínimo 8 digitos!");

        senhaInvalida = true;
    } else {
        $(jid).next().text("");
        senhaInvalida = false;
    }
}

//valida o nome
$.fn.verificaNome = function (id) {
    var jid = js_cerquilha.concat(id)
    var nome = $(jid).val();

    nomeInvalido = false;

    if (!nome.match(regex_nome)) {
        nomeInvalido = true;
    }

    if (nomeInvalido) {
        $(jid).next().text("Por favor preecha um Nome válido!");

    } else {
        $(jid).next().text("");
    }
}

//valida o sobrenome
$.fn.verificaSobrenome = function (id) {
    var jid = js_cerquilha.concat(id)
    var sobrenome = $(jid).val();

    sobrenomeInvalido = false;

    if (!sobrenome.match(regex_sobrenome)) {
        sobrenomeInvalido = true;
    }

    if (sobrenomeInvalido) {
        $(jid).next().text("Por favor preecha um sobrenome válido!");

    } else {
        $(jid).next().text("");
    }
}

//valida a data de nascimento
$.fn.verificaDataNascimento = function (id) {
    var jid = js_cerquilha.concat(id)
    var datanascimento = $(jid).val();

    datanascimentoInvalida = false;

    if (!datanascimento) {
        datanascimentoInvalida = true;
    }

    if (datanascimentoInvalida) {
        $(jid).next().text("Por favor preecha uma data!");
    } else {
        $(jid).next().text("");
    }
}

//Funções de cadastro e login//