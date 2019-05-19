function set_dados_form(dados) {
    $('#id_cadastro').val(dados.Id);
    $('#txt_nome').val(dados.Nome);
    $('#txt_num_documento').val(dados.NumDocumento);
    $('#txt_razao_social').val(dados.RazaoSocial);
    $('#txt_telefone').val(dados.Telefone);
    $('#txt_contato').val(dados.Contato);
    $('#txt_logradouro').val(dados.Logradouro);
    $('#txt_numero').val(dados.Numero);
    $('#txt_complemento').val(dados.Complemento);
    $('#txt_cep').val(dados.Cep);
    $('#txt_bairro').val(dados.Bairro);
    $('#txt_cidade').val(dados.Cidade);
    $('#txt_estado').val(dados.Estado);
    $('#txt_pais').val(dados.Pais);
    $('#cbx_ativo').prop('checked', dados.Ativo);
    $('#cbx_pessoa_juridica').prop('checked', false);
    $('#cbx_pessoa_fisica').prop('checked', false);

    if (dados.Tipo == 2) {
        $('#cbx_pessoa_juridica').prop('checked', true).trigger('click');
    }
    else {
        $('#cbx_pessoa_fisica').prop('checked', true).trigger('click');
    }

  

}

function set_focus_form() {
    $('#txt_nome').focus();
}

function set_dados_grid(dados) {
    return '<td>' + dados.Nome + '</td>' +
            '<td>' + dados.Telefone + '</td>' +
            '<td>' + dados.Contato + '</td>' +
           '<td>' + (dados.Ativo ? 'Sim' : 'Não') + '</td>';
}


function get_dados_inclusao() {
    return {
        Id: 0,
        Nome: '',
        NumDocumento: '',
        RazaoSocial: '',
        Tipo: 2,
        Telefone: '',
        Contato: '',
        Logradouro: '',
        Numero: '',
        Complemento: '',
        Cep: '',
        Bairro: '',
        Cidade: '',
        Estado: '',
        Pais: '',
        Ativo: true
    };
}

function get_dados_form() {
    return {
        Id: $('#id_cadastro').val(),
        Nome: $('#txt_nome').val(),
        NumDocumento: $('#txt_num_documento').val(),
        RazaoSocial: $('#txt_razao_social').val(),
        Tipo: $('#cbx_pessoa_juridica').is(':checked') ? 2 : 1,
        Telefone: $('#txt_telefone').val(),
        Contato: $('#txt_contato').val(),
        Logradouro: $('#txt_logradouro').val(),
        Numero: $('#txt_numero').val(),
        Complemento: $('#txt_complemento').val(),
        Cep: $('#txt_cep').val(),
        Bairro: $('#txt_bairro').val(),
        Cidade: $('#txt_cidade').val(),
        Estado: $('#txt_estado').val(),
        Pais: $('#txt_pais').val(),
        Ativo: $('#cbx_ativo').prop('checked')
    };
}

function preencher_linha_grid(param, linha) {
    linha
        .eq(0).html(param.Nome).end()
        .eq(1).html(param.Telefone).end()
        .eq(2).html(param.Contato).end()
        .eq(3).html(param.Ativo ? 'Sim' : 'Não');
}

$(document)
.ready(function () {
    $('#txt_telefone').mask('(00) 0000-0000');
    $('#txt_cep').mask('00000-000');
})
.on('click', '#cbx_pessoa_juridica', function () {
    $('label[for="txt_num_documento"]').text('CNPJ');
    $('#txt_num_documento').mask('00.000.000/0000-00', { reverse: true });
    $('#container_razao_social').removeClass('invisible');
})
.on('click', '#cbx_pessoa_fisica', function () {
    $('label[for="txt_num_documento"]').text('CPF');
    $('#txt_num_documento').mask('000.000.000-00', { reverse: true });
    $('#container_razao_social').addClass('invisible');
});


$('#txt_cep').on('blur', function () {
    var cep = $('#txt_cep').val();
    console.log(cep.length);
    $.getJSON("http://api.postmon.com.br/v1/cep/" + cep, function (dados) {

        if (!("erro" in dados)) {
            //Atualiza os campos com os valores da consulta.
            $("#txt_logradouro").val(dados.logradouro);
            $("#txt_bairro").val(dados.Bairro);
            $("#txt_estado").val(dados.estado_info.nome);
            $("#txt_logradouro").val(dados.logradouro);
           
        } //end if.
        else {
            //CEP pesquisado não foi encontrado.
           
            alert("CEP não encontrado.");
        }
    });
});



