class UnidadeMedidaModel {
  int id;
  String nome;
  String sigla;
  bool ativo;

  UnidadeMedidaModel({this.id, this.nome, this.sigla, this.ativo});

  UnidadeMedidaModel.fromJson(Map<String, dynamic> json) {
    id = json['Id'];
    nome = json['Nome'];
    sigla = json['Sigla'];
    ativo = json['Ativo'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['Id'] = this.id;
    data['Nome'] = this.nome;
    data['Sigla'] = this.sigla;
    data['Ativo'] = this.ativo;
    return data;
  }
}