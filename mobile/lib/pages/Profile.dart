import 'package:flutter/material.dart';
import 'package:inventory_analytics/api/api_service.dart';
import 'package:inventory_analytics/model/Usuario.dart';
import 'package:shared_preferences/shared_preferences.dart';

class Profile extends StatefulWidget {
  @override
  _ProfileState createState() => _ProfileState();
}

class _ProfileState extends State<Profile> {
  Usuario user = new Usuario();
  Perfil perfil = new Perfil();
  KeyC key = new KeyC();

  @override
  void initState() {
  
    super.initState();
    getUser();
  }

  void getUser() async {
    user = await Services.getUserSession();
    setState(() {
      perfil = user.perfil;
      key = user.key;
      user = user;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Container(
      padding: EdgeInsets.all(30),
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: <Widget>[
          Card(
            child: Container(
              padding: EdgeInsets.all(20),
              child: Column(
                children: <Widget>[
                  new Text(
                    'Olá, ${user.nome}',
                    style: new TextStyle(
                      color: Colors.black,
                      fontSize: 18,
                    ),
                    textAlign: TextAlign.left,
                  ),
                  new Padding(
                    padding: EdgeInsets.all(15),
                  ),

                  Row(
                    children: <Widget>[
                      new Text(
                        'E-mail:',
                        style: new TextStyle(color: Colors.black, fontSize: 18,fontWeight: FontWeight.bold),
                      ),
                      new Padding(
                        padding: EdgeInsets.only(left:140),
                      ),
                      new Text(
                        'Login:',
                        style: new TextStyle(color: Colors.black, fontSize: 18,fontWeight: FontWeight.bold),
                      ),
                    ],
                  ),
                  Row(
                    children: <Widget>[
                      new Text(
                        '${user.email}',
                        style: new TextStyle(color: Colors.black, fontSize: 18),
                      ),
                      new Padding(
                        padding: EdgeInsets.all(20),
                      ),
                      new Text(
                        '${user.login}',
                        style: new TextStyle(color: Colors.black, fontSize: 18),
                      ),
                    ],
                  ),
                  
                  new Padding(
                    padding: EdgeInsets.all(15),
                  ),

                  Row(
                    children: <Widget>[
                      new Text(
                        'Nome:',
                        style: new TextStyle(color: Colors.black, fontSize: 18,fontWeight: FontWeight.bold),
                      ),
                      new Padding(
                        padding: EdgeInsets.only(left:140),
                      ),
                      new Text(
                        'Perfil:',
                        style: new TextStyle(color: Colors.black, fontSize: 18,fontWeight: FontWeight.bold),
                      ),
                    ],
                  ),
                  Row(
                    children: <Widget>[
                      new Text(
                        '${user.nome}',
                        style: new TextStyle(color: Colors.black, fontSize: 18),
                      ),
                       new Padding(
                        padding: EdgeInsets.all(15),
                      ),
                      new Padding(padding: EdgeInsets.only(left: 102),),
                      new Text(
                        '${perfil.nome}',
                        style: new TextStyle(color: Colors.black, fontSize: 18),
                      ),
                    ],
                  ),
                    
                  new Padding(
                    padding: EdgeInsets.all(10),
                  ),

                  Row(
                    children: <Widget>[
                      new Text(
                        'API Key:',
                        style: new TextStyle(color: Colors.black, fontSize: 18,fontWeight: FontWeight.bold),
                      ),
                     
                    ],
                  ),
                  Row(
                    children: <Widget>[
                      new Text(
                        '${key.codigo}',
                        style: new TextStyle(color: Colors.black, fontSize: 12),
                      ),
                      
                    ],
                  ),

                ],
              ),
            ),
          ),
        ],
      ),
    );
  }
}
