/*
Developed by Leonardo Oliveira
Função: Servir de template para realizar todas calls dos cruds.
Data de criação: 30/04/2019
*/


//anti_forgery_token
function add_anti_forgery_token(data) {
    data.__RequestVerificationToken = $('[name=__RequestVerificationToken]').val();
    return data;
}

//Formanta a mensagem e avisos
function formatar_mensagem_aviso(mensagens) {
    var ret = '';
    for (var i = 0; i < mensagens.length; i++) {
        ret += '<li>' + mensagens[i] + '</li>';
    }
    return '<ul>' + ret + '</ul>';
}


//Função responsável por abrir o form do crud
function abrir_form(dados) {
    set_dados_form(dados);

    var modal_cadastro = $('#modal_cadastro');
    $('#msg_mensagem_aviso').empty();
    $('#msg_aviso').hide();
    $('#msg_mensagem_aviso').hide();
    $('#msg_erro').hide();
    bootbox.dialog({
        title: 'Cadastro de ' + titulo_pagina,
        message: modal_cadastro,
        className: 'dialogo',
    })
    .on('shown.bs.modal', function () {
        modal_cadastro.show(0, function () {
            set_focus_form();

        });
    })
    .on('hidden.bs.modal', function () {
        modal_cadastro.hide().appendTo('body');
    });
}


//Função responsável por criar linha na tabela
function criar_linha_grid(dados) {
    var result = set_dados_grid(dados);
    var ret =
         '<tr data-id=' + dados.Id + '>' +
        result +
        '<td>' +
        '<a class="btn btn-primary btn-alterar" style="margin-right: 3px; color: white;" role="button" >Alterar</a>' +
        '<a class="btn btn-danger btn-excluir" role="button" style="margin-right: 3px; color: white;"> Excluir</a>' +
        '</td>' +
        '</tr>';
    return ret;
}


$(document).on('click', '#btn_incluir', function () {
    abrir_form(get_dados_inclusao());
})
.on('click', '.btn-alterar', function () {
    var btn = $(this),
        id = btn.closest('tr').attr('data-id'),
        url = url_alterar,
        param = { 'id': id };
    $.post(url, add_anti_forgery_token(param), function (response) {
        if (response) {
            abrir_form(response);
        }

    })
     .fail(function () {
         swal('Aviso', 'Não foi possível recuperar as informações. Tente novamente em instantes.', 'warning');
     });
})
.on('click', '.btn-excluir', function () {

    //o que será enviado no post
    var btn = $(this),
    tr = btn.closest('tr'),
    id = tr.attr('data-id'),
    url = url_exclusao,
    param = { 'id': id };

    //Dialog
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: true,

    })
    swalWithBootstrapButtons.fire({
        title: 'Você tem certeza?',
        text: "Você não poderá desfazer isso!",
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Excluir',
        cancelButtonText: 'Cancelar',
        reverseButtons: true
    }).then((result) => {
        if (result.value) {
            //Realizar POST 
            $.post(url, add_anti_forgery_token(param), function (response) {
                if (response) {
                    tr.remove();
                    var quant = $('#grid_cadastro > tbody > tr').length;
                    if (quant == 0) {
                        $('#grid_cadastro').addClass('invisivel');
                        $('#mensagem_grid').removeClass('invisivel');
                    }


                }
            }).fail(function () {
                swal('Aviso', 'Não foi possível excluir as informações. Tente novamente em instantes.', 'warning');
            })
            swalWithBootstrapButtons.fire(
             'Deletado!',
             titulo_pagina + ' foi deletado com sucesso.',
             'success'
             )


        } else if (
            // Read more about handling dismissals
          result.dismiss === Swal.DismissReason.cancel
        ) {
            swalWithBootstrapButtons.fire(
              'Cancelado',
              titulo_pagina + ' está seguro :)',
              'error'
            )
        }
    })
})



.on('click', '#btn_confirmar', function () {
    var btn = $(this),
        url = url_confirmar,
        param = get_dados_form();
    console.log(param);
    $.post(url, add_anti_forgery_token(param), function (response) {
        if (response.Resultado == "OK") {
            if (param.Id == 0) {
                param.Id = response.IdSalvo;
                var table = $('#grid_cadastro').find('tbody'),
                    linha = criar_linha_grid(param);
                table.append(linha);
                $('#grid_cadastro').removeClass('invisivel');
                $('#mensagem_grid').addClass('invisivel');

                Swal.fire({
                    type: 'success',
                    title: titulo_pagina + ' foi incluído(a) com sucesso!',
                    showConfirmButton: false,
                    timer: 1500
                })

            }
            else {
                var linha = $('#grid_cadastro').find('tr[data-id=' + param.Id + ']').find('td');
                preencher_linha_grid(param, linha);
                Swal.fire({
                    type: 'success',
                    title: titulo_pagina + ' foi alterado(a) com sucesso!',
                    showConfirmButton: false,
                    timer: 1500
                })

            }
            $('#modal_cadastro').parents('.bootbox').modal('hide');
        }
        else if (response.Resultado == "ERRO") {
            $('#msg_aviso').hide();
            $('#msg_mensagem_aviso').hide();
            $('#msg_erro').show();
        }
        else if (response.Resultado == "AVISO") {
            $('#msg_mensagem_aviso').html(formatar_mensagem_aviso(response.Mensagens));
            $('#msg_aviso').show();
            $('#msg_mensagem_aviso').show();
            $('#msg_erro').hide();
        }


    }).fail(function () {
        swal('Aviso', 'Não foi possível salvar as informações. Tente novamente em instantes.', 'warning');
    });
})
.on('click', '.page-item', function () {
    var btn = $(this),
        filtro = $('#txt_filtro'),
        tamPag = $('#ddl_tam_pag').val(),
        pagina = btn.text(),
        url = url_page_click,
        param = { 'pagina': pagina, 'tamPag': tamPag, 'filtro': filtro.val() };
    //Realizando POST

    $.post(url, add_anti_forgery_token(param), function (response) {
        if (response) {
            var table = $('#grid_cadastro').find('tbody');
            table.empty();

            if (response.length > 0) {
                $('#grid_cadastro').removeClass('invisivel');
                $('#mensagem_grid').addClass('invisivel');

                for (var i = 0; i < response.length; i++) {
                    table.append(criar_linha_grid(response[i]));
                }
            }
            else {
                $('#grid_cadastro').addClass('invisivel');
                $('#mensagem_grid').removeClass('invisivel');
            }

            btn.siblings().removeClass('active');
            btn.addClass('active');

        }

    }).fail(function () {
        swal('Aviso', 'Não foi possível recuperar as informações. Tente novamente em instantes.', 'warning');
    });


})
.on('change', '#ddl_tam_pag', function () {
    var ddl = $(this),
        filtro = $('#txt_filtro'),
        tamPag = ddl.val(),
        pagina = 1,
        url = url_ddl_tam_pag,
        param = { 'pagina': pagina, 'tamPag': tamPag, 'filtro': filtro.val() };

    $.post(url, add_anti_forgery_token(param), function (response) {
        if (response) {
            var table = $('#grid_cadastro').find('tbody');

            table.empty();
            if (response.length > 0) {
                $('#grid_cadastro').removeClass('invisivel');
                $('#mensagem_grid').addClass('invisivel');

                for (var i = 0; i < response.length; i++) {
                    table.append(criar_linha_grid(response[i]));
                }
            }
            else {
                $('#grid_cadastro').addClass('invisivel');
                $('#mensagem_grid').removeClass('invisivel');
            }

            ddl.siblings().removeClass('active');
            ddl.addClass('active');
        }
    }).fail(function () {
        swal('Aviso', 'Não foi possível recuperar as informações. Tente novamente em instantes.', 'warning');
    });
}).on('keyup', '#txt_filtro', function () {
    var filtro = $(this),
        ddl = $('#ddl_tam_pag'),
        tamPag = ddl.val(),
        pagina = 1,
        url = url_filtro_change,
        param = { 'pagina': pagina, 'tamPag': tamPag, 'filtro': filtro.val() };
    //Realizando POST
    $.post(url, add_anti_forgery_token(param), function (response) {
        if (response) {
            var table = $('#grid_cadastro').find('tbody');
            table.empty();

            if (response.length > 0) {
                $('#grid_cadastro').removeClass('invisivel');
                $('#mensagem_grid').addClass('invisivel');

                for (var i = 0; i < response.length; i++) {
                    table.append(criar_linha_grid(response[i]));

                }
            }
            else {
                $('#grid_cadastro').addClass('invisivel');
                $('#mensagem_grid').removeClass('invisivel');
            }


            ddl.siblings().removeClass('active');
            ddl.addClass('active');

        }

    }).fail(function () {
        swal('Aviso', 'Não foi possível recuperar as informações. Tente novamente em instantes.', 'warning');
    });
});
