import 'package:flutter/material.dart';
import 'package:inventory_analytics/model/UnidadeMedidaModel.dart';

class DialogUnidadeMedida extends StatefulWidget {
  final UnidadeMedidaModel gp;
  DialogUnidadeMedida({this.gp});

  @override
  _DialogUnidadeMedidaState createState() => _DialogUnidadeMedidaState();
}

class _DialogUnidadeMedidaState extends State<DialogUnidadeMedida> {
  final _nomeController = TextEditingController();
  final _siglaController = TextEditingController();

  int _radioValue = 0;
  UnidadeMedidaModel _unidadeAtual = UnidadeMedidaModel();

  @override
  void initState() {
    super.initState();

    if (widget.gp != null) {
      _unidadeAtual = widget.gp;
      if (_unidadeAtual.ativo) {
        _radioValue = 1;
      } else {
        _radioValue = 0;
      }
    }

    _nomeController.text = _unidadeAtual.nome;
    _siglaController.text = _unidadeAtual.sigla;
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
    _siglaController.clear();
  }

  @override
  Widget build(BuildContext context) {
    return AlertDialog(
      title: Text(widget.gp == null
          ? 'Nova Unidade de Medida'
          : 'Editar Unidade de Medida'),
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
                controller: _siglaController,
                decoration: InputDecoration(labelText: 'Sigla'),
                autofocus: true),
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
            _unidadeAtual.nome = _nomeController.value.text;
            _unidadeAtual.sigla = _siglaController.value.text;
            if (_radioValue == 1) {
              _unidadeAtual.ativo = true;
            } else {
              _unidadeAtual.ativo = false;
            }

            Navigator.of(context).pop(_unidadeAtual);
          },
        ),
      ],
    );
  }
}
