

jQuery(document).ready(function () {

    //Requisitando Ganhos do Mês Atual
    var url_ganhosAtual = urlGanhosAtual;
    $.post(url_ganhosAtual, function (response) {
        if (response) {

            //Formatando o valor para o formato pt-br BRL
            var formatter = new Intl.NumberFormat('pt-BR', {
                style: 'currency',
                currency: 'BRL',
            });

            if (parseFloat(response.ResultadoMes) < 0) {
                $('#ganhoAtualMes').html(formatter.format(response.ResultadoMes));
                $('#CardganhoAtualMes').removeClass('border-left-primary').addClass('border-left-danger');
                $('#CardganhoAtualMes').removeClass('border-left-success').addClass('border-left-danger');
                $('#TituloganhoAtualMes').removeClass('text-primary').addClass('text-danger');
                $('#TituloganhoAtualMes').removeClass('text-success').addClass('text-danger');
            } else {
                $('#ganhoAtualMes').html(formatter.format(response.ResultadoMes));
                $('#CardganhoAtualMes').removeClass('border-left-primary').addClass('border-left-success');
                $('#CardganhoAtualMes').removeClass('border-left-danger').addClass('border-left-success');
                $('#TituloganhoAtualMes').removeClass('text-primary').addClass('text-success');
                $('#TituloganhoAtualMes').removeClass('text-danger').addClass('text-success');

            }

            if (parseFloat(response.ResultadoAno) < 0) {
                $('#ganhoAtualAno').html(formatter.format(response.ResultadoAno));
                $('#CardganhoAtualAno').removeClass('border-left-primary').addClass('border-left-danger');
                $('#CardganhoAtualAno').removeClass('border-left-success').addClass('border-left-danger');
                $('#TituloganhoAtualAno').removeClass('text-primary').addClass('text-danger');
                $('#TituloganhoAtualAno').removeClass('text-success').addClass('text-danger');
            } else {
                $('#ganhoAtualAno').html(formatter.format(response.ResultadoAno));
                $('#CardganhoAtualAno').removeClass('border-left-danger').addClass('border-left-success');
                $('#CardganhoAtualAno').removeClass('border-left-primary').addClass('border-left-success');
                $('#TituloganhoAtualAno').removeClass('text-primary').addClass('text-success');
                $('#TituloganhoAtualAno').removeClass('text-danger').addClass('text-success');

            }


        }
    });


    //Requisitando Entradas e Saídas
    var url_entradas_saidas = urlEntradasESaidas;
    $.post(url_entradas_saidas, function (response) {
        if (response.Entradas != undefined && response.Saidas != undefined) {
            CharsJs(response.Entradas, response.Saidas);
            GoogleCharts(response.Entradas, response.Saidas);
        }

    });


    var formatter = new Intl.NumberFormat('pt-BR', {
        style: 'currency',
        currency: 'BRL',
    });


    var url_dolar = 'http://economia.awesomeapi.com.br/USD-BRL/1?format=json';
    $.get(url_dolar, function (response) {
        $('#dolar').html(formatter.format(response[0].ask));
    });

    //Fazer ainda
    var url_quantidade_produtos 


});


$(document).on('click', '#Autenticar', function () {

    var url_autenticar = urlAutenticar;
    $.post(url_autenticar, function (response) {
        if (response.Status) {
            $('#mensagem_autenticar').html("Autenticado com Sucesso  ");
        }
    });

});



//Gráficos Google Charts
function GoogleCharts(Entradas, Saidas) {
    var dadosEntrada = Entradas;
    var dadosSaidas = Saidas;

    if (dadosEntrada != undefined && dadosSaidas != undefined) {
        google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.setOnLoadCallback(drawChart);

        function drawChart() {
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Mes/Ano');
            data.addColumn('number', 'Entradas (Compras) R$');
            data.addColumn('number', 'Saidas (Vendas) R$');

            for (var i in dadosEntrada) {
                for (var j in dadosSaidas) {
                    var entradaValor = dadosEntrada[i]["total"];
                    var saidaValor = dadosSaidas[j]["total"];
                    var dataConvertEntrada = parseJsonDate(dadosEntrada[i]["Data"]);
                    var dataConvertSaida = parseJsonDate(dadosSaidas[j]["Data"]);
                    if (dataConvertEntrada == dataConvertSaida) {
                        data.addRows([[dataConvertEntrada, entradaValor, saidaValor], ]);
                    }
                }
            }

            //Opções do Gráfico
            var options = {
                title: 'Entradas (Compras) e Saídas (Vendas)',
                curveType: 'function',
                legend: { position: 'bottom' },
                series: {
                    0: { color: '#d21625' },
                    1: { color: '#16d265' },
                }
            };


            var chart = new google.visualization.LineChart(document.getElementById('curve_chart'));

            chart.draw(data, options);
        }
    }

};

//Gráficos Charts.js
function CharsJs(Entradas, Saidas) {
    var dadosEntrada = Entradas;
    var dadosSaidas = Saidas;
    console.log(dadosEntrada);
    console.log(dadosSaidas);

    if (dadosEntrada != undefined && dadosSaidas != undefined) {
        var labels = [];
        var entradas = [];
        var saidas = [];
        for (var i in dadosEntrada) {
            for (var j in dadosSaidas) {

                var entradaValor = dadosEntrada[i]["total"];
                var saidaValor = dadosSaidas[j]["total"];

                var dataConvertEntrada = parseJsonDate(dadosEntrada[i]["Data"]);
                var dataConvertSaida = parseJsonDate(dadosSaidas[j]["Data"]);



                if (dataConvertEntrada == dataConvertSaida) {
                    labels.push(dataConvertEntrada.toString());
                    entradas.push(entradaValor);
                    saidas.push(saidaValor);
                }
            }
        }
        console.log("LABELS");
        console.log(labels);
        console.log("----------");
        console.log("ENTRADAS");
        console.log(entradas);
        console.log("----------");
        console.log("ENTRADAS");
        console.log(saidas);



        renderChart(entradas, saidas, labels);
    }
};



//Renderezição Charts.js
function renderChart(entradas, saidas, labels) {
    var ctx = document.getElementById("myChart").getContext('2d');

    var gradientstrokeSaida = ctx.createLinearGradient(700, 0, 0, 0);
    gradientstrokeSaida.addColorStop(0, '#e8023f');
    gradientstrokeSaida.addColorStop(1, '#ff6a72');

    var gradientfillSaida = ctx.createLinearGradient(700, 0, 0, 0);
    gradientfillSaida.addColorStop(0, 'rgba(232,2,63,1)');
    gradientfillSaida.addColorStop(1, 'rgba(255,106,114,1)');


    var gradientstrokeEntrada = ctx.createLinearGradient(700, 0, 0, 0);
    gradientstrokeEntrada.addColorStop(1, "#5272ce");
    gradientstrokeEntrada.addColorStop(0, "#3B61D1");

    const purple_orange_gradient = ctx.createLinearGradient(700, 0, 0, 0);
    purple_orange_gradient.addColorStop(1, "#5272ce");
    purple_orange_gradient.addColorStop(0, "#3B61D1");

    var myChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,

            datasets: [
           {
               label: 'Entradas',
               data: entradas,
               borderColor: gradientstrokeEntrada,
               backgroundColor: purple_orange_gradient,
               pointBorderColor: '#00000',
               pointBackgroundColor: "#ffffff",
               pointHoverBackgroundColor: gradientstrokeEntrada,
               pointHoverBorderColor: "#00000",
           },
           {
               label: 'Saídas',
               data: saidas,
               borderColor: gradientstrokeSaida,
               backgroundColor: gradientfillSaida,
               pointBorderColor: "#00000",
               pointBackgroundColor: "#ffffff",
               pointHoverBackgroundColor: gradientstrokeSaida,
               pointHoverBorderColor: "#00000",
           },

            ]

        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        callback: function (value) {

                            var formatter = new Intl.NumberFormat('pt-BR', {
                                style: 'currency',
                                currency: 'BRL',
                            });

                            return formatter.format(value);
                        }
                    }
                }]
            }
        }
    });
};











//Deserilização da Data JSON
function parseJsonDate(jsonDate) {
    var offset = new Date().getTimezoneOffset() * 60000;
    var parts = /\/Date\((-?\d+)([+-]\d{2})?(\d{2})?.*/.exec(jsonDate);
    if (parts[2] == undefined) parts[2] = 0;
    if (parts[3] == undefined) parts[3] = 0;
    d = new Date(+parts[1] + offset + parts[2] * 3600000 + parts[3] * 60000);
    date = d.getDate() + 1;
    date = date < 10 ? "0" + date : date;
    mon = d.getMonth() + 1;
    mon = mon < 10 ? "0" + mon : mon;
    year = d.getFullYear();
    return (mon + "/" + year);
};







