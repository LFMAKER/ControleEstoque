
import 'package:flutter/material.dart';
import 'package:inventory_analytics/api/api_service.dart';
import 'package:inventory_analytics/model/MarcaModel.dart';
import 'package:inventory_analytics/pages/MarcaProduto/dialog.dart';

class MarcaProduto extends StatefulWidget {
  @override
  _MarcaProdutoState createState() => new _MarcaProdutoState();
}

class _MarcaProdutoState extends State<MarcaProduto>
    with TickerProviderStateMixin {
  List<MarcaProdutoModel> _MarcaProduto = new List();

  @override
  void initState() {
    super.initState();
    carregar();
    setState(() {
      
    });
  }



/* Remove the data from the List DataSource */
  void _removeGrupos(int index, MarcaProdutoModel gp) async {
    setState(() {
      _MarcaProduto.removeAt(index);
      Services.DeleteMarcaProduto(gp.id);
    });
  }

  void carregar() async{
    _MarcaProduto = await Services.getMarcasProduto();
    setState(() {
      
    });
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
    if (_MarcaProduto.length > 0) {
      return ListView.builder(
        itemCount: _MarcaProduto.length,
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
        var result = _MarcaProduto.elementAt(index);

        if (direction == DismissDirection.startToEnd) {
          _editarGrupo(gpEditar: result, index: index);
        } else if (direction == DismissDirection.endToStart) {
          _removeGrupos(index, result);
        }
      },
      background: refreshBgEdit(),
      secondaryBackground: refreshBgDelete(),
      child: Card(
        margin: EdgeInsets.all(10),
        child: ExpansionTile(
          title: Center(child: Text(_MarcaProduto[index].nome)),
          children: <Widget>[
            ListTile(
              title: Text(
                "Id: " + _MarcaProduto[index].id.toString(),
                textAlign: TextAlign.left,
              ),
            ),
            ListTile(
              title: Text(
                "Status: " +
                    _MarcaProduto[index]
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
  
 void _editarGrupo({MarcaProdutoModel gpEditar, int index}) async {
    final gpEd = await showDialog<MarcaProdutoModel>(
      context: context,
      barrierDismissible: false,
      builder: (BuildContext context) {
        return DialogMarcaProduto(gp: gpEditar);
      },
    );

    if (gpEd != null) {
      setState(() {
        if (index == null) {
          _MarcaProduto.add(gpEd);
          Services.AddMarcaProduto(gpEd.nome, gpEd.ativo);
          list();
          setState(() {
            
          });
        } else {
          _MarcaProduto[index] = gpEd;
          Services.UpdateMarcaProduto(gpEd.id, gpEd.nome, gpEd.ativo);
          list();
          setState(() {});
        }
      });
    }else {
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
          title: new Text('Marcas de Produto', style: TextStyle(color: Colors.deepPurple),),
          leading: IconButton(
              icon: new Icon(Icons.arrow_back, color: Colors.deepPurpleAccent, size: 35,),
              onPressed: () => Navigator.of(context).pop(null),
            ),

        ),
        body: Container(
          color: Colors.deepPurple,
          child: list(),
        ),
        floatingActionButton: FloatingActionButton(
          onPressed: _editarGrupo,
          child: Icon(Icons.add),
          backgroundColor: Colors.green,
        ),
      ),
    );
  }
}
