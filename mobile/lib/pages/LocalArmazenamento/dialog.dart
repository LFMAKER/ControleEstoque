import 'package:flutter/material.dart';
import 'package:inventory_analytics/model/LocalArmazenamentoModel.dart';

class DialogLocalArmazenamento extends StatefulWidget {
  final LocalArmazenamentoModel gp;
  DialogLocalArmazenamento({this.gp});

  @override
  _DialogLocalArmazenamentoState createState() => _DialogLocalArmazenamentoState();
}

class _DialogLocalArmazenamentoState extends State<DialogLocalArmazenamento> {
  final _nomeController = TextEditingController();
  final _capacidadeTotalController = TextEditingController();
  final _capacidadeAtualController = TextEditingController();

  int _radioValue = 0;

  LocalArmazenamentoModel _localAtual = LocalArmazenamentoModel();

  @override
  void initState() {
    super.initState();

    if (widget.gp != null) {
      _localAtual = widget.gp;
      if (_localAtual.ativo) {
        _radioValue = 1;
      } else {
        _radioValue = 0;
      }
    }else{
      _localAtual.capacidadeAtual = 0;
      _localAtual.capacidadeTotal = 0;
    
    }

    

    _nomeController.text = _localAtual.nome;
    _capacidadeAtualController.text = _localAtual.capacidadeAtual.toString();
    _capacidadeTotalController.text = _localAtual.capacidadeTotal.toString();

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
    _capacidadeAtualController.clear();
    _capacidadeTotalController.clear();
  }

  @override
  Widget build(BuildContext context) {
    return AlertDialog(
      title: Text(widget.gp == null
          ? 'Novo Local de Armazenamento'
          : 'Editar Local de Armazenamento'),
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
                controller: _capacidadeAtualController,
                decoration: InputDecoration(labelText: 'Capacidade Atual'),
                readOnly: true,
                autofocus: true),
                 TextField(
                controller: _capacidadeTotalController,
                decoration: InputDecoration(labelText: 'Capacidade Total'),
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
            _localAtual.nome = _nomeController.value.text;
            _localAtual.capacidadeAtual = int.parse(_capacidadeAtualController.value.text);
            _localAtual.capacidadeTotal = int.parse(_capacidadeTotalController.value.text);
            if (_radioValue == 1) {
              _localAtual.ativo = true;
            } else {
              _localAtual.ativo = false;
            }

            Navigator.of(context).pop(_localAtual);
          },
        ),
      ],
    );
  }
}
