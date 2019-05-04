function set_dados_form(dados) {
    $('#id_cadastro').val(dados.Id);
    $('#txt_nome').val(dados.Nome);
    $('#txt_num_documento').val(dados.NumDocumento);
    $('#txt_razao_social').val(dados.RazaoSocial);
    $('#txt_telefone').val(dados.Telefone);
    $('#txt_contato').val(dados.Contato);
    $('#txt_logradouro').val(dados.Logradouro);
    $('#txt_complemento').val(dados.Complemento);
    $('#txt_cep').val(dados.Cep);
    $('#ddl_pais').val(dados.IdPais);
    $('#ddl_estado').val(dados.IdEstado);
    $('#ddl_cidade').val(dados.IdCidade);
    $('#cbx_ativo').prop('checked', dados.Ativo);
    $('#cbx_pessoa_juridica').prop('checked', false);
    $('#cbx_pessoa_fisica').prop('checked', false);

    if (dados.Tipo == 2) {
        $('#cbx_pessoa_juridica').prop('checked', true).trigger('click');
    }
    else {
        $('#cbx_pessoa_fisica').prop('checked', true).trigger('click');
    }

    $('#ddl_estado').prop('disabled', dados.IdEstado <= 0 || dados.IdEstado == undefined);
    $('#ddl_cidade').prop('disabled', dados.IdCidade <= 0 || dados.IdCidade == undefined);
    

    //Req Estado
    var ddl_pais = $('#ddl_pais'),
       id_pais = parseInt(ddl_pais.val()),
       ddl_estado = $('#ddl_estado');

    if (id_pais > 0) {
        var url = url_listar_estados,
            param = { idPais: id_pais };

        ddl_estado.empty();
        $('#ddl_estado').prop('disabled', true);

        $.post(url, add_anti_forgery_token(param), function (response) {
            if (response && response.length > 0) {
                for (var i = 0; i < response.length; i++) {
                    if (response[i].Id == dados.IdEstado) {
                        ddl_estado.append('<option value=' + response[i].Id + ' selected>' + response[i].Nome + '</option>');
                    } else {
                        ddl_estado.append('<option value=' + response[i].Id + '>' + response[i].Nome + '</option>');
                    }

                }
                $('#ddl_estado').prop('disabled', false);
                
            }
        })
        
    }
    setTimeout(function () {  
  
    //Req Estado
    var ddl_estado = $('#ddl_estado'),
       id_estado = parseInt(ddl_estado.val()),
       ddl_cidade = $('#ddl_cidade');

    console.log(id_estado);
    if (id_estado > 0) {
        var url = url_listar_cidades,
            param = { idEstado: id_estado};

        ddl_cidade.empty();
        $('#ddl_cidade').prop('disabled', true);

        $.post(url, add_anti_forgery_token(param), function (response) {
            if (response && response.length > 0) {
                for (var i = 0; i < response.length; i++) {
                    if (response[i].Id == dados.IdCidade) {
                        ddl_cidade.append('<option value=' + response[i].Id + ' selected>' + response[i].Nome + '</option>');
                    } else {
                        ddl_cidade.append('<option value=' + response[i].Id + '>' + response[i].Nome + '</option>');
                    }

                }
                $('#ddl_cidade').prop('disabled', false);
            }
        }).fail(function () {
            swal('Aviso', 'Não foi possível recuperar as informações. Tente novamente em instantes.', 'warning');
        });
    }
    }, 1000);


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
        Complemento: '',
        Cep: '',
        IdPais: 0,
        IdEstado: 0,
        IdCidade: 0,
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
        Complemento: $('#txt_complemento').val(),
        Cep: $('#txt_cep').val(),
        IdPais: $('#ddl_pais').val(),
        IdEstado: $('#ddl_estado').val(),
        IdCidade: $('#ddl_cidade').val(),
        Ativo: $('#cbx_ativo').prop('checked')
    };
}

function preencher_linha_grid(param, linha) {
    linha
        .eq(0).html(param.Nome).end()
        .eq(1).html(param.Telefone).end()
        .eq(1).html(param.Contato).end()
        .eq(2).html(param.Ativo ? 'Sim' : 'Não');
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
})
.on('change', '#ddl_pais', function () {
    var ddl_pais = $(this),
        id_pais = parseInt(ddl_pais.val()),
        ddl_estado = $('#ddl_estado');

    if (id_pais > 0) {
        var url = url_listar_estados,
            param = { idPais: id_pais };

        ddl_estado.empty();
        ddl_estado.prop('disabled', true);

        $.post(url, add_anti_forgery_token(param), function (response) {
            if (response && response.length > 0) {
                for (var i = 0; i < response.length; i++) {
                    ddl_estado.append('<option value=' + response[i].Id + '>' + response[i].Nome + '</option>');
                }
                ddl_estado.prop('disabled', false);
            }
        });
    }
})
.on('change', '#ddl_estado', function () {
    var ddl_estado = $(this),
        id_estado = parseInt(ddl_estado.val()),
        ddl_cidade = $('#ddl_cidade');
    console.log("OPA");

    if (id_estado > 0) {
        var url = url_listar_cidades,
            param = { idEstado: id_estado };

        ddl_cidade.empty();
        ddl_cidade.prop('disabled', true);

        $.post(url, add_anti_forgery_token(param), function (response) {
            if (response && response.length > 0) {
                for (var i = 0; i < response.length; i++) {
                    ddl_cidade.append('<option value=' + response[i].Id + '>' + response[i].Nome + '</option>');
                }
                ddl_cidade.prop('disabled', false);
            }
        });
    }
});


$('#txt_cep').on('blur', function () {

    if ($.trim($("#txt_cep").val()) != "") {

        $("#mensagem").html('(Aguarde, consultando CEP ...)');
        $.getScript("http://cep.republicavirtual.com.br/web_cep.php?formato=javascript&cep=" + $("#txt_cep").val(), function () {

            if (resultadoCEP["resultado"]) {
                $("#txt_logradouro").val(unescape(resultadoCEP["tipo_logradouro"]) + " " + unescape(resultadoCEP["logradouro"]));
            }

            $("#mensagem").html('');
        });
    }
});



