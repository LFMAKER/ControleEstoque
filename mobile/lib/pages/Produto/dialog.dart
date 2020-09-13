import 'package:flutter/material.dart';
import 'package:http/http.dart';
import 'package:inventory_analytics/api/api_service.dart';
import 'package:inventory_analytics/model/FornecedorModel.dart';
import 'package:inventory_analytics/model/GrupoProdutoModel.dart';
import 'package:inventory_analytics/model/LocalArmazenamentoModel.dart';
import 'package:inventory_analytics/model/MarcaModel.dart';
import 'package:inventory_analytics/model/ProdutoModel.dart';
import 'package:inventory_analytics/model/ProdutoModel.dart';
import 'package:inventory_analytics/model/UnidadeMedidaModel.dart';
import 'package:inventory_analytics/pages/UnidadeMedida/UnidadeMedida.dart';

class DialogProduto extends StatefulWidget {
  final ProdutoModel gp;
  DialogProduto({this.gp});

  @override
  _DialogProdutoState createState() => _DialogProdutoState();
}

class _DialogProdutoState extends State<DialogProduto> {
  final _codigoController = TextEditingController();
  final _nomeController = TextEditingController();
  final _precoCustoController = TextEditingController();
  final _precoVendaController = TextEditingController();
  final _quantEstoqueController = TextEditingController();

  List<UnidadeMedidaModel> unidades = List<UnidadeMedidaModel>();
  List<GrupoProdutoModel> grupos = List<GrupoProdutoModel>();
  List<MarcaProdutoModel> marcas = List<MarcaProdutoModel>();
  List<FornecedorModel> fornecedores = List<FornecedorModel>();
  List<LocalArmazenamentoModel> locais = List<LocalArmazenamentoModel>();

  int _radioValue = 0;
  int posiUnidadeMedida = 1;
  int posiGrupoProduto = 1;
  int posiMarcaProduto = 1;
  int posiFornecedor = 1;
  int posiLocal = 1;
  var readl = true;

  ProdutoModel _produtoAtual = ProdutoModel();

  @override
  void initState() {
    super.initState();

    if (widget.gp != null) {
      _produtoAtual = widget.gp;
      if (_produtoAtual.ativo) {
        _radioValue = 1;
      } else {
        _radioValue = 0;
      }

      readl = true;

      _precoCustoController.text = (_produtoAtual.precoCusto/100).toString();
      _precoVendaController.text = (_produtoAtual.precoVenda/100).toString();
      _quantEstoqueController.text = _produtoAtual.quantEstoque.toString();
    } else {
      readl = false;
      var numero = 0;
      _precoCustoController.text = numero.toString();
      _precoVendaController.text = numero.toString();
      _quantEstoqueController.text = numero.toString();
    }

    _codigoController.text = _produtoAtual.codigo;
    _nomeController.text = _produtoAtual.nome;
  }

  void _handleRadioValueChange(int value) {
    setState(() {
      _radioValue = value;
      switch (_radioValue) {
        case 0:
          _radioValue = value;
          break;
        case 1:
          _radioValue = value;
          break;
      }
    });
  }

  @override
  void dispose() {
    super.dispose();
    _nomeController.clear();
    _precoVendaController.clear();
    _precoCustoController.clear();
    _quantEstoqueController.clear();
  }

  @override
  Widget build(BuildContext context) {
    return AlertDialog(
      contentPadding: EdgeInsets.all(10),
      title: Text(widget.gp == null ? 'Novo Produto' : 'Editar Produto'),
      content: SingleChildScrollView(
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.center,
          mainAxisSize: MainAxisSize.min,
          children: <Widget>[
            TextField(
                controller: _nomeController,
                decoration: InputDecoration(labelText: 'Nome'),
                autofocus: true),
            TextField(
                controller: _codigoController,
                decoration: InputDecoration(labelText: 'Código'),
                autofocus: true),
            TextField(
                controller: _precoCustoController,
                decoration: InputDecoration(labelText: 'Preço Custo'),
                autofocus: true),
            TextField(
                controller: _precoVendaController,
                decoration: InputDecoration(labelText: 'Preço Venda'),
                autofocus: true),
            TextField(
                controller: _quantEstoqueController,
                decoration: InputDecoration(labelText: 'Quantidade Estoque'),
                autofocus: true,
                readOnly: readl ),
            Row(
              children: <Widget>[
                new Radio(
                  value: 1,
                  groupValue: _radioValue,
                  onChanged: _handleRadioValueChange,
                ),
                new Text('Ativado'),
                new Radio(
                  value: 0,
                  groupValue: _radioValue,
                  onChanged: _handleRadioValueChange,
                ),
                new Text('Desativado'),
              ],
            ),
          ],
        ),
      ),
      actions: <Widget>[
        FlatButton(
          child: Text('Cancelar'),
          onPressed: () {
            Navigator.of(context).pop();
          },
        ),
        FlatButton(
          child: Text('Salvar'),
          onPressed: () {
            _produtoAtual.nome = _nomeController.value.text;
            _produtoAtual.codigo = _codigoController.value.text;
            _produtoAtual.precoCusto = double.parse(_precoCustoController.text);
             _produtoAtual.precoVenda = double.parse(_precoVendaController.text);
            _produtoAtual.quantEstoque = int.parse(_quantEstoqueController.value.text);
            _produtoAtual.idUnidadeMedida = posiUnidadeMedida;
            _produtoAtual.idGrupo = posiGrupoProduto;
            _produtoAtual.idMarca = posiMarcaProduto;
            _produtoAtual.idLocalArmazenamento = posiLocal;
            _produtoAtual.idFornecedor = posiFornecedor;

            if (_radioValue == 1) {
              _produtoAtual.ativo = true;
            } else {
              _produtoAtual.ativo = false;
            }

            Navigator.of(context).pop(_produtoAtual);
          },
        ),
      ],
    );
  }
}
