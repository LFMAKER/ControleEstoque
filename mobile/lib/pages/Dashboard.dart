
import 'dart:convert';

import 'package:intl/intl.dart';
import 'package:inventory_analytics/api/api_service.dart';
import 'package:inventory_analytics/model/Usuario.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'Login.dart';
import 'package:fl_chart/fl_chart.dart';
import 'package:flutter/material.dart';
import 'ChartLocalArmazenamento.dart';
import 'package:inventory_analytics/model/CardItemModel.dart';

class Dashboard extends StatefulWidget {

  @override
  _DashboardState createState() => new _DashboardState();
}

class _DashboardState extends State<Dashboard> with TickerProviderStateMixin {
  

  var appColors = [Color.fromRGBO(231, 129, 109, 1.0),Color.fromRGBO(99, 138, 223, 1.0),Color.fromRGBO(111, 194, 173, 1.0)];
  var cardIndex = 0;
  ScrollController scrollController;
  var currentColor = Color.fromRGBO(231, 129, 109, 1.0);

  static String  formatDate(DateTime date) => new DateFormat("dd/MM/yyyy").format(date);
  var dataAtual = formatDate(DateTime.now());

  var cardsList = [CardItemModel("Movimentação Mensal", Icons.attach_money, _ganhosMensal()), CardItemModel("Locais Armazenamento", Icons.move_to_inbox, _localArmazenamento())];

  AnimationController animationController;
  ColorTween colorTween;
  CurvedAnimation curvedAnimation;

  String userName = "";
  @override
  void initState() {
    super.initState();
    scrollController = new ScrollController();

  }
  
  void getUser() async{
    SharedPreferences pref = await SharedPreferences.getInstance();
    setState(() {
      userName = pref.getString("user_nome");
    });
  }



  @override
  Widget build(BuildContext context) {
    getUser();

    return Center(
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: <Widget>[
          Row(),
          Padding(
            padding: const EdgeInsets.symmetric(horizontal: 64.0, vertical: 12.0),
            child: Container(
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: <Widget>[

                 /* boasVindas(),*/
                  Text("Sentimos a sua falta $userName..", style: TextStyle(color: Colors.white),),
                  Text("Verifique seus principais ativos abaixo.", style: TextStyle(color: Colors.white,),),
                ],
              ),
            ),
          ),
          Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: <Widget>[
              Padding(
                padding: const EdgeInsets.symmetric(horizontal: 64.0, vertical: 16.0),
                child: Text("Hoje é $dataAtual", style: TextStyle(color: Colors.white),),
              ),
              Container(
                height: 380.0,
                child: ListView.builder(
                  physics: NeverScrollableScrollPhysics(),
                  itemCount: cardsList.length,
                  controller: scrollController,
                  scrollDirection: Axis.horizontal,
                  itemBuilder: (context, position) {
                    return GestureDetector(
                      child: Padding(
                        padding: const EdgeInsets.all(8.0),
                        child: Card(
                          child: Container(
                            width: 350.0,
                            child: Column(
                              crossAxisAlignment: CrossAxisAlignment.start,
                              mainAxisAlignment: MainAxisAlignment.spaceBetween,
                              children: <Widget>[
                                Padding(
                                  padding: const EdgeInsets.all(8.0),
                                  child: Row(
                                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                                    children: <Widget>[
                                      Icon(cardsList[position].icon, color: appColors[position],),
                                    ],
                                  ),
                                ),
                                Padding(
                                  padding: const EdgeInsets.all(8.0),
                                  child: Column(
                                    crossAxisAlignment: CrossAxisAlignment.start,
                                    children: <Widget>[
                                      Padding(
                                        padding: const EdgeInsets.symmetric(horizontal: 8.0, vertical: 4.0),
                                        child: cardsList[position].Content,
                                      ),
                                      Padding(
                                        padding: const EdgeInsets.symmetric(horizontal: 8.0, vertical: 4.0),
                                        child: Text("${cardsList[position].cardTitle}", style: TextStyle(fontSize: 28.0),),
                                      ),

                                    ],
                                  ),
                                ),
                              ],
                            ),
                          ),
                          shape: RoundedRectangleBorder(
                              borderRadius: BorderRadius.circular(10.0)
                          ),
                        ),
                      ),
                      onHorizontalDragEnd: (details) {

                        animationController = AnimationController(vsync: this, duration: Duration(milliseconds: 500));
                        curvedAnimation = CurvedAnimation(parent: animationController, curve: Curves.fastOutSlowIn);
                        animationController.addListener(() {
                          setState(() {
                            currentColor = colorTween.evaluate(curvedAnimation);
                          });
                        });

                        if(details.velocity.pixelsPerSecond.dx > 0) {
                          if(cardIndex>0) {
                            cardIndex--;
                            colorTween = ColorTween(begin:currentColor,end:appColors[cardIndex]);
                          }
                        }else {
                          if(cardIndex<2) {
                            cardIndex++;
                            colorTween = ColorTween(begin: currentColor,
                                end: appColors[cardIndex]);
                          }
                        }
                        setState(() {
                          scrollController.animateTo((cardIndex)*256.0, duration: Duration(milliseconds: 500), curve: Curves.fastOutSlowIn);
                        });

                        colorTween.animate(curvedAnimation);

                        animationController.forward( );

                      },
                    );
                  },
                ),
              ),
            ],
          )
        ],
      ),
    );
  }

}




Widget _ganhosMensal(){
  List<Color> gradientColors = [
    Colors.deepPurpleAccent,
    Colors.deepPurple,
  ];

  return AspectRatio(
    aspectRatio: 1.70,
    child: Container(
      decoration: BoxDecoration(
          borderRadius: BorderRadius.all(Radius.circular(18)),
          color: Colors.white
      ),
      child: Padding(
        padding: const EdgeInsets.only(right: 18.0, left: 12.0, top: 24, bottom: 12),
        child: FlChart(
          chart: LineChart(
            LineChartData(
              gridData: FlGridData(
                show: true,
                drawHorizontalGrid: true,
                getDrawingVerticalGridLine: (value) {
                  return const FlLine(
                    color: Color(0xff37434d),
                    strokeWidth:  1,
                  );
                },
                getDrawingHorizontalGridLine: (value) {
                  return const FlLine(
                    color: Color(0xff37434d),
                    strokeWidth: 1,
                  );
                },
              ),
              titlesData: FlTitlesData(
                show: true,
                bottomTitles: SideTitles(
                  showTitles: true,
                  reservedSize: 22,
                  textStyle: TextStyle(
                      color: const Color(0xff68737d),
                      fontWeight: FontWeight.bold,
                      fontSize: 16
                  ),
                  getTitles: (value) {
                    switch(value.toInt()) {
                      case 2: return 'MAR';
                      case 5: return 'JUN';
                      case 8: return 'SET';
                    }

                    return '';
                  },
                  margin: 8,
                ),
                leftTitles: SideTitles(
                  showTitles: true,
                  textStyle: TextStyle(
                    color: const Color(0xff67727d),
                    fontWeight: FontWeight.bold,
                    fontSize: 15,
                  ),
                  getTitles: (value) {
                    switch(value.toInt()) {
                      case 1: return '10';
                      case 3: return '30';
                      case 5: return '50';
                    }
                    return '';
                  },
                  reservedSize: 28,
                  margin: 12,
                ),
              ),
              borderData: FlBorderData(
                  show: true,
                  border: Border.all(color: Color(0xff37434d), width: 1)
              ),
              minX: 0,
              maxX: 11,
              minY: 0,
              maxY: 6,
              lineBarsData: [
                LineChartBarData(
                  spots: [
                    FlSpot(0, 3),
                    FlSpot(2.6, 2),
                    FlSpot(4.9, 5),
                    FlSpot(6.8, 3.1),
                    FlSpot(8, 4),
                    FlSpot(9.5, 3),
                    FlSpot(11, 4),
                  ],
                  isCurved: true,
                  colors: gradientColors,
                  barWidth: 5,
                  isStrokeCapRound: true,
                  dotData: FlDotData(
                    show: false,
                  ),
                  belowBarData: BelowBarData(
                    show: true,
                    colors: gradientColors.map((color) => color.withOpacity(0.3)).toList(),
                  ),
                ),
              ],
            ),
          ),
        ),
      ),
    ),
  );
}
Widget _localArmazenamento(){
  return PieChartSample2();
}

/*
Widget boasVindas() {
  return FutureBuilder(
    builder: (context, projectSnap) {
      String data = projectSnap.data;

      if (projectSnap.connectionState == ConnectionState.none &&
          projectSnap.hasData == null) {
        //print('project snapshot data is: ${projectSnap.data}');
        return Container();
      }
      return Padding(
        padding: const EdgeInsets.fromLTRB(0.0,16.0,0.0,12.0),
        child: Text("Olá $data", style: TextStyle(fontSize: 30.0, color: Colors.white, fontWeight: FontWeight.w400),),
      );
    },
    future: Services.getUserNome(),
  );
}*/
