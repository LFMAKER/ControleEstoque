
import 'package:flutter/material.dart';
import 'package:inventory_analytics/model/GerenciarModel.dart';
import 'package:inventory_analytics/pages/Fornecedor/Fornecedor.dart';
import 'package:inventory_analytics/pages/GrupoProduto/GrupoProduto.dart';
import 'package:inventory_analytics/pages/LocalArmazenamento/LocalArmazenamento.dart';
import 'package:inventory_analytics/pages/MarcaProduto/MarcaProduto.dart';
import 'package:inventory_analytics/pages/Produto/Produto.dart';
import 'package:inventory_analytics/pages/UnidadeMedida/UnidadeMedida.dart';
import 'Home.dart';


class Gerenciar extends StatefulWidget {
  @override
  _GerenciarState createState() => new _GerenciarState();
}

class _GerenciarState extends State<Gerenciar> with TickerProviderStateMixin {


  List<GerenciarModel> gerenciarLista = [
    GerenciarModel("Produtos", Icons.shopping_cart, Produto()),
    GerenciarModel("Fornecedores", Icons.people, Fornecedor()),
    GerenciarModel("Armazenamento", Icons.inbox, LocalArmazenamento()),
    GerenciarModel("Unidades Medida", Icons.blur_on, UnidadeMedida()),
    GerenciarModel("Grupos", Icons.group_work, GrupoProduto()),
    GerenciarModel("Marcas", Icons.branding_watermark, MarcaProduto()),
  ];


  @override
  Widget build(BuildContext context) {


    Widget _montarCard(String titulo, IconData icon, Widget route){
      return Card(
        child: FlatButton(
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: <Widget>[
              Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: <Widget>[
                  Text('$titulo', style: TextStyle(fontSize: 15.0), softWrap: true,),
                ],
              ),
              Padding(
                padding: EdgeInsets.all(1.0),
              ),
              Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: <Widget>[
                  Icon(icon, size: 40),
                ],
              ),
            ],
          ),
          onPressed: () {
            Route nv = MaterialPageRoute(builder: (context) => route);
            Navigator.push(context, nv);
          },
        ),
      );
    }


    return GridView.count(
        crossAxisCount: 2,
        crossAxisSpacing: 1,
        childAspectRatio: 1.5,
        padding: EdgeInsets.all(5.0),
        children: List.generate(gerenciarLista.length, (index) {
          GerenciarModel geren = gerenciarLista[index];
          return _montarCard(geren.Nome, geren.Icon, geren.route);
        })
    );;
  }

}
