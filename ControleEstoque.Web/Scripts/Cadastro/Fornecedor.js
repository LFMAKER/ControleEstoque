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
}).on('keyup', '#txt_num_documento', function () {

    if ($('#cbx_pessoa_fisica').is(':checked')) {

        var valido = validarCPF($(this).val())
        if (valido) {
            $(this).removeClass('is-invalid');
            $(this).addClass('is-valid');
        } else {
            $(this).removeClass('is-valid');
            $(this).addClass('is-invalid');
        }
    } else {
        var valido = validarCNPJ($(this).val())
        if (valido) {
            $(this).removeClass('is-invalid');
            $(this).addClass('is-valid');
        } else {
            $(this).removeClass('is-valid');
            $(this).addClass('is-invalid');
        }
    }



}).on('change', '#cbx_pessoa_juridica', function () {
    $('#txt_num_documento').removeClass('is-invalid');
    $('#txt_num_documento').removeClass('is-valid');
    $('#txt_num_documento').val('');
}).on('change', '#cbx_pessoa_fisica', function () {
    $('#txt_num_documento').removeClass('is-invalid');
    $('#txt_num_documento').removeClass('is-valid');
    $('#txt_num_documento').val('');
});



$('#txt_cep').on('blur', function () {
    var cep = $('#txt_cep').val();
    console.log(cep.length);
    $.getJSON("http://api.postmon.com.br/v1/cep/" + cep, function (dados) {

        if (!("erro" in dados)) {
            //Atualiza os campos com os valores da consulta.
            $("#txt_logradouro").val(dados.logradouro);
            $("#txt_bairro").val(dados.bairro);
            $("#txt_cidade").val(dados.cidade);
            $("#txt_estado").val(dados.estado_info.nome);
            $("#txt_pais").val("Brasil");

        } //end if.
        else {
            //CEP pesquisado não foi encontrado.

            alert("CEP não encontrado.");
        }
    });
});

function validarCPF(cpf) {
    cpf = cpf.replace(/[^\d]+/g, '');
    if (cpf == '') return false;
    // Elimina CPFs invalidos conhecidos	
    if (cpf.length != 11 ||
		cpf == "00000000000" ||
		cpf == "11111111111" ||
		cpf == "22222222222" ||
		cpf == "33333333333" ||
		cpf == "44444444444" ||
		cpf == "55555555555" ||
		cpf == "66666666666" ||
		cpf == "77777777777" ||
		cpf == "88888888888" ||
		cpf == "99999999999" ||
		cpf == "12345678909")
        return false;
    // Valida 1o digito	
    add = 0;
    for (i = 0; i < 9; i++)
        add += parseInt(cpf.charAt(i)) * (10 - i);
    rev = 11 - (add % 11);
    if (rev == 10 || rev == 11)
        rev = 0;
    if (rev != parseInt(cpf.charAt(9)))
        return false;
    // Valida 2o digito	
    add = 0;
    for (i = 0; i < 10; i++)
        add += parseInt(cpf.charAt(i)) * (11 - i);
    rev = 11 - (add % 11);
    if (rev == 10 || rev == 11)
        rev = 0;
    if (rev != parseInt(cpf.charAt(10)))
        return false;
    return true;
}


function validarCNPJ(cnpj) {

    cnpj = cnpj.replace(/[^\d]+/g, '');

    if (cnpj == '') return false;

    if (cnpj.length != 14)
        return false;

    // Elimina CNPJs invalidos conhecidos
    if (cnpj == "00000000000000" ||
        cnpj == "11111111111111" ||
        cnpj == "22222222222222" ||
        cnpj == "33333333333333" ||
        cnpj == "44444444444444" ||
        cnpj == "55555555555555" ||
        cnpj == "66666666666666" ||
        cnpj == "77777777777777" ||
        cnpj == "88888888888888" ||
        cnpj == "99999999999999")
        return false;

    // Valida DVs
    tamanho = cnpj.length - 2
    numeros = cnpj.substring(0, tamanho);
    digitos = cnpj.substring(tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != digitos.charAt(0))
        return false;

    tamanho = tamanho + 1;
    numeros = cnpj.substring(0, tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != digitos.charAt(1))
        return false;

    return true;

}