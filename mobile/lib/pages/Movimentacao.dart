import 'package:flutter/material.dart';
import 'package:inventory_analytics/model/GerenciarModel.dart';

import 'Home.dart';

class Movimentacao extends StatefulWidget {
  @override
  _MovimentacaoState createState() => new _MovimentacaoState();
}

class _MovimentacaoState extends State<Movimentacao>
    with TickerProviderStateMixin {
  List<String> companies;

  @override
  void initState() {
    super.initState();
    companies = List();
    addCompanies();
  }

/* Initialize the list with Some company names */
  addCompanies() {
    companies.add("[Saída] Iphone 8 Plus 64GB - R#1000 - QT1");
    companies.add("[Entrada] Geladeira Samsung - R#4000 - QT100");
    companies.add("[Saída] Televisão 4K - R#8400 - QT20");
    companies.add("[Saída] Iphone 8 Plus 64GB - R#1000 - QT1");
    companies.add("[Entrada] Geladeira Samsung - R#4000 - QT100");
    companies.add("[Saída] Televisão 4K - R#8400 - QT20");

  }

/* Remove the data from the List DataSource */
  removeCompany(index) {
    setState(() {
      companies.removeAt(index);
    });
  }

/* Undo the Deleted row when user clicks on UNDO in the SnackBar message */
  undoDelete(index, company) {
    setState(() {
      companies.insert(index, company);
    });
  }

/* Show Snackbar when Deleted with an action to Undo the delete */
  showSnackBar(context, company, index) {
    Scaffold.of(context).showSnackBar(SnackBar(
      content: Text('$company removido'),
      action: SnackBarAction(
        label: "Desfazer",
        onPressed: () {
          undoDelete(index, company);
        },
      ),
    ));
  }

/* Give a background to the Swipe Delete as a indicator to Delete */
  Widget refreshBg() {
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

  Widget list() {
    return Column(
      children: <Widget>[
        Padding(
          padding: EdgeInsets.all(20.0),
        ),
        Expanded(
          child: Row(
            children: <Widget>[
              Expanded(
                child:  ListView.builder(
                  padding: EdgeInsets.all(20.0),
                  itemCount: companies.length,
                  itemBuilder: (BuildContext context, int index) {
                    return row(context, index);
                  },
                ),
              ),
            ],
          ),
        ),
      ],
    );
  }

  Widget row(context, index) {
    return Dismissible(
      key: Key(companies[index]), // UniqueKey().toString()
      onDismissed: (direction) {
        var company = companies[index];
        showSnackBar(context, company, index);
        removeCompany(index);
      },
      background: refreshBg(),
      child: Card(
        child: ListTile(
          title: Text(companies[index]),
        ),
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    return Container(
        color: Colors.deepPurpleAccent,
          child: list(),
        );
  }
}
