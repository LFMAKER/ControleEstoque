//Para proteger a aplicação, a senha cadastrada não é populada
function set_dados_form(dados) {
    $('#id_cadastro').val(dados.Id);
    $('#txt_nome').val(dados.Nome);
    $('#txt_login').val(dados.Login);
    $('#txt_email').val(dados.Email);
    $('#txt_senha').val("{$127;$188}");
    $('#ddl_perfil').val(dados.IdPerfil);

}


function set_focus_form() {
    $('#txt_nome').focus();
}
function set_dados_grid(dados) {
    return '<td>' + dados.Nome + '</td>' +
           '<td>' + dados.Login + '</td>'+
           '<td>' + dados.Email + '</td>';
}
function get_dados_inclusao() {

    return {
        Id: 0,
        Nome: '',
        Login: '',
        Email: '',
        Senha: '',
        IdPerfil: 0
    }
}

function get_dados_form() {

    if ($('#perfis').val() != "undefined") {
        return {
            Id: $('#id_cadastro').val(),
            Nome: $('#txt_nome').val(),
            Login: $('#txt_login').val(),
            Email: $('#txt_email').val(),
            Senha: $('#txt_senha').val(),
            IdPerfil: $('#ddl_perfil').val()
        }
    }

    return {
        Id: $('#id_cadastro').val(),
        Nome: $('#txt_nome').val(),
        Login: $('#txt_login').val(),
        Email: $('#txt_email').val(),
        Senha: $('#txt_senha').val(),
        IdPerfil: null
    }



}

function preencher_linha_grid(param, linha) {
    linha
        .eq(0).html(param.Nome).end()
        .eq(1).html(param.Login)
        .eq(2).html(param.Email);
}