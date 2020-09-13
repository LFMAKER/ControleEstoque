import 'package:flutter/material.dart';
import 'package:inventory_analytics/api/api_service.dart';
import 'package:inventory_analytics/model/FornecedorModel.dart';
import 'package:inventory_analytics/pages/Fornecedor/dialog.dart';

class Fornecedor extends StatefulWidget {
  @override
  _FornecedorState createState() => new _FornecedorState();
}

class _FornecedorState extends State<Fornecedor> with TickerProviderStateMixin {
  List<FornecedorModel> _Fornecedor = new List();

  @override
  void initState() {
    super.initState();
    carregar();
    setState(() {});
  }

/* Remove the data from the List DataSource */
  void _removeFornecedor(int index, FornecedorModel gp) async {
    setState(() {
      _Fornecedor.removeAt(index);
      Services.DeleteFornecedor(gp.id);
    });
  }

  void carregar() async {
    _Fornecedor = await Services.getFornecedores();
    setState(() {});
  }

/* Give a background to the Swipe Delete as a indicator to Delete */
  Widget refreshBgDelete() {
    return Container(
      alignment: Alignment.centerRight,
      padding: EdgeInsets.only(right: 20.0),
      color: Colors.red,
      child: const Icon(
        Icons.delete,
        color: Colors.white,
      ),
    );
  }

  Widget refreshBgEdit() {
    return Container(
      alignment: Alignment.centerRight,
      padding: EdgeInsets.only(right: 20.0),
      color: Colors.yellow,
      child: const Icon(
        Icons.edit,
        color: Colors.white,
      ),
    );
  }

  Widget list() {
    if (_Fornecedor.length > 0) {
      return ListView.builder(
        itemCount: _Fornecedor.length,
        itemBuilder: (BuildContext context, int index) {
          return row(context, index);
        },
      );
    }
  }

  Widget row(context, index) {
    var result;
    return Dismissible(
      key: Key(UniqueKey().toString()), // UniqueKey().toString()
      onDismissed: (direction) {
        var result = _Fornecedor.elementAt(index);

        if (direction == DismissDirection.startToEnd) {
          _editarFornecedor(gpEditar: result, index: index);
        } else if (direction == DismissDirection.endToStart) {
          _removeFornecedor(index, result);
        }
      },
      background: refreshBgEdit(),
      secondaryBackground: refreshBgDelete(),
      child: Card(
        margin: EdgeInsets.all(10),
        child: ExpansionTile(
          title: Center(child: Text(_Fornecedor[index].nome)),
          children: <Widget>[
            ListTile(
              title: Text(
                "Id: " + _Fornecedor[index].id.toString(),
                textAlign: TextAlign.left,
              ),
            ),
            ListTile(
              title: Text(
                "Nome: " + _Fornecedor[index].nome.toString(),
                textAlign: TextAlign.left,
              ),
            ),
            ListTile(
              title: Text(
                "Documento: " + _Fornecedor[index].numDocumento.toString(),
                textAlign: TextAlign.left,
              ),
            ),
            ListTile(
              title: Text(
                "Telefone: " + _Fornecedor[index].telefone.toString(),
                textAlign: TextAlign.left,
              ),
            ),
            ListTile(
              title: Text(
                "Endereço: " +
                    _Fornecedor[index].logradouro.toString() +
                    ", " +
                    _Fornecedor[index].numero.toString(),
                textAlign: TextAlign.left,
              ),
            ),
            ListTile(
              title: Text(
                "CEP: " + _Fornecedor[index].cep.toString(),
                textAlign: TextAlign.left,
              ),
            ),
            ListTile(
              title: Text(
                "País: " + _Fornecedor[index].pais.toString(),
                textAlign: TextAlign.left,
              ),
            ),
            ListTile(
              title: Text(
                "Status: " +
                    _Fornecedor[index]
                        .ativo
                        .toString()
                        .replaceAll("true", "Ativado")
                        .replaceAll("false", "Desativado"),
                textAlign: TextAlign.left,
              ),
            ),
          ],
        ),
      ),
    );
  }

  void _editarFornecedor({FornecedorModel gpEditar, int index}) async {
    final gpEd = await showDialog<FornecedorModel>(
      context: context,
      barrierDismissible: false,
      builder: (BuildContext context) {
        return DialogFornecedor(gp: gpEditar);
      },
    );

    if (gpEd != null) {
      setState(() {
        if (index == null) {
          _Fornecedor.add(gpEd);
          Services.AddFornecedor(gpEd.nome, gpEd.numDocumento, gpEd.telefone,
              gpEd.logradouro, gpEd.numero, gpEd.cep, gpEd.pais, gpEd.ativo);
          list();
          setState(() {});
        } else {
          _Fornecedor[index] = gpEd;
          Services.UpdateFornecedor(
              gpEd.id,
              gpEd.nome,
              gpEd.razaoSocial,
              gpEd.numDocumento,
              gpEd.tipo,
              gpEd.telefone,
              gpEd.contato,
              gpEd.logradouro,
              gpEd.numero,
              gpEd.complemento,
              gpEd.cep,
              gpEd.bairro,
              gpEd.cidade,
              gpEd.estado,
              gpEd.pais,
              gpEd.ativo);
          list();
          setState(() {});
        }
      });
    } else {
      list();
      setState(() {});
    }
  }

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: Scaffold(
        appBar: new AppBar(
          backgroundColor: Colors.white,
          centerTitle: true,
          toolbarOpacity: 0.8,
          title: new Text(
            'Fornecedores',
            style: TextStyle(color: Colors.deepPurple),
          ),
          leading: IconButton(
            icon: new Icon(
              Icons.arrow_back,
              color: Colors.deepPurpleAccent,
              size: 35,
            ),
            onPressed: () => Navigator.of(context).pop(null),
          ),
        ),
        body: Container(
          color: Colors.deepPurple,
          child: list(),
        ),
        floatingActionButton: FloatingActionButton(
          onPressed: _editarFornecedor,
          child: Icon(Icons.add),
          backgroundColor: Colors.green,
        ),
      ),
    );
  }
}
