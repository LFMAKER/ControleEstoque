
import 'package:flutter/material.dart';
import 'package:inventory_analytics/api/api_service.dart';
import 'package:inventory_analytics/model/LocalArmazenamentoModel.dart';
import 'package:inventory_analytics/pages/LocalArmazenamento/dialog.dart';

class LocalArmazenamento extends StatefulWidget {
  @override
  _LocalArmazenamentoState createState() => new _LocalArmazenamentoState();
}

class _LocalArmazenamentoState extends State<LocalArmazenamento>
    with TickerProviderStateMixin {
  List<LocalArmazenamentoModel> _LocalArmazenamento = new List();

  @override
  void initState() {
    super.initState();
    carregar();
    setState(() {
      
    });
  }



/* Remove the data from the List DataSource */
  void _removeGrupos(int index, LocalArmazenamentoModel gp) async {
    setState(() {
      _LocalArmazenamento.removeAt(index);
      Services.DeleteLocalArmazenamento(gp.id);
    });
  }

  void carregar() async{
    _LocalArmazenamento = await Services.getLocaisArmazenamento();
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
    if (_LocalArmazenamento.length > 0) {
      return ListView.builder(
        itemCount: _LocalArmazenamento.length,
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
        var result = _LocalArmazenamento.elementAt(index);

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
          title: Center(child: Text(_LocalArmazenamento[index].nome)),
          children: <Widget>[
            ListTile(
              title: Text(
                "Id: " + _LocalArmazenamento[index].id.toString(),
                textAlign: TextAlign.left,
              ),
            ),
             ListTile(
              title: Text(
                "Capacidade Atual: " + _LocalArmazenamento[index].capacidadeAtual.toString(),
                textAlign: TextAlign.left,
              ),
            ),
            ListTile(
              title: Text(
                "Capacidade Total: " + _LocalArmazenamento[index].capacidadeTotal.toString(),
                textAlign: TextAlign.left,
              ),
            ),
            ListTile(
              title: Text(
                "Status: " +
                    _LocalArmazenamento[index]
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
  
 void _editarGrupo({LocalArmazenamentoModel gpEditar, int index}) async {
    final gpEd = await showDialog<LocalArmazenamentoModel>(
      context: context,
      barrierDismissible: false,
      builder: (BuildContext context) {
        return DialogLocalArmazenamento(gp: gpEditar);
      },
    );

    if (gpEd != null) {
      setState(() {
        if (index == null) {
          _LocalArmazenamento.add(gpEd);
          Services.AddLocalArmazenamento(gpEd.nome, gpEd.capacidadeTotal, gpEd.ativo);
          list();
          setState(() {
            
          });
        } else {
          _LocalArmazenamento[index] = gpEd;
          Services.UpdateLocalArmazenamento(gpEd.id, gpEd.nome, gpEd.capacidadeAtual, gpEd.capacidadeTotal, gpEd.ativo);
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
          title: new Text('Local de Armazenamento', style: TextStyle(color: Colors.deepPurple),),
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
