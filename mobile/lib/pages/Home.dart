import 'dart:developer';
import 'dart:io';

import 'package:curved_navigation_bar/curved_navigation_bar.dart';
import 'package:flutter/material.dart';
import 'package:inventory_analytics/api/api_service.dart';
import 'package:inventory_analytics/pages/Fornecedor/Fornecedor.dart';
import 'package:inventory_analytics/pages/GrupoProduto/GrupoProduto.dart';
import 'package:inventory_analytics/pages/LocalArmazenamento/LocalArmazenamento.dart';
import 'package:inventory_analytics/pages/Login.dart';
import 'package:inventory_analytics/pages/MarcaProduto/MarcaProduto.dart';
import 'package:inventory_analytics/pages/Produto/Produto.dart';
import 'package:inventory_analytics/pages/Profile.dart';
import 'Dashboard.dart';
import 'Gerenciar.dart';
import 'Movimentacao.dart';

void main() => runApp(MaterialApp(
      routes: {
        '/login': (context) => LoginPage(),
        '/': (context) => BottomNavBar(),
        '/dashboard': (context) => Dashboard(),
        '/gerenciar': (context) => Gerenciar(),
        '/movimentacao': (context) => Movimentacao(),
        '/grupos' : (context) => GrupoProduto(),
        '/marcas' : (context) => MarcaProduto(),
        '/local' : (context) => LocalArmazenamento(),
      },
      initialRoute: '/login',
    ));


class BottomNavBar extends StatefulWidget {
  @override
  _BottomNavBarState createState() => _BottomNavBarState();
}

class _BottomNavBarState extends State<BottomNavBar> {
  int _page = 0;

  Color corPadraoAplicativo = Colors.deepPurple;
  Color paginaAndBottomMenuColor = Colors.deepPurple.withOpacity(0.7);

  //Create all the pages
  final Dashboard _dashboard = Dashboard();
  final Gerenciar _gerenciar = Gerenciar();
  final Profile _profile = Profile();
  Widget _showPage = new Dashboard();

  Widget _pageChooser(int page){
    switch(page){
      case 0:
        return _dashboard;
        break;
      case 1:
        return _gerenciar;
        break;
      case 2:
       return _profile;
       break;
    }
  }




  GlobalKey _bottomNavigationKey = GlobalKey();

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        iconTheme:  new IconThemeData(color: corPadraoAplicativo),
        backgroundColor: Colors.white,
        title: Center(
            child: Text("Inventory Analytics",
              style: TextStyle(color: corPadraoAplicativo,
                  fontWeight: FontWeight.bold)
              ,)
        ),
      ),
      drawer: Drawer(
        child: ListView(
          children: <Widget>[
            DrawerHeader(child: Column(mainAxisAlignment: MainAxisAlignment.center,children: <Widget>[Text("Inventory Analytics", style: TextStyle(fontSize: 20, color: corPadraoAplicativo), textAlign: TextAlign.center,)],)),
            ListTile(
              title: Text("Perfil", style: TextStyle(fontSize: 20),),
              onTap: (){
             Route nv = MaterialPageRoute(builder: (context) => Profile());
                  Navigator.push(context, nv);;
              },
            ),
            ListTile(
              title: Text("Produtos", style: TextStyle(fontSize: 20),),
              onTap: (){
             Route nv = MaterialPageRoute(builder: (context) => Produto());
                  Navigator.push(context, nv);;
              },
            ),
            
            ListTile(
              title: Text("Grupo de Produtos", style: TextStyle(fontSize: 20),),
              onTap: (){
            
                
                  Route nv = MaterialPageRoute(builder: (context) => GrupoProduto());
                  Navigator.push(context, nv);;
              },
            ),
            
            ListTile(
              title: Text("Fornecedores", style: TextStyle(fontSize: 20),),
              onTap: (){
    
               Route nv = MaterialPageRoute(builder: (context) => Fornecedor());
                  Navigator.push(context, nv);;
              },
            ),
            
            ListTile(
              title: Text("Marcas de Produtos", style: TextStyle(fontSize: 20),),
              onTap: (){
        
                  Route nv = MaterialPageRoute(builder: (context) => MarcaProduto());
                  Navigator.push(context, nv);
              },
            ),
            
            ListTile(
              title: Text("Local de Armazenamento", style: TextStyle(fontSize: 20),),
              onTap: (){
             
               
                  Route nv = MaterialPageRoute(builder: (context) => LocalArmazenamento());
                  Navigator.push(context, nv);
              },
            ),
            
            ListTile(
              title: Text("Logout", style: TextStyle(fontSize: 20),),
              onTap: (){
                Services.logout(context);
                
                Navigator.pushNamed(context, "/login");
              },
            ),
          ],
        ),
      ),
      bottomNavigationBar: CurvedNavigationBar(
        key: _bottomNavigationKey,
        index: 0,
        height: 50.0,
        items: <Widget>[
          Icon(Icons.dashboard, size: 30),
          Icon(Icons.add, size: 30),
          Icon(Icons.perm_identity, size: 30),
        ],
        color: Colors.white,
        buttonBackgroundColor: Colors.white,
        backgroundColor: paginaAndBottomMenuColor,
        animationCurve: Curves.easeInOut,
        animationDuration: Duration(milliseconds: 600),
        onTap: (index) {
          setState(() {
            _showPage = _pageChooser(index);
          });
        },
      ),
      body: Container(
        color: paginaAndBottomMenuColor,
        child: Center(
          child: _showPage,
        ),
      ),
      //floatingActionButton: FloatingActionButton(
        //onPressed: () {
          //Redirecionar para add produtos
        //},
        //child: Icon(Icons.add),
        //backgroundColor: Colors.white,
        //foregroundColor: Colors.black,
      //),
    );
  }
}
