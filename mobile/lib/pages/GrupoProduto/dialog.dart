import 'package:flutter/material.dart';
import 'package:inventory_analytics/model/GrupoProdutoModel.dart';

class DialogGrupoProduto extends StatefulWidget {
  final GrupoProdutoModel gp;
  DialogGrupoProduto({this.gp});

  @override
  _DialogGrupoProdutoState createState() => _DialogGrupoProdutoState();
}

class _DialogGrupoProdutoState extends State<DialogGrupoProduto> {
  final _nomeController = TextEditingController();

  int _radioValue = 0;
  GrupoProdutoModel _grupoAtual = GrupoProdutoModel();

  @override
  void initState() {
    super.initState();

    if (widget.gp != null) {
      _grupoAtual = widget.gp;
      if (_grupoAtual.ativo) {
        _radioValue = 1;
      } else {
        _radioValue = 0;
      }
    }

    _nomeController.text = _grupoAtual.nome;
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
          ? 'Novo Grupo de Produtos'
          : 'Editar Grupo de Produtos'),
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
            _grupoAtual.nome = _nomeController.value.text;
            if (_radioValue == 1) {
              _grupoAtual.ativo = true;
            } else {
              _grupoAtual.ativo = false;
            }

            Navigator.of(context).pop(_grupoAtual);
          },
        ),
      ],
    );
  }
}
