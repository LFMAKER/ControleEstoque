function set_dados_form(dados) {
    $('#id_cadastro').val(dados.Id);
    $('#txt_codigo').val(dados.Codigo);
    $('#txt_nome').val(dados.Nome);
    $('#txt_preco_custo').val(dados.PrecoCusto);
    $('#txt_preco_venda').val(dados.PrecoVenda);
    $('#txt_quant_estoque').val(dados.QuantEstoque);
    $('#ddl_unidade_medida').val(dados.IdUnidadeMedida);
    $('#ddl_grupo').val(dados.IdGrupo);
    $('#ddl_marca').val(dados.IdMarca);
    $('#ddl_fornecedor').val(dados.IdFornecedor);
    $('#ddl_local_armazenamento').val(dados.IdLocalArmazenamento);
    $('#cbx_ativo').prop('checked', dados.Ativo);
}

function set_focus_form() {
    var alterando = (parseInt($('#id_cadastro').val()) > 0);
    $('#txt_quant_estoque').attr('disabled', alterando);
    $('#ddl_local_armazenamento').attr('disabled', alterando);

    $('#txt_codigo').focus();
}

function get_dados_inclusao() {
    return {
        Id: 0,
        Codigo: '',
        Nome: '',
        PrecoCusto: 0,
        PrecoVenda: 0,
        QuantEstoque: 0,
        IdUnidadeMedida: 0,
        IdGrupo: 0,
        IdMarca: 0,
        IdFornecedor: 0,
        IdLocalArmazenamento: 0,
        Ativo: true
    };
}

function get_dados_form() {
    return {
        Id: $('#id_cadastro').val(),
        Codigo: $('#txt_codigo').val(),
        Nome: $('#txt_nome').val(),
        PrecoCusto: $('#txt_preco_custo').val().replace(".","").replace(",",""),
        PrecoVenda: $('#txt_preco_venda').val().replace(".", "").replace(",", ""),
        QuantEstoque: $('#txt_quant_estoque').val(),
        IdUnidadeMedida: $('#ddl_unidade_medida').val(),
        IdGrupo: $('#ddl_grupo').val(),
        IdMarca: $('#ddl_marca').val(),
        IdFornecedor: $('#ddl_fornecedor').val(),
        IdLocalArmazenamento: $('#ddl_local_armazenamento').val(),
        Ativo: $('#cbx_ativo').prop('checked')
    };
}

function set_dados_grid(dados) {
    return '<td>' + dados.Codigo + '</td>' +
            '<td>' + dados.Nome + '</td>' +
            '<td>' + dados.QuantEstoque + '</td>' +
           '<td>' + (dados.Ativo ? 'Sim' : 'Não') + '</td>';
}


function preencher_linha_grid(param, linha) {
    linha
        .eq(0).html(param.Codigo).end()
        .eq(1).html(param.Nome).end()
        .eq(2).html(param.QuantEstoque).end()
        .eq(3).html(param.Ativo ? 'Sim' : 'Não');
}

$(document)
.ready(function () {
    $('#txt_preco_custo,#txt_preco_venda').mask('#.##0,00', { reverse: true });
    $('#txt_quant_estoque').mask('00000');
});

$(document).on('click', '#btn_incluir', function () {
    $('#txt_preco_custo,#txt_preco_venda').mask('#.##0,00', { reverse: true });
    $('#txt_quant_estoque').mask('00000');
})

function verificarDadosValidos() {
    return true;
}
