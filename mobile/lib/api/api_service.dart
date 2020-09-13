import 'dart:io';

import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'package:inventory_analytics/model/FornecedorModel.dart';
import 'package:inventory_analytics/model/GrupoProdutoModel.dart';
import 'package:inventory_analytics/model/LocalArmazenamentoModel.dart';
import 'package:inventory_analytics/model/MarcaModel.dart';
import 'package:inventory_analytics/model/ProdutoModel.dart';
import 'package:inventory_analytics/model/UnidadeMedidaModel.dart';
import 'dart:convert';

import 'package:inventory_analytics/model/Usuario.dart';
import 'package:shared_preferences/shared_preferences.dart';

class Services {
  static Future<String> signIn(String email, pass) async {
    var url = "http://inventoryanalytics.me/api/Autenticar";
    var body = json.encode({
      'Login': email,
      'Senha': pass,
    });

    Map<String, String> headers = {
      'Content-type': 'application/json',
      'Accept': 'application/json',
    };

    final response = await http.post(url, body: body, headers: headers);
    final Map parsed = json.decode(response.body);
    if (parsed.containsKey("erro")) {
      setIsLogin();
      return "F";
    }

    Usuario user = Usuario.fromJson(parsed);

    //Criando session
    setUserLogin(user);

    return "OK";
  }

//GRUPO PRODUTO - LIST
  static Future<List<GrupoProdutoModel>> getGruposProduto() async {
    var url = "http://inventoryanalytics.me/api/GrupoProdutoList";
    final response = await http.get(url);
    var jsonData = json.decode(response.body);
    List<GrupoProdutoModel> gruposProduto = [];
    for (var u in jsonData) {
      GrupoProdutoModel gp = GrupoProdutoModel.fromJson(u);
      gruposProduto.add(gp);
    }

    return gruposProduto;
  }

//GRUPO PRODUTO - DELETE
  static Future<bool> DeleteGrupoProduto(int id) async {
    var url = "http://inventoryanalytics.me/api/GrupoProdutoDelete";
    var body = json.encode({
      'Id': id,
    });

    Map<String, String> headers = {
      'Content-type': 'application/json',
      'Accept': 'application/json',
    };

    final response = await http.post(url, body: body, headers: headers);
    var jsonData = json.decode(response.body);
    if (jsonData == true) {
      return true;
    }

    return false;
  }

//GRUPO PRODUTO - ADD
  static Future<bool> AddGrupoProduto(String nome, bool ativo) async {
    var url = "http://inventoryanalytics.me/api/GrupoProdutoCreate";
    var body = json.encode({'Id': 0, 'Nome': nome, 'Ativo': ativo});

    Map<String, String> headers = {
      'Content-type': 'application/json',
      'Accept': 'application/json',
    };

    final response = await http.post(url, body: body, headers: headers);
    var jsonData = json.decode(response.body);
    if (jsonData == true) {
      return true;
    }

    return false;
  }

//GRUPO PRODUTO - Update
  static Future<bool> UpdateGrupoProduto(
      int id, String nome, bool ativo) async {
    var url = "http://inventoryanalytics.me/api/GrupoProdutoUpdate";
    var body = json.encode({'Id': id, 'Nome': nome, 'Ativo': ativo});

    Map<String, String> headers = {
      'Content-type': 'application/json',
      'Accept': 'application/json',
    };

    final response = await http.post(url, body: body, headers: headers);
    var jsonData = json.decode(response.body);
    if (jsonData == true) {
      return true;
    }

    return false;
  }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

//MARCA PRODUTO - LIST
  static Future<List<MarcaProdutoModel>> getMarcasProduto() async {
    var url = "http://inventoryanalytics.me/api/MarcaProdutoList";
    final response = await http.get(url);
    var jsonData = json.decode(response.body);
    List<MarcaProdutoModel> marcasProduto = [];
    for (var u in jsonData) {
      MarcaProdutoModel mc = MarcaProdutoModel.fromJson(u);
      marcasProduto.add(mc);
    }

    return marcasProduto;
  }

//MARCA PRODUTO - DELETE
  static Future<bool> DeleteMarcaProduto(int id) async {
    var url = "http://inventoryanalytics.me/api/MarcaProdutoDelete";
    var body = json.encode({
      'Id': id,
    });

    Map<String, String> headers = {
      'Content-type': 'application/json',
      'Accept': 'application/json',
    };

    final response = await http.post(url, body: body, headers: headers);
    var jsonData = json.decode(response.body);
    if (jsonData == true) {
      return true;
    }

    return false;
  }

//MARCA PRODUTO - ADD
  static Future<bool> AddMarcaProduto(String nome, bool ativo) async {
    var url = "http://inventoryanalytics.me/api/MarcaProdutoCreate";
    var body = json.encode({'Id': 0, 'Nome': nome, 'Ativo': ativo});

    Map<String, String> headers = {
      'Content-type': 'application/json',
      'Accept': 'application/json',
    };

    final response = await http.post(url, body: body, headers: headers);
    var jsonData = json.decode(response.body);
    if (jsonData == true) {
      return true;
    }

    return false;
  }

//MARCA PRODUTO - Update
  static Future<bool> UpdateMarcaProduto(
      int id, String nome, bool ativo) async {
    var url = "http://inventoryanalytics.me/api/MarcaProdutoUpdate";
    var body = json.encode({'Id': id, 'Nome': nome, 'Ativo': ativo});

    Map<String, String> headers = {
      'Content-type': 'application/json',
      'Accept': 'application/json',
    };

    final response = await http.post(url, body: body, headers: headers);
    var jsonData = json.decode(response.body);
    if (jsonData == true) {
      return true;
    }

    return false;
  }

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////

//UNIDADE MEDIDA
  static Future<List<UnidadeMedidaModel>> getUnidadesMedida() async {
    var url = "http://inventoryanalytics.me/api/UnidadeMedidaList";
    final response = await http.get(url);
    var jsonData = json.decode(response.body);
    List<UnidadeMedidaModel> unidadesMedida = [];
    for (var u in jsonData) {
      UnidadeMedidaModel um = UnidadeMedidaModel.fromJson(u);
      unidadesMedida.add(um);
    }

    return unidadesMedida;
  }

//MARCA PRODUTO
  static Future<bool> DeleteUnidadeMedida(int id) async {
    var url = "http://inventoryanalytics.me/api/UnidadeMedidaDelete";
    var body = json.encode({
      'Id': id,
    });

    Map<String, String> headers = {
      'Content-type': 'application/json',
      'Accept': 'application/json',
    };

    final response = await http.post(url, body: body, headers: headers);
    var jsonData = json.decode(response.body);
    if (jsonData == true) {
      return true;
    }

    return false;
  }

//UNIDADE MEDIDA
  static Future<bool> AddUnidadeMedida(
      String nome, String sigla, bool ativo) async {
    var url = "http://inventoryanalytics.me/api/UnidadeMedidaCreate";
    var body =
        json.encode({'Id': 0, 'Nome': nome, 'Sigla': sigla, 'Ativo': ativo});

    Map<String, String> headers = {
      'Content-type': 'application/json',
      'Accept': 'application/json',
    };

    final response = await http.post(url, body: body, headers: headers);
    var jsonData = json.decode(response.body);
    if (jsonData == true) {
      return true;
    }

    return false;
  }

//UNIDADE MEDIDA
  static Future<bool> UpdateUnidadeMedida(
      int id, String nome, String sigla, bool ativo) async {
    var url = "http://inventoryanalytics.me/api/UnidadeMedidaUpdate";
    var body =
        json.encode({'Id': id, 'Nome': nome, 'Sigla': sigla, 'Ativo': ativo});

    Map<String, String> headers = {
      'Content-type': 'application/json',
      'Accept': 'application/json',
    };

    final response = await http.post(url, body: body, headers: headers);
    var jsonData = json.decode(response.body);
    if (jsonData == true) {
      return true;
    }

    return false;
  }

///////////////////////////////////////////////////////////////////////////////////////////////////////////////

//LOCAL ARMAZENAMENTO
  static Future<List<LocalArmazenamentoModel>> getLocaisArmazenamento() async {
    var url = "http://inventoryanalytics.me/api/LocalArmazenamentoList";
    final response = await http.get(url);
    var jsonData = json.decode(response.body);
    List<LocalArmazenamentoModel> locaisArmazenamento = [];
    for (var u in jsonData) {
      LocalArmazenamentoModel la = LocalArmazenamentoModel.fromJson(u);
      locaisArmazenamento.add(la);
    }

    return locaisArmazenamento;
  }

//LOCAL ARMAZENAMENTO
  static Future<bool> DeleteLocalArmazenamento(int id) async {
    var url = "http://inventoryanalytics.me/api/LocalArmazenamentoDelete";
    var body = json.encode({
      'Id': id,
    });

    Map<String, String> headers = {
      'Content-type': 'application/json',
      'Accept': 'application/json',
    };

    final response = await http.post(url, body: body, headers: headers);
    var jsonData = json.decode(response.body);
    if (jsonData == true) {
      return true;
    }

    return false;
  }

//LOCAL ARMAZENAMENTO
  static Future<bool> AddLocalArmazenamento(
      String nome, int capacidadeTotal, bool ativo) async {
    var url = "http://inventoryanalytics.me/api/LocalArmazenamentoCreate";
    var body = json.encode({
      'Id': 0,
      'Nome': nome,
      'CapacidadeTotal': capacidadeTotal,
      'CapacidadeAtual': 0,
      'Ativo': ativo
    });

    Map<String, String> headers = {
      'Content-type': 'application/json',
      'Accept': 'application/json',
    };

    final response = await http.post(url, body: body, headers: headers);
    var jsonData = json.decode(response.body);
    if (jsonData == true) {
      return true;
    }

    return false;
  }

//LOCAL ARMAZENAMENTO
  static Future<bool> UpdateLocalArmazenamento(int id, String nome,
      int capacidadeAtual, int capacidadeTotal, bool ativo) async {
    var url = "http://inventoryanalytics.me/api/LocalArmazenamentoUpdate";
    var body = json.encode({
      'Id': id,
      'Nome': nome,
      'CapacidadeTotal': capacidadeTotal,
      'CapacidadeAtual': capacidadeAtual,
      'Ativo': ativo
    });

    Map<String, String> headers = {
      'Content-type': 'application/json',
      'Accept': 'application/json',
    };

    final response = await http.post(url, body: body, headers: headers);
    var jsonData = json.decode(response.body);
    if (jsonData == true) {
      return true;
    }

    return false;
  }
//////////////////////////////////////////////////////////////////////////////////////////////////////////////

//FORNECEDORES LIST
  static Future<List<FornecedorModel>> getFornecedores() async {
    var url = "http://inventoryanalytics.me/api/FornecedoresList";
    final response = await http.get(url);
    var jsonData = json.decode(response.body);
    List<FornecedorModel> fornecedores = [];
    for (var u in jsonData) {
      FornecedorModel la = FornecedorModel.fromJson(u);
      fornecedores.add(la);
    }

    return fornecedores;
  }

//Fornecedores DELETE
  static Future<bool> DeleteFornecedor(int id) async {
    var url = "http://inventoryanalytics.me/api/FornecedoresDelete";
    var body = json.encode({
      'Id': id,
    });

    Map<String, String> headers = {
      'Content-type': 'application/json',
      'Accept': 'application/json',
    };

    final response = await http.post(url, body: body, headers: headers);
    var jsonData = json.decode(response.body);
    if (jsonData == true) {
      return true;
    }

    return false;
  }

//Fornecedores ADD
//gpEd.nome, gpEd.numDocumento, gpEd.telefone, gpEd.logradouro, gpEd.numero, gpEd.cep, gpEd.pais, gpEd.ativo
  static Future<bool> AddFornecedor(
      String nome,
      String numDoc,
      String telefone,
      String logradouro,
      String numero,
      String cep,
      String pais,
      bool ativo) async {
    var url = "http://inventoryanalytics.me/api/FornecedoresCreate";
    var body = json.encode({
      'Id': 0,
      'Nome': nome,
      'RazaoSocial': "Não cadastrado",
      'NumDocumento': numDoc,
      'Tipo': 2,
      'Telefone': telefone,
      'Contato': "Não cadastrado",
      'Logradouro': logradouro,
      'Numero': numero,
      'Complemento': "Não cadastrado",
      'Cep': cep,
      'Bairro': "Não cadastrado",
      'Cidade': "Não cadastrado",
      'Estado': "Não cadastrado",
      'Pais': pais,
      'Ativo': ativo
    });

    Map<String, String> headers = {
      'Content-type': 'application/json',
      'Accept': 'application/json',
    };

    final response = await http.post(url, body: body, headers: headers);
    var jsonData = json.decode(response.body);
    if (jsonData == true) {
      return true;
    }

    return false;
  }

//Fornecedores
//gpEd.nome, gpEd.razaoSocial, gpEd.numDocumento, gpEd.tipo, gpEd.telefone, gpEd.contato, gpEd.logradouro,  gpEd.numero, gpEd.complemento, gpEd.cep, gpEd.bairro, gpEd.cidade, gpEd.estado, gpEd.pais, gpEd.ativo
  static Future<bool> UpdateFornecedor(
      int id,
      String nome,
      String razaoSocial,
      String numDoc,
      int tipo,
      String telefone,
      String contato,
      String logradouro,
      String numero,
      String complemento,
      String cep,
      String bairro,
      String cidade,
      String estado,
      String pais,
      bool ativo) async {
    var url = "http://inventoryanalytics.me/api/FornecedoresUpdate";
    var body = json.encode({
      'Id': id,
      'Nome': nome,
      'RazaoSocial': razaoSocial,
      'NumDocumento': numDoc,
      'Tipo': tipo,
      'Telefone': telefone,
      'Contato': contato,
      'Logradouro': logradouro,
      'Numero': numero,
      'Complemento': complemento,
      'Cep': cep,
      'Bairro': bairro,
      'Cidade': cidade,
      'Estado': estado,
      'Pais': pais,
      'Ativo': ativo
    });

    Map<String, String> headers = {
      'Content-type': 'application/json',
      'Accept': 'application/json',
    };

    final response = await http.post(url, body: body, headers: headers);
    var jsonData = json.decode(response.body);
    if (jsonData == true) {
      return true;
    }

    return false;
  }

////////////////////////////////////////////////////
//PRODUTO
  static Future<List<ProdutoModel>> getProdutos() async {
    var url = "http://inventoryanalytics.me/api/ProdutoList";
    final response = await http.get(url);
    var jsonData = json.decode(response.body);
    List<ProdutoModel> produtos = [];
    for (var u in jsonData) {
      ProdutoModel la = ProdutoModel.fromJson(u);
      la.precoCusto = la.precoCusto;
      la.precoVenda = la.precoVenda;
      produtos.add(la);
    }

    return produtos;
  }

//PRODUTO
  static Future<bool> deleteProduto(int id) async {
    var url = "http://inventoryanalytics.me/api/ProdutoDelete";
    var body = json.encode({
      'Id': id,
    });

    Map<String, String> headers = {
      'Content-type': 'application/json',
      'Accept': 'application/json',
    };

    final response = await http.post(url, body: body, headers: headers);
    var jsonData = json.decode(response.body);
    if (jsonData == true) {
      return true;
    }

    return false;
  }

//PRODUTO
  static Future<bool> addProduto(
      String codigo,
      String nome,
      double precoCusto,
      double precoVenda,
      int quantEstoque,
      int idUnidadeMedida,
      int idGrupo,
      int idMarca,
      int idFornecedor,
      int idLocalArmazenamento,
      bool ativo) async {
    var url = "http://inventoryanalytics.me/api/ProdutoCreate";
    var precoC = precoCusto.toString();
    var precoV = precoVenda.toString();
    precoC = precoC.replaceAll(".", "");
    precoV = precoV.replaceAll(".", "");
    var body = json.encode({
      'Id': 0,
      'Codigo': codigo,
      'Nome': nome,
      'PrecoCusto': precoCusto,
      'PrecoVenda': precoVenda,
      'QuantEstoque': quantEstoque,
      'IdUnidadeMedida': 4,
      'UnidadeMedida': null,
      'IdGrupo': 24,
      'GrupoProduto': null,
      'IdMarca': 12,
      'MarcaProduto': null,
      'IdFornecedor': 10,
      'Fornecedor': null,
      'IdLocalArmazenamento': 12,
      'Ativo': ativo
    });

    Map<String, String> headers = {
      'Content-type': 'application/json',
      'Accept': 'application/json',
    };

    final response = await http.post(url, body: body, headers: headers);
    var jsonData = json.decode(response.body);
    if (jsonData == true) {
      return true;
    }

    return false;
  }

//PRODUTO
  static Future<bool> updateProduto(
      int id,
      String codigo,
      String nome,
      double precoCusto,
      double precoVenda,
      int quantEstoque,
      int idUnidadeMedida,
      int idGrupo,
      int idMarca,
      int idFornecedor,
      int idLocalArmazenamento,
      bool ativo) async {
    var precoC = precoCusto.toString();
    var precoV = precoVenda.toString();
    precoC = precoC.replaceAll(".", "");
    precoV = precoV.replaceAll(".", "");


    var url = "http://inventoryanalytics.me/api/ProdutoUpdate";
    var body = json.encode({
      'Id': id,
      'Codigo': codigo,
      'Nome': nome,
      'PrecoCusto': precoC,
      'PrecoVenda': precoV,
      'QuantEstoque': quantEstoque,
      'IdUnidadeMedida': 4,
      'UnidadeMedida': null,
      'IdGrupo': 24,
      'GrupoProduto': null,
      'IdMarca': 12,
      'MarcaProduto': null,
      'IdFornecedor': 10,
      'Fornecedor': null,
      'IdLocalArmazenamento': 12,
      'Ativo': ativo
    });

    Map<String, String> headers = {
      'Content-type': 'application/json',
      'Accept': 'application/json',
    };

    final response = await http.post(url, body: body, headers: headers);
    var jsonData = json.decode(response.body);
    if (jsonData == true) {
      return true;
    }

    return false;
  }

////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//USER LOGIN SESSION
  static Future<Usuario> getUserSession() async {
    Usuario user = new Usuario();
    Perfil perfil = new Perfil();
    KeyC keyC = new KeyC();

    SharedPreferences pref = await SharedPreferences.getInstance();
    user.nome = pref.getString("user_nome");
    user.email = pref.getString("user_email");
    user.senha = pref.getString("user_senha");
    perfil.nome = pref.getString("user_perfil_nome");
    user.perfil = perfil;
    keyC.codigo = pref.getString("user_apikey");
    user.key = keyC;
    user.login = pref.getString("user_login");
    return user;
  }

  static setIsLogin() async {
    SharedPreferences pref = await SharedPreferences.getInstance();
    pref.setBool("is_login", false);
  }

  static Future<void> setUserLogin(Usuario user) async {
    SharedPreferences pref = await SharedPreferences.getInstance();
    pref.setString("user_nome", user.nome);
    pref.setString("user_email", user.email);
    pref.setString("user_senha", user.senha);
    pref.setString("user_perfil_nome", user.perfil.nome);
    pref.setString("user_apikey", user.key.codigo);
    pref.setString("user_login", user.login);
    pref.setBool("is_login", true);
  }

  static isUserLogin() async {
    SharedPreferences pref = await SharedPreferences.getInstance();
    bool value = pref.getBool("is_login");
    return value;
  }

  static Future<void> logout(BuildContext context) async {
    SharedPreferences pref = await SharedPreferences.getInstance();
    pref.remove("user_nome");
    pref.remove("user_email");
    pref.remove("user_senha");
    pref.remove("user_perfil_nome");
    pref.remove("user_apikey");
    pref.remove("user_login");
    pref.remove("is_login");
    Navigator.pop(context);
    Navigator.pushNamed(context, '/login');
  }

  static Future<String> getUserNome() async {
    SharedPreferences pref = await SharedPreferences.getInstance();
    String nome = pref.getString("user_nome");
    print(nome);
    return nome;
  }

  static Future<String> getUserEmail() async {
    SharedPreferences pref = await SharedPreferences.getInstance();
    return pref.getString("user_email");
  }

  static Future<String> getUserSenha() async {
    SharedPreferences pref = await SharedPreferences.getInstance();
    return pref.getString("user_senha");
  }

  static Future<String> getUserPerfilNome() async {
    SharedPreferences pref = await SharedPreferences.getInstance();
    return pref.getString("user_perfil_nome");
  }

  static Future<String> getUserApiKey() async {
    SharedPreferences pref = await SharedPreferences.getInstance();
    return pref.getString("user_apikey");
  }

  static Future<String> getUserLogin() async {
    SharedPreferences pref = await SharedPreferences.getInstance();
    return pref.getString("user_login");
  }
}
