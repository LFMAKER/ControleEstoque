import 'package:flutter/material.dart';
import 'package:inventory_analytics/model/FornecedorModel.dart';

class DialogFornecedor extends StatefulWidget {
  final FornecedorModel gp;
  DialogFornecedor({this.gp});

  @override
  _DialogFornecedorState createState() => _DialogFornecedorState();
}

class _DialogFornecedorState extends State<DialogFornecedor> {
  final _nomeController = TextEditingController();
  final _numDocumentoController = TextEditingController();
  final _telefoneController = TextEditingController();
  final _logradouroController = TextEditingController();
  final _numeroController = TextEditingController();
  final _cepController = TextEditingController();
  final _paisController = TextEditingController();

  int _radioValue = 0;
  FornecedorModel _fornecedor = FornecedorModel();

  @override
  void initState() {
    super.initState();

    if (widget.gp != null) {
      _fornecedor = widget.gp;
      if (_fornecedor.ativo) {
        _radioValue = 1;
      } else {
        _radioValue = 0;
      }

      

     
    }

    _nomeController.text = _fornecedor.nome;
    _numDocumentoController.text = _fornecedor.numDocumento;
    _telefoneController.text = _fornecedor.telefone;
    _logradouroController.text = _fornecedor.logradouro;
    _numeroController.text = _fornecedor.numero;
    _cepController.text = _fornecedor.cep;
    _paisController.text = _fornecedor.pais;
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
    _numDocumentoController.clear();
    _telefoneController.clear();
    _logradouroController.clear();
    _numeroController.clear();
    _cepController.clear();
    _paisController.clear();
  }

  @override
  Widget build(BuildContext context) {
    return AlertDialog(
      title: Text(widget.gp == null ? 'Novo Fornecedor' : 'Editar Fornecedor'),
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
                controller: _numDocumentoController,
                decoration: InputDecoration(labelText: 'Documento'),
                autofocus: true),
           
            TextField(
                controller: _telefoneController,
                decoration: InputDecoration(labelText: 'Telefone'),
                autofocus: true),
            TextField(
                controller: _logradouroController,
                decoration: InputDecoration(labelText: 'Logradouro'),
                autofocus: true),
            TextField(
                controller: _numeroController,
                decoration: InputDecoration(labelText: 'Número'),
                autofocus: true),
            TextField(
                controller: _cepController,
                decoration: InputDecoration(labelText: 'CEP'),
                autofocus: true),
            TextField(
                controller: _paisController,
                decoration: InputDecoration(labelText: 'Pais'),
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
            _fornecedor.nome = _nomeController.value.text;
            _fornecedor.numDocumento = _numDocumentoController.value.text;
            _fornecedor.telefone = _telefoneController.value.text;
            _fornecedor.logradouro = _logradouroController.value.text;
            _fornecedor.numero = _numeroController.value.text;
            _fornecedor.cep = _cepController.value.text;
            _fornecedor.pais = _paisController.value.text;

            if (_radioValue == 1) {
              _fornecedor.ativo = true;
            } else {
              _fornecedor.ativo = false;
            }

            

            Navigator.of(context).pop(_fornecedor);
          },
        ),
      ],
    );
  }
}
