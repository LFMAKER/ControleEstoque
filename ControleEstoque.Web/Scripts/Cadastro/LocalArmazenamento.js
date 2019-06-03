/*
Developed by Leonardo Oliveira
Função: Responsável por implementar os dados do CRUD LOCALARMAZENAMENTO no CadBase.JS
Data de criação: 30/04/2019
*/


function set_dados_form(dados) {
    $('#id_cadastro').val(dados.Id);
    $('#txt_nome').val(dados.Nome);
    $('#txt_capacidade_total').val(dados.CapacidadeTotal);
    $('#txt_capacidade_atual').val(dados.CapacidadeAtual);
    $('#cbx_ativo').prop('checked', dados.Ativo);
}

function set_focus_form() {
    $('#txt_nome').focus();
}

function set_dados_grid(dados) {
    
    //var cemPorcento = Number(dados.CapacidadeTotal);
    //var xAtualPorcento = Number((100 * Number(dados.CapacidadeAtual)) / Number(dados.CapacidadeTotal));
    var cemPorcento = dados.CapacidadeTotal;
    var xAtualPorcento = (100 * dados.CapacidadeAtual) / dados.CapacidadeTotal;
    xAtualPorcento = parseFloat(xAtualPorcento).toFixed(2);


    if (xAtualPorcento < 100) {
        xAtualPorcento = xAtualPorcento + '%';  
        return '<td>' + dados.Nome + '</td>' +
               '<td>' + (dados.Ativo ? 'Sim' : 'Não') + '</td>' +
               '<td>' + '<div class="row no-gutters align-items-center">' + '<div class="col-auto"><div class="h5 mb-0 mr-3 font-weight-bold text-gray-800">' + xAtualPorcento + '</div>' + '</div>' + '<div class="col">' + '<div class="progress progress-sm mr-2"><div class="progress-bar bg-warning" role="progressbar" style="width:' + xAtualPorcento + '" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100">' + '</div>' + '</div>' + '</div>' + '</div>' + '</td>';

    } else {

        return '<td>' + dados.Nome + '</td>' +
               '<td>' + (dados.Ativo ? 'Sim' : 'Não') + '</td>' +
               '<td>' + '<div class="row no-gutters align-items-center"><div class="col-auto"><div class="h5 mb-0 mr-3 font-weight-bold text-gray-800">' + xAtualPorcento + '</div></div><div class="col"><div class="progress progress-sm mr-2"><div class="progress-bar bg-danger" role="progressbar" style="width:' + xAtualPorcento + '" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div></div></div></div>' + '</td>';
    }

}

function get_dados_inclusao() {
    return {
        Id: 0,
        Nome: '',
        CapacidadeTotal: 0,
        CapacidadeAtual: 0,
        Ativo: true
    };
}

function get_dados_form() {
    return {
        Id: $('#id_cadastro').val(),
        Nome: $('#txt_nome').val(),
        CapacidadeTotal: $('#txt_capacidade_total').val(),
        CapacidadeAtual: $('#txt_capacidade_atual').val(),
        Ativo: $('#cbx_ativo').prop('checked')
    };
}

function preencher_linha_grid(param, linha) {

    var cemPorcento = param.CapacidadeTotal;
    var xAtualPorcento = (100 * param.CapacidadeAtual) / param.CapacidadeTotal;
    xAtualPorcento = parseFloat(xAtualPorcento).toFixed(2);
    


    if (xAtualPorcento < 100) {
        xAtualPorcento = xAtualPorcento + '%';
        var progress = '<div class="row no-gutters align-items-center">' + '<div class="col-auto"><div class="h5 mb-0 mr-3 font-weight-bold text-gray-800">' + xAtualPorcento + '</div>' + '</div>' + '<div class="col">' + '<div class="progress progress-sm mr-2"><div class="progress-bar bg-warning" role="progressbar" style="width:' + xAtualPorcento + '" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100">' + '</div>' + '</div>' + '</div>' + '</div>';
        linha
        .eq(0).html(param.Nome).end()
        .eq(1).html(param.Ativo ? 'Sim' : 'Não');
        linha.eq(2).html(progress)
    } else {
        xAtualPorcento = xAtualPorcento + '%';
        var progress = '<div class="row no-gutters align-items-center">' + '<div class="col-auto"><div class="h5 mb-0 mr-3 font-weight-bold text-gray-800">' + xAtualPorcento + '</div>' + '</div>' + '<div class="col">' + '<div class="progress progress-sm mr-2"><div class="progress-bar bg-danger" role="progressbar" style="width:' + xAtualPorcento + '" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100">' + '</div>' + '</div>' + '</div>' + '</div>';

        linha
       .eq(0).html(param.Nome).end()
       .eq(1).html(param.Ativo ? 'Sim' : 'Não');
        linha.eq(2).html(progress);
       

    }



};



function verificarDadosValidos() {
    return true;
}
