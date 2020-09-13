
import 'package:flutter/material.dart';
import 'package:inventory_analytics/api/api_service.dart';
import 'package:inventory_analytics/model/ProdutoModel.dart';
import 'package:inventory_analytics/pages/Produto/dialog.dart';

class Produto extends StatefulWidget {
  @override
  _ProdutoState createState() => new _ProdutoState();
}

class _ProdutoState extends State<Produto>
    with TickerProviderStateMixin {
  List<ProdutoModel> _Produto = new List();

  @override
  void initState() {
    super.initState();
    carregar();
    setState(() {
      
    });
  }



/* Remove the data from the List DataSource */
  void _removeGrupos(int index, ProdutoModel gp) async {
    setState(() {
      _Produto.removeAt(index);
      Services.deleteProduto(gp.id);
    });
  }

  void carregar() async{
    _Produto = await Services.getProdutos();
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
    if (_Produto.length > 0) {
      return ListView.builder(
        itemCount: _Produto.length,
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
        var result = _Produto.elementAt(index);

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
          title: Center(child: Text(_Produto[index].nome)),
          children: <Widget>[
            ListTile(
              title: Text(
                "Id: " + _Produto[index].id.toString(),
                textAlign: TextAlign.left,
              ),
            ),
             ListTile(
              title: Text(
                "Código: " + _Produto[index].codigo.toString(),
                textAlign: TextAlign.left,
              ),
            ),
            ListTile(
              title: Text(
                "Preço Custo: " + (_Produto[index].precoCusto / 10).toString(),
                textAlign: TextAlign.left,
              ),
            ),
             ListTile(
              title: Text(
                "Preço Venda: " + (_Produto[index].precoVenda/ 10).toString(),
                textAlign: TextAlign.left,
              ),
            ),
             ListTile(
              title: Text(
                "Quantidade Estoque: " + _Produto[index].quantEstoque.toString(),
                textAlign: TextAlign.left,
              ),
            ),
            ListTile(
              title: Text(
                "Status: " +
                    _Produto[index]
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
  
 void _editarGrupo({ProdutoModel gpEditar, int index}) async {
    final gpEd = await showDialog<ProdutoModel>(
      context: context,
      barrierDismissible: false,
      builder: (BuildContext context) {
        return DialogProduto(gp: gpEditar);
      },
    );

            
   
    if (gpEd != null) {
      print("FORNECE RECEBIDO:"+gpEd.idFornecedor.toString());
      setState(() {
        if (index == null) {
          _Produto.add(gpEd);
          Services.addProduto(gpEd.codigo, gpEd.nome, gpEd.precoCusto, gpEd.precoVenda, gpEd.quantEstoque, gpEd.idUnidadeMedida,
          gpEd.idGrupo, gpEd.idMarca, gpEd.idFornecedor, gpEd.idLocalArmazenamento, gpEd.ativo);
          list();
          setState(() {
            
          });
        } else {
          _Produto[index] = gpEd;
          Services.updateProduto(gpEd.id, gpEd.codigo, gpEd.nome, gpEd.precoCusto, gpEd.precoVenda, gpEd.quantEstoque, gpEd.idUnidadeMedida,
          gpEd.idGrupo, gpEd.idMarca, gpEd.idFornecedor, gpEd.idLocalArmazenamento, gpEd.ativo);
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
          title: new Text('Produtos', style: TextStyle(color: Colors.deepPurple),),
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