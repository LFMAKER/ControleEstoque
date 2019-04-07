function set_dados_form(dados) {
    $('#id_cadastro').val(dados.Id);
    $('#txt_nome').val(dados.Nome);
    $('#cbx_ativo').prop('checked', dados.Ativo);
}

function set_focus_form() {
    $('#txt_nome').focus();
}

function get_dados_inclusao() {
    return {
        Id: 0,
        Nome: '',
        Ativo: true
    };
}

function get_dados_form() {
    return {
        Id: $('#id_cadastro').val(),
        Nome: $('#txt_nome').val(),
        Ativo: $('#cbx_ativo').prop('checked')
    };
}

function preencher_linha_grid(param, linha) {
    linha
        .eq(0).html(param.Nome).end()
        .eq(1).html(param.Ativo ? 'SIM' : 'NÃO');
}

function set_dados_grid(dados) {
    return '<td>' + dados.Nome + '</td>' +
           '<td>' + (dados.Ativo ? 'SIM' : 'NÃO') + '</td>';
}


//Função responsável por validações visuais
function validationForms() {

    //Validação checkbox
    $("input[name=cbx_ativo]").change(function () {
        // verifica se foi selecionado
        if ($(this).is(':checked')) {
            var caixaTexto = $("#caixa").text();
            var result = (caixaTexto.length == 0);
            if (result == false) {
                $('#caixa').empty();
                $("#caixa").append("O grupo de produtos está ativo!");
            } else {
                $("#caixa").append("O grupo de produtos está ativo!");
            }
        } else {
            var caixaTexto = $("#caixa").text();
            var result = (caixaTexto.length == 0);
            if (result == false) {
                $('#caixa').empty();
                $("#caixa").append("O grupo de produtos não está ativo!");
            } else {
                $("#caixa").append("O grupo de produtos não está ativo!");
            }
        };
    });

    //Validação Input Nome
    $("input[name=txt_nome]").blur(function () {

        var text_name = $("#txt_nome").val();
        if (text_name.length < 3) {
            $("#txt_nome").addClass("form-control is-invalid");
            $('#caixa-texto').empty();
            $("#caixa-texto").append("O nome do grupo de produtos deve possuir no mínimo 3 caracteres!");
        } else {
            var nomeClasse = $("#txt_nome").hasClass();
            console.log(nomeClasse);
            $("#txt_nome").removeClass("form-control is-invalid").addClass("form-control is-valid");
            $('#caixa-texto').empty();
        }
    });
}

