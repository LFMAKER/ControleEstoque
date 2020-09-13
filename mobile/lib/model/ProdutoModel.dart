import 'package:inventory_analytics/model/FornecedorModel.dart';
import 'package:inventory_analytics/model/GrupoProdutoModel.dart';
import 'package:inventory_analytics/model/LocalArmazenamentoModel.dart';
import 'package:inventory_analytics/model/MarcaModel.dart';
import 'package:inventory_analytics/model/UnidadeMedidaModel.dart';

class ProdutoModel {
  int id;
  String codigo;
  String nome;
  double precoCusto;
  double precoVenda;
  int quantEstoque;
  int idUnidadeMedida;
  UnidadeMedidaModel unidadeMedida;
  int idGrupo;
  GrupoProdutoModel grupoProduto;
  int idMarca;
  MarcaProdutoModel marcaProduto;
  int idFornecedor;
  FornecedorModel fornecedor;
  int idLocalArmazenamento;
  LocalArmazenamentoModel localArmazenamento;
  bool ativo;

  ProdutoModel(
      {this.id,
      this.codigo,
      this.nome,
      this.precoCusto,
      this.precoVenda,
      this.quantEstoque,
      this.idUnidadeMedida,
      this.unidadeMedida,
      this.idGrupo,
      this.grupoProduto,
      this.idMarca,
      this.marcaProduto,
      this.idFornecedor,
      this.fornecedor,
      this.idLocalArmazenamento,
      this.localArmazenamento,
      this.ativo});

  ProdutoModel.fromJson(Map<String, dynamic> json) {
    id = json['Id'];
    codigo = json['Codigo'];
    nome = json['Nome'];
    precoCusto = json['PrecoCusto'];
    precoVenda = json['PrecoVenda'];
    quantEstoque = json['QuantEstoque'];
    idUnidadeMedida = json['IdUnidadeMedida'];
    unidadeMedida = json['UnidadeMedida'] != null
        ? new UnidadeMedidaModel.fromJson(json['UnidadeMedida'])
        : null;
    idGrupo = json['IdGrupo'];
    grupoProduto = json['GrupoProduto'] != null
        ? new GrupoProdutoModel.fromJson(json['GrupoProduto'])
        : null;
    idMarca = json['IdMarca'];
    marcaProduto = json['MarcaProduto'] != null
        ? new MarcaProdutoModel.fromJson(json['MarcaProduto'])
        : null;
    idFornecedor = json['IdFornecedor'];
    fornecedor = json['Fornecedor'] != null
        ? new FornecedorModel.fromJson(json['Fornecedor'])
        : null;
    idLocalArmazenamento = json['IdLocalArmazenamento'];
    localArmazenamento = json['LocalArmazenamento'] != null
        ? new LocalArmazenamentoModel.fromJson(json['LocalArmazenamento'])
        : null;
    ativo = json['Ativo'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['Id'] = this.id;
    data['Codigo'] = this.codigo;
    data['Nome'] = this.nome;
    data['PrecoCusto'] = this.precoCusto;
    data['PrecoVenda'] = this.precoVenda;
    data['QuantEstoque'] = this.quantEstoque;
    data['IdUnidadeMedida'] = this.idUnidadeMedida;
    if (this.unidadeMedida != null) {
      data['UnidadeMedida'] = this.unidadeMedida.toJson();
    }
    data['IdGrupo'] = this.idGrupo;
    if (this.grupoProduto != null) {
      data['GrupoProduto'] = this.grupoProduto.toJson();
    }
    data['IdMarca'] = this.idMarca;
    if (this.marcaProduto != null) {
      data['MarcaProduto'] = this.marcaProduto.toJson();
    }
    data['IdFornecedor'] = this.idFornecedor;
    if (this.fornecedor != null) {
      data['Fornecedor'] = this.fornecedor.toJson();
    }
    data['IdLocalArmazenamento'] = this.idLocalArmazenamento;
    if (this.localArmazenamento != null) {
      data['LocalArmazenamento'] = this.localArmazenamento.toJson();
    }
    data['Ativo'] = this.ativo;
    return data;
  }
}




