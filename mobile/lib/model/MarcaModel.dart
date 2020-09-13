import 'package:flutter/material.dart';

class MarcaProdutoModel {
  int id;
  String nome;
  bool ativo;

  MarcaProdutoModel({this.id, this.nome, this.ativo});
  //MarcaProdutoModel({this.id, this.nome, this.ativo});

  MarcaProdutoModel.fromJson(Map<String, dynamic> json) {
    id = json['Id'];
    nome = json['Nome'];
    ativo = json['Ativo'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['Id'] = this.id;
    data['Nome'] = this.nome;
    data['Ativo'] = this.ativo;
    return data;
  }


}