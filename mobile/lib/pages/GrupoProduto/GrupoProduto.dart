
import 'package:flutter/material.dart';
import 'package:inventory_analytics/api/api_service.dart';
import 'package:inventory_analytics/model/GrupoProdutoModel.dart';
import 'package:inventory_analytics/pages/GrupoProduto/dialog.dart';

class GrupoProduto extends StatefulWidget {
  @override
  _GrupoProdutoState createState() => new _GrupoProdutoState();
}

class _GrupoProdutoState extends State<GrupoProduto>
    with TickerProviderStateMixin {
  List<GrupoProdutoModel> _grupoproduto = new List();

  @override
  void initState() {
    super.initState();
    carregar();
    setState(() {
      
    });
  }



/* Remove the data from the List DataSource */
  void _removeGrupos(int index, GrupoProdutoModel gp) async {
    setState(() {
      _grupoproduto.removeAt(index);
      Services.DeleteGrupoProduto(gp.id);
    });
  }

  void carregar() async{
    _grupoproduto = await Services.getGruposProduto();
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
    if (_grupoproduto.length > 0) {
      return ListView.builder(
        itemCount: _grupoproduto.length,
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
        var result = _grupoproduto.elementAt(index);

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
          title: Center(child: Text(_grupoproduto[index].nome)),
          children: <Widget>[
            ListTile(
              title: Text(
                "Id: " + _grupoproduto[index].id.toString(),
                textAlign: TextAlign.left,
              ),
            ),
            ListTile(
              title: Text(
                "Status: " +
                    _grupoproduto[index]
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
  
 void _editarGrupo({GrupoProdutoModel gpEditar, int index}) async {
    final gpEd = await showDialog<GrupoProdutoModel>(
      context: context,
      barrierDismissible: false,
      builder: (BuildContext context) {
        return DialogGrupoProduto(gp: gpEditar);
      },
    );

    if (gpEd != null) {
      setState(() {
        if (index == null) {
          _grupoproduto.add(gpEd);
          Services.AddGrupoProduto(gpEd.nome, gpEd.ativo);
          list();
          setState(() {
            
          });
        } else {
          _grupoproduto[index] = gpEd;
          Services.UpdateGrupoProduto(gpEd.id, gpEd.nome, gpEd.ativo);
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
          title: new Text('Grupos de Produto', style: TextStyle(color: Colors.deepPurple),),
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
