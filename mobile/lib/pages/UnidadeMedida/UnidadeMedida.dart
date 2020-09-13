
import 'package:flutter/material.dart';
import 'package:inventory_analytics/api/api_service.dart';
import 'package:inventory_analytics/model/UnidadeMedidaModel.dart';
import 'package:inventory_analytics/pages/UnidadeMedida/dialog.dart';


class UnidadeMedida extends StatefulWidget {
  @override
  _UnidadeMedidaState createState() => new _UnidadeMedidaState();
}

class _UnidadeMedidaState extends State<UnidadeMedida>
    with TickerProviderStateMixin {
  List<UnidadeMedidaModel> _UnidadeMedida = new List();

  @override
  void initState() {
    super.initState();
    carregar();
    setState(() {
      
    });
  }



/* Remove the data from the List DataSource */
  void _removeGrupos(int index, UnidadeMedidaModel gp) async {
    setState(() {
      _UnidadeMedida.removeAt(index);
      Services.DeleteUnidadeMedida(gp.id);
    });
  }

  void carregar() async{
    _UnidadeMedida = await Services.getUnidadesMedida();
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
    if (_UnidadeMedida.length > 0) {
      return ListView.builder(
        itemCount: _UnidadeMedida.length,
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
        var result = _UnidadeMedida.elementAt(index);

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
          title: Center(child: Text(_UnidadeMedida[index].nome)),
          children: <Widget>[
            ListTile(
              title: Text(
                "Id: " + _UnidadeMedida[index].id.toString(),
                textAlign: TextAlign.left,
              ),
            ),
            ListTile(
              title: Text(
                "Status: " +
                    _UnidadeMedida[index]
                        .ativo
                        .toString()
                        .replaceAll("true", "Ativado")
                        .replaceAll("false", "Desativado"),
                textAlign: TextAlign.left,
              ),
            ),
             ListTile(
              title: Text(
                "Sigla: " + _UnidadeMedida[index].sigla.toString(),
                textAlign: TextAlign.left,
              ),
            ),
          ],
        ),
      ),
    );
  }
  
 void _editarGrupo({UnidadeMedidaModel gpEditar, int index}) async {
    final gpEd = await showDialog<UnidadeMedidaModel>(
      context: context,
      barrierDismissible: false,
      builder: (BuildContext context) {
        return DialogUnidadeMedida(gp: gpEditar);
      },
    );

    if (gpEd != null) {
      setState(() {
        if (index == null) {
          _UnidadeMedida.add(gpEd);
          Services.AddUnidadeMedida(gpEd.nome, gpEd.sigla, gpEd.ativo);
          list();
          setState(() {
            
          });
        } else {
          _UnidadeMedida[index] = gpEd;
          Services.UpdateUnidadeMedida(gpEd.id, gpEd.nome, gpEd.sigla, gpEd.ativo);
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
          title: new Text('Unidades de Medidas', style: TextStyle(color: Colors.deepPurple),),
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
