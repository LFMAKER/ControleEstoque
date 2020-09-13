import 'package:flutter/material.dart';
import 'package:inventory_analytics/model/MarcaModel.dart';

class DialogMarcaProduto extends StatefulWidget {
  final MarcaProdutoModel gp;
  DialogMarcaProduto({this.gp});

  @override
  _DialogMarcaProdutoState createState() => _DialogMarcaProdutoState();
}

class _DialogMarcaProdutoState extends State<DialogMarcaProduto> {
  final _nomeController = TextEditingController();

  int _radioValue = 0;
  MarcaProdutoModel _marcaAtual = MarcaProdutoModel();

  @override
  void initState() {
    super.initState();

    if (widget.gp != null) {
      _marcaAtual = widget.gp;
      if (_marcaAtual.ativo) {
        _radioValue = 1;
      } else {
        _radioValue = 0;
      }
    }

    _nomeController.text = _marcaAtual.nome;
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
  }

  @override
  Widget build(BuildContext context) {
    return AlertDialog(
      title: Text(widget.gp == null
          ? 'Nova Marca de Produtos'
          : 'Editar Marca de Produtos'),
      content: SingleChildScrollView(
              child: Column(
          crossAxisAlignment: CrossAxisAlignment.center,
          mainAxisSize: MainAxisSize.min,
          children: <Widget>[
            TextField(
                controller: _nomeController,
                decoration: InputDecoration(labelText: 'Nome'),
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
            _marcaAtual.nome = _nomeController.value.text;
            if (_radioValue == 1) {
              _marcaAtual.ativo = true;
            } else {
              _marcaAtual.ativo = false;
            }

            Navigator.of(context).pop(_marcaAtual);
          },
        ),
      ],
    );
  }
}
