/*
Developed by Leonardo Oliveira
Função: Responsável por implementar os dados do CRUD PAIS no CadBase.JS
Data de criação: 30/04/2019
*/



function set_dados_form(dados) {
    $('#id_cadastro').val(dados.Id);
    $('#txt_nome').val(dados.Nome);
    $('#txt_nome_pt').val(dados.NomePt);
    $('#txt_sigla').val(dados.Sigla);
    $('#txt_bacen').val(dados.Bacen);
    $('#cbx_ativo').prop('checked', dados.Ativo);
}

function set_focus_form() {
    $('#txt_nome').focus();
}

function set_dados_grid(dados) {
    return '<td>' + dados.Nome + '</td>' +
            '<td>' + dados.Sigla + '</td>' +
            '<td>' + dados.Bacen + '</td>' +
           '<td>' + (dados.Ativo ? 'Sim' : 'Não') + '</td>';
}

function get_dados_inclusao() {
    return {
        Id: 0,
        Nome: '',
        NomePt: '',
        Sigla: '',
        Bacen: 0,
        Ativo: true
    };
}

function get_dados_form() {
    return {
        Id: $('#id_cadastro').val(),
        Nome: $('#txt_nome').val(),
        NomePt: $('#txt_nome_pt').val(),
        Sigla: $('#txt_sigla').val(),
        Bacen:  $('#txt_bacen').val(),
        Ativo: $('#cbx_ativo').prop('checked')
    };
}

function preencher_linha_grid(param, linha) {
    linha
        .eq(0).html(param.Nome).end()
        .eq(1).html(param.Sigla).end()
        .eq(2).html(param.Bacen).end()
        .eq(3).html(param.Ativo ? 'Sim' : 'Não');
}