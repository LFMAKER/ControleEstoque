import 'package:inventory_analytics/api/api_service.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_offline/flutter_offline.dart';

class LoginPage extends StatefulWidget {
  @override
  _LoginPageState createState() => _LoginPageState();
}

class _LoginPageState extends State<LoginPage> {
  bool _isLoading = false;
  String userResponse = "";
  bool conn;

  _signInOne(String email, pass, BuildContext context) async {
    var result = await Services.signIn(email, pass);

    if (result == "OK") {
      setState(() {
        _isLoading = false;
      });


      Navigator.pushNamed(context, '/', arguments: userResponse);
    } else {
      setState(() {
        _isLoading = false;
      });
      _showAlertDialog(context);
    }
  }

  Widget _body(BuildContext context) {
    SystemChrome.setSystemUIOverlayStyle(SystemUiOverlayStyle.light
        .copyWith(statusBarColor: Colors.transparent));
    return Scaffold(body: okay());
  }

  Widget okay() {
    return OfflineBuilder(
      connectivityBuilder: (
        BuildContext context,
        ConnectivityResult connectivity,
        Widget child,
      ) {
        final bool connected = connectivity != ConnectivityResult.none;

        return new Stack(
          fit: StackFit.expand,
          children: [
            Container(
              decoration: BoxDecoration(
                gradient: LinearGradient(
                    colors: [Colors.deepPurple, Colors.purple[300]],
                    begin: Alignment.topCenter,
                    end: Alignment.bottomCenter),
              ),
              child: _isLoading
                  ? Center(
                      child: CircularProgressIndicator(),
                    )
                  : ListView(
                      children: <Widget>[
                        headerSection(),
                        textSection(),
                        buttonSection(),
                      ],
                    ),
            ),
            Positioned(
              height: 24.0,
              left: 0.0,
              right: 0.0,
              child: Container(
                color: connected ? Color(0xFF00EE44) : Color(0xFFEE4400),
                child: Center(
                  child: Text("${connected ? 'ONLINE' : 'OFFLINE'}"),
                ),
              ),
            ),
          ],
        );
      },
      child: Text('S'),
    );
  }

  @override
  Widget build(BuildContext context) {
    return _body(context);
  }

  Container buttonSection() {
    return Container(
      width: MediaQuery.of(context).size.width,
      height: 40.0,
      padding: EdgeInsets.symmetric(horizontal: 15.0),
      margin: EdgeInsets.only(top: 15.0),
      child: RaisedButton(
        onPressed:  () {
          //emailController.text == "" || passwordController.text == ""
            //? null
            //:
                setState(() {
                  _isLoading = true;
                });
                _signInOne(
                    emailController.text, passwordController.text, context);
              },
        elevation: 0.0,
        color: Colors.deepPurpleAccent,
        child: Text("Entrar", style: TextStyle(color: Colors.white)),
        shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(5.0)),
      ),
    );
  }

  final TextEditingController emailController = new TextEditingController();
  final TextEditingController passwordController = new TextEditingController();

  Container textSection() {
    return Container(
      padding: EdgeInsets.symmetric(horizontal: 15.0, vertical: 20.0),
      child: Column(
        children: <Widget>[
          TextFormField(
            controller: emailController,
            cursorColor: Colors.white,
            style: TextStyle(color: Colors.white),
            decoration: InputDecoration(
              icon: Icon(Icons.email, color: Colors.white),
              hintText: "Login",
              border: UnderlineInputBorder(
                  borderSide: BorderSide(color: Colors.white)),
              hintStyle: TextStyle(color: Colors.white),
            ),
          ),
          SizedBox(height: 30.0),
          TextFormField(
            controller: passwordController,
            cursorColor: Colors.white,
            obscureText: true,
            style: TextStyle(color: Colors.white),
            decoration: InputDecoration(
              icon: Icon(Icons.lock, color: Colors.white),
              hintText: "Senha",
              border: UnderlineInputBorder(
                  borderSide: BorderSide(color: Colors.white)),
              hintStyle: TextStyle(color: Colors.white),
            ),
          ),
        ],
      ),
    );
  }

  Container headerSection() {
    return Container(
      margin: EdgeInsets.only(top: 50.0),
      padding: EdgeInsets.symmetric(horizontal: 20.0, vertical: 30.0),
      child: Text("Inventory Analytics",
          style: TextStyle(
              color: Colors.white,
              fontSize: 40.0,
              fontWeight: FontWeight.bold)),
    );
  }
}

void _showAlertDialog(BuildContext context) {
  // set up the button
  Widget okButton = FlatButton(
    child: Text("Ok"),
    onPressed: () {
      Navigator.pop(context);
    },
  );
  // set up the AlertDialog

  // show the dialog
  showDialog(
      context: context,
      builder: (BuildContext context) => AlertDialog(
            title: Text("Falha no login!"),
            content: Text("Login ou senha inválidos, tente novamente..."),
            actions: [
              okButton,
            ],
          ));
}
